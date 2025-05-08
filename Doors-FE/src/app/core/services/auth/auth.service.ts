import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpBackend, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import {
  BehaviorSubject,
  catchError,
  delay,
  EMPTY,
  finalize,
  map,
  Observable,
  of,
  shareReplay,
  switchMap,
  tap,
  throwError
} from 'rxjs';
import { ApiResponse, LoginRequest, LoginResponse } from '../../../models/auth.models';
import { JwtHelperService } from '@auth0/angular-jwt';
import {LoggerService} from '../logger/logger.service';

const API_BASE_URL = `${environment.apiUrl}/auth`;
const VALID_ROLES = ['student', 'landlord', 'company', 'institution', 'superadmin'] as const;
type Role = typeof VALID_ROLES[number];
type SubRole = 'admin' | 'recruiter' | 'viewer' | 'editor' | 'privateLandlord' | null;
const ROLE_CLAIM = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly broadcastChannel: BroadcastChannel = new BroadcastChannel('auth-channel');
  private readonly loginUrl = `${API_BASE_URL}/login`;
  private readonly logoutUrl = `${API_BASE_URL}/logout`;
  private readonly refreshUrl = `${API_BASE_URL}/refresh`;
  private readonly jwtHelper = new JwtHelperService();

  private isAuthenticated$ = new BehaviorSubject<boolean>(false);
  private currentRole$ = new BehaviorSubject<Role | null>(null);
  private currentSubRole$ = new BehaviorSubject<SubRole>(null);
  private refreshTokenTimeout: any;
  private refreshTokenInProgress$: Observable<LoginResponse> | null = null;
  private authReady$ = new BehaviorSubject<boolean>(false);
  public authStateReady$ = this.authReady$.asObservable();
  private suppressRedirectOnce = false;
  private readonly RECENT_LOGOUT_KEY = 'recentLogout';

  // Propriétés en mémoire
  private lastPrivateRoute: string | null = null;
  private recentLogout: boolean = false;
  private lastRefresh: number = 0;
  private authInitInProgress: boolean = true;

  private accessToken: string | null = null;
  private authHttp: HttpClient;

  constructor(
    private readonly router: Router,
    httpBackend: HttpBackend,
    private readonly logger: LoggerService,
  ) {
    this.authHttp = new HttpClient(httpBackend);
    // Restaurer recentLogout depuis sessionStorage au démarrage
    this.recentLogout = sessionStorage.getItem(this.RECENT_LOGOUT_KEY) === 'true';
    this.logger.log('AuthService initialized, recentLogout:', this.recentLogout);
    this.initializeAuthState();
    this.setupVisibilityListener();
    this.setupBroadcastListener();
  }

  private setupBroadcastListener(): void {
    this.broadcastChannel.onmessage = (event) => {
      const data = event.data;
      this.logger.log('📡 Message reçu via BroadcastChannel:', data);

      if (data === 'logout') {
        this.logger.log('📡 Déconnexion détectée dans un autre onglet');
        this.recentLogout = true; // Marquer le logout dans cet onglet
        sessionStorage.setItem(this.RECENT_LOGOUT_KEY, 'true'); // Persister
        this.clearAuthState(false, false); // Ne pas rediffuser logout
        this.logger.log('🔁 Forcer la redirection vers / dans cet onglet');
        this.router.navigate(['/']).catch(err => this.logger.error('Navigation error:', err));
      } else if (data?.type === 'login') {
        this.logger.log('📡 Connexion détectée dans un autre onglet, synchronisation...');
        const receivedToken = data.token;
        if (receivedToken && !this.jwtHelper.isTokenExpired(receivedToken)) {
          this.logger.log('✅ Token valide reçu, mise à jour de l’état');
          const loginResponse: LoginResponse = {
            accessToken: receivedToken,
            refreshToken: ''
          };
          this.handleLoginSuccess(loginResponse, false);
          this.authReady$.next(true);
        } else {
          this.logger.warn('⚠️ Token invalide ou absent, vérification backend');
          this.refreshToken().subscribe({
            next: () => this.logger.log('✅ État synchronisé après login broadcast'),
            error: () => {
              this.logger.warn('❌ Échec de synchronisation, nettoyage');
              this.clearAuthState(true, false);
            }
          });
        }
      } else if (data === 'refresh') {
        this.logger.log('📡 Rafraîchissement détecté dans un autre onglet');
        const token = this.getToken();
        if (token && !this.jwtHelper.isTokenExpired(token)) {
          this.logger.log('✅ Token local valide, pas de rafraîchissement nécessaire');
          this.authReady$.next(true);
          return;
        }
        this.logger.log('🔄 Token expiré ou absent, tentative de rafraîchissement');
        this.refreshToken().subscribe({
          next: () => this.logger.log('✅ Rafraîchissement réussi après broadcast'),
          error: () => {
            this.logger.warn('❌ Échec du rafraîchissement, nettoyage');
            this.clearAuthState(true, false);
          }
        });
      } else {
        this.logger.warn('⚠️ Message BroadcastChannel inconnu:', data);
      }
    };
  }



  checkForSession(): Observable<boolean> {
    this.logger.log('🔍 Envoi de la requête /api/check-session avec withCredentials: true');
    return this.authHttp.get<ApiResponse<{ isAuthenticated?: boolean; IsAuthenticated?: boolean }>>(
      `${API_BASE_URL}/check-session`,
      { withCredentials: true }
    ).pipe(
      map(response => {
        this.logger.log('🔍 Réponse complète de /api/check-session:', JSON.stringify(response, null, 2));
        if (typeof response?.data?.isAuthenticated === 'boolean') {
          this.logger.log('✅ Session vérifiée (isAuthenticated):', response.data.isAuthenticated);
          return response.data.isAuthenticated;
        }
        if (typeof response?.data?.IsAuthenticated === 'boolean') {
          this.logger.log('✅ Session vérifiée (IsAuthenticated):', response.data.IsAuthenticated);
          return response.data.IsAuthenticated;
        }
        this.logger.warn('⚠️ Réponse inattendue de /api/check-session:', response);
        return false;
      }),
      catchError((error) => {
        this.logger.error('❌ Erreur lors de la vérification de session:', error);
        if (error?.error?.errorKey === 'INTERNAL_SERVER_ERROR') {
          this.logger.warn('⛔ Erreur serveur interne détectée');
          return throwError(() => new Error('INTERNAL_SERVER_ERROR'));
        }
        this.logger.log('🔄 Aucune session détectée, retour à false');
        return of(false);
      })
    );
  }

  private initializeAuthState(): void {
    this.logger.log(' Initialisation de l’état d’authentification...');
    this.logger.log('Cookies actuels (peut être vide si HttpOnly):', document.cookie);

    this.authInitInProgress = true;

    if (this.recentLogout) {
      this.logger.log('ℹ️ Déconnexion récente détectée, initialisation sans rafraîchissement');
      this.clearAuthState(false, false);
      this.authReady$.next(true);
      this.authInitInProgress = false;
      return;
    }

    // Tenter directement un rafraîchissement du token
    this.refreshToken().pipe(
      tap(() => this.logger.log('✅ Rafraîchissement initial réussi')),
      catchError((err) => {
        this.logger.warn('❌ Échec du rafraîchissement initial:', err);
        this.clearAuthState(false, false);
        return of(null);
      }),
      finalize(() => {
        this.logger.log('✅ Initialisation terminée, authStateReady TRUE');
        this.authInitInProgress = false;
        this.authReady$.next(true);
      })
    ).subscribe();
  }

  private setupVisibilityListener(): void {
    document.addEventListener('visibilitychange', () => {
      if (document.visibilityState !== 'visible') return;

      this.logger.log('👁️ Onglet redevient actif → vérification de l’état');

      if (this.recentLogout) {
        this.logger.log('ℹ️ Déconnexion récente, redirection vers /');
        this.clearAuthState(false, false);
        this.router.navigate(['/']).catch(err => this.logger.error('Navigation error:', err));
        return;
      }

      const token = this.getToken();
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        this.logger.log('✅ Token local valide, restauration de l’état');
        this.isAuthenticated$.next(true);
        this.authReady$.next(true);
        return;
      }

      this.logger.log('🔄 Token expiré ou absent, tentative de rafraîchissement');
      this.refreshToken().subscribe({
        next: () => this.logger.log('✅ Rafraîchissement réussi après retour visibilité'),
        error: () => {
          this.logger.warn('❌ Échec du rafraîchissement, nettoyage');
          this.clearAuthState(true);
        }
      });
    });
  }

  login(email: string, password: string, retryCount = 0): Observable<LoginResponse> {
    const MAX_RETRIES = 1;
    if (!email || !password) {
      return throwError(() => ({
        key: 'MissingCredentials',
        message: 'Email et mot de passe sont requis.',
        field: null
      }));
    }

    const request: LoginRequest = { Email: email, Password: password };
    this.logger.log('📤 Tentative de connexion :', { email, retryCount });

    return this.authHttp.post<ApiResponse<LoginResponse>>(this.loginUrl, request, { withCredentials: true }).pipe(
      tap((res) => this.logger.log('✅ Réponse du backend :', res)),
      switchMap((response) => {
        if (!response) {
          return throwError(() => ({
            key: 'EmptyResponse',
            message: 'Réponse vide du backend.',
            field: null
          }));
        }

        if (response.data?.accessToken) {
          this.logger.log('✅ Connexion réussie, traitement...');
          this.handleLoginSuccess(response.data, true);
          this.authReady$.next(true);
          return of(response.data);
        }

        if (response.error?.key === 'AlreadyAuthenticated' && retryCount < MAX_RETRIES) {
          this.logger.warn('⚠️ Déjà authentifié, tentative de déconnexion et reconnexion...');
          return this.logout().pipe(
            tap(() => this.logger.log('🔁 Déconnexion effectuée, nouvelle tentative de connexion')),
            switchMap(() => this.login(email, password, retryCount + 1))
          );
        }

        this.logger.error('❌ Échec de la connexion :', response.error);
        return throwError(() => response.error ?? {
          key: 'UnknownError',
          message: 'Erreur inattendue lors de la connexion.',
          field: null
        });
      }),
      catchError((error: any) => {
        this.logger.error('❌ Erreur de connexion :', { error, retryCount, cookies: document.cookie });
        return throwError(() => error);
      })
    );
  }

  private isPublicRoute(route: string, normalized: string): boolean {
    const publicRoutes = [
      '/', '/login', '/register', '/student', '/student/:section',
      '/professionnel', '/professionnel/:section',
      '/register/company/invite', '/register/institution/invite',
      '/register/company/colleague/invite', '/register/institution/colleague/invite',
      '/register/public/:roleName', '/confirm-email',
      '/request-reset-password', '/reset-password', '/contact'
    ];
    return publicRoutes.includes(route) || publicRoutes.includes(normalized);
  }

  refreshToken(): Observable<LoginResponse> {
    this.logger.log('🔄 Tentative de rafraîchissement du token...');
    this.logger.log('Cookies actuels:', document.cookie);

    if (this.recentLogout) {
      this.logger.log('🚫 Déconnexion récente, aucun rafraîchissement');
      return throwError(() => ({
        key: 'RECENT_LOGOUT',
        message: 'Déconnexion récente, aucun rafraîchissement possible'
      }));
    }

    const token = this.getToken();
    const now = Date.now();
    const tooSoon = now - this.lastRefresh < 5000;

    if (tooSoon && token && !this.jwtHelper.isTokenExpired(token)) {
      this.logger.log('⏳ Rafraîchissement ignoré : token valide et récent');
      return of({ accessToken: token, refreshToken: '' });
    }

    if (this.refreshTokenInProgress$) {
      this.logger.log('🔄 Rafraîchissement déjà en cours, renvoi de l’observable existant');
      return this.refreshTokenInProgress$;
    }

    if (document.visibilityState !== 'visible') {
      this.logger.log('🚫 Onglet non actif — report du rafraîchissement');
      return EMPTY;
    }

    const attemptRefresh = (retryCount = 0, maxRetries = 2): Observable<LoginResponse> => {
      const startTime = Date.now();
      return this.authHttp.post<ApiResponse<LoginResponse>>(
        this.refreshUrl,
        {},
        { withCredentials: true }
      ).pipe(
        tap(() => this.logger.log(`🔄 Requête /refresh terminée en ${Date.now() - startTime}ms`)),
        map(response => {
          if (!response.data?.accessToken) {
            throw new Error('Aucun token d’accès retourné');
          }
          return response.data;
        }),
        tap(loginResponse => {
          this.logger.log('✅ Token rafraîchi avec succès');
          this.handleLoginSuccess(loginResponse, false);
          this.lastRefresh = Date.now();
          this.broadcastChannel.postMessage('refresh');
        }),
        catchError((error: HttpErrorResponse) => {
          this.logger.error('❌ Erreur de rafraîchissement:', {
            status: error.status,
            statusText: error.statusText,
            message: error.message,
            error: error.error,
            cookies: document.cookie
          });
          if (error.status === 0) {
            this.logger.warn('🔐 Erreur CORS ou serveur inaccessible');
            this.clearAuthState(false);
            return throwError(() => ({
              key: 'CORS_OR_SERVER_ERROR',
              message: 'Impossible de contacter le serveur.'
            }));
          }
          if (error.status === 401 || error.error?.key === 'REFRESH_TOKEN_INVALID_OR_EXPIRED') {
            this.logger.warn('🔐 Token de rafraîchissement invalide ou expiré');
            this.clearAuthState(false);
            return throwError(() => ({
              key: 'SESSION_EXPIREE',
              message: 'Session expirée - veuillez vous reconnecter'
            }));
          }
          if (error.error?.key === 'REFRESH_TOKEN_ALREADY_USED' && retryCount < maxRetries) {
            this.logger.warn('🔄 Refresh token déjà utilisé, nouvelle tentative après délai');
            return of(null).pipe(
              delay(1000),
              switchMap(() => attemptRefresh(retryCount + 1, maxRetries))
            );
          }
          if (error.status === 400) {
            this.logger.warn('🔐 Requête de rafraîchissement invalide:', error.error);
            if (error.error?.key === 'NO_REFRESH_TOKEN' || error.error?.key === 'INVALID_MODEL_STATE') {
              this.logger.log('ℹ️ Aucun refresh token, état non authentifié');
              this.clearAuthState(false);
              return throwError(() => ({
                key: 'NO_REFRESH_TOKEN',
                message: 'Aucune session active'
              }));
            }

            this.clearAuthState(false);
            return throwError(() => ({
              key: 'REQUETE_INVALIDE',
              message: 'Requête de rafraîchissement invalide - veuillez vous reconnecter'
            }));
          }
          this.clearAuthState(false);
          return throwError(() => ({
            key: 'ERREUR_RESEAU',
            message: 'Problème de connexion, nouvelle tentative ultérieure'
          }));
        }),
        finalize(() => {
          this.logger.log('✅ Rafraîchissement terminé, réinitialisation de l’observable');
          this.refreshTokenInProgress$ = null;
        }),
        shareReplay(1)
      );
    };

    this.refreshTokenInProgress$ = attemptRefresh();
    return this.refreshTokenInProgress$;
  }

  logout(): Observable<void> {
    this.logger.log(' Tentative de déconnexion...');
    this.logger.log('Cookies actuels:', document.cookie);
    return this.authHttp.post<void>(this.logoutUrl, {}, { withCredentials: true }).pipe(
      tap(() => {
        this.logger.log('🔁 Déconnexion effectuée');
        this.recentLogout = true;
        sessionStorage.setItem(this.RECENT_LOGOUT_KEY, 'true');
        this.clearAuthState(true, true);
        this.router.navigate(['/']).catch(err => this.logger.error('Navigation error:', err));
      }),
      catchError((error: HttpErrorResponse) => {
        this.logger.error('❌ Erreur de déconnexion:', {
          status: error.status,
          statusText: error.statusText,
          message: error.message,
          error: error.error,
          cookies: document.cookie
        });
        this.recentLogout = true;
        sessionStorage.setItem(this.RECENT_LOGOUT_KEY, 'true');
        this.clearAuthState(true, true);
        this.router.navigate(['/']).catch(err => this.logger.error('Navigation error:', err));
        return throwError(() => error);
      })
    );
  }

  public handleLoginSuccess(response: LoginResponse, sendBroadcast: boolean = true): void {
    if (!response.accessToken) {
      this.logger.warn('Aucun accessToken reçu');
      return;
    }
    this.accessToken = response.accessToken;

    const { role, subRole } = this.extractRoleFromToken(response.accessToken);
    this.isAuthenticated$.next(true);
    this.currentRole$.next(role);
    this.currentSubRole$.next(subRole);

    this.recentLogout = false;
    sessionStorage.removeItem(this.RECENT_LOGOUT_KEY);

    this.scheduleTokenRefresh(response.accessToken);

    this.logger.log('✅ Connexion réussie, cookies actuels :', document.cookie);
    this.logger.log('🚪 Redirection vers la page d’accueil pour le rôle :', role);
    if (sendBroadcast) {
      this.logger.log('📡 Diffusion de l’événement login avec token');
      this.broadcastChannel.postMessage({ type: 'login', token: response.accessToken });
    }
    this.authReady$.next(true);
    this.suppressRedirectOnce = true;

    const currentUrlTree = this.router.parseUrl(this.router.url);
    const returnUrl = currentUrlTree.queryParams['returnUrl'];

    if (returnUrl) {
      this.logger.log('🔁 Redirection vers returnUrl depuis l’URL :', returnUrl);
      this.router.navigateByUrl(returnUrl).catch(err => this.logger.error('Navigation error:', err));
      return;
    }

    const lastRoute = this.getLastPrivateRoute();
    if (lastRoute && !this.isPublicRoute(lastRoute, lastRoute)) {
      this.logger.log('🔁 Redirection vers la dernière route privée :', lastRoute);
      this.router.navigateByUrl(lastRoute).catch(err => this.logger.error('Navigation error:', err));
    } else {
      this.redirectToHome(role);
    }
  }

  public getToken(): string | null {
    return this.accessToken;
  }

  private scheduleTokenRefresh(token: string): void {
    if (this.refreshTokenTimeout) {
      clearTimeout(this.refreshTokenTimeout);
    }
    if (this.jwtHelper.isTokenExpired(token)) {
      this.logger.warn('Token déjà expiré - pas de rafraîchissement programmé');
      return;
    }

    const expiration = this.jwtHelper.getTokenExpirationDate(token);
    if (!expiration) return;

    const expiresIn = expiration.getTime() - Date.now() - 60000;

    if (expiresIn > 0) {
      this.refreshTokenTimeout = setTimeout(() => {
        this.refreshToken().subscribe({
          error: (err) => {
            this.logger.error('Échec rafraîchissement:', err);
            this.clearAuthState(true);
          }
        });
      }, expiresIn);
    }
  }

  public updateLastPrivateRoute(route: string): void {
    const isPublic = this.isPublicRoute(route, route);
    if (!isPublic) {
      this.lastPrivateRoute = route;
    }
  }

  public getLastPrivateRoute(): string | null {
    return this.lastPrivateRoute;
  }

  clearAuthState(shouldRedirect: boolean = true, broadcastLogout: boolean = true): void {
    if (this.refreshTokenTimeout) {
      clearTimeout(this.refreshTokenTimeout);
      this.refreshTokenTimeout = null;
    }
    this.isAuthenticated$.next(false);
    this.currentRole$.next(null);
    this.currentSubRole$.next(null);
    this.accessToken = null;

    this.lastPrivateRoute = null;

    this.logger.log('🔒 Auth state cleared', { shouldRedirect, broadcastLogout, currentPath: this.router.url });

    if (shouldRedirect && !this.suppressRedirectOnce && !this.authInitInProgress) {
      const currentRoute = this.router.url.split('?')[0];
      const normalizedRoute = currentRoute.replace(/\/[^\/]+$/, '/:section').replace(/\/[^\/]+$/, '/:roleName');
      if (!this.isPublicRoute(currentRoute, normalizedRoute)) {
        this.logger.log('🚪 Redirection immédiate vers / depuis la route :', currentRoute);
        this.router.navigate(['/']).catch(err => this.logger.error('Navigation error:', err));
      } else {
        this.logger.log('ℹ️ Déjà sur une route publique, pas de redirection', { currentRoute, normalizedRoute });
      }
    } else {
      this.suppressRedirectOnce = false;
    }

    if (broadcastLogout) {
      this.logger.log('📡 Envoi du message logout via BroadcastChannel');
      this.broadcastChannel.postMessage('logout');
    }

    this.authReady$.next(true);
  }

  isLoggedIn(): Observable<boolean> {
    return this.isAuthenticated$.asObservable();
  }

  isAuthenticatedSync(): boolean {
    return !!this.accessToken && !this.jwtHelper.isTokenExpired(this.accessToken);
  }

  getRole(): Observable<Role | null> {
    return this.currentRole$.asObservable();
  }

  getSubRole(): Observable<SubRole> {
    return this.currentSubRole$.asObservable();
  }

  getCurrentRoleSync(): Role | null {
    return this.currentRole$.value;
  }

  getCurrentSubRoleSync(): SubRole {
    return this.currentSubRole$.value;
  }

  private redirectToHome(role: Role): void {
    const routes: Record<Role, string> = {
      student: '/student-home',
      landlord: '/landlord-home',
      company: '/company-home',
      institution: '/institution-home',
      superadmin: '/superadmin-home'
    };
    const route = routes[role] || '/';
    this.router.navigate([route]).catch(err => this.logger.error('Navigation error:', err));
  }

  private extractRoleFromToken(token: string): { role: Role; subRole: SubRole } {
    try {
      const decodedToken = this.jwtHelper.decodeToken(token);
      const fullRole = decodedToken?.[ROLE_CLAIM]?.toLowerCase();
      if (!fullRole) {
        this.logger.warn('No role found in token');
        return { role: 'student', subRole: null };
      }

      let role: Role;
      let subRole: SubRole;

      if (fullRole.startsWith('public.')) {
        const publicRole = fullRole.split('.')[1];
        if (publicRole === 'student') {
          role = 'student';
          subRole = null;
        } else if (publicRole === 'privatelandlord') {
          role = 'landlord';
          subRole = 'privateLandlord';
        } else {
          role = 'student';
          subRole = null;
        }
      } else {
        const [rolePart, subRolePart] = fullRole.split('.');
        role = VALID_ROLES.includes(rolePart as Role) ? (rolePart as Role) : 'student';
        subRole = subRolePart ? (subRolePart as SubRole) : null;

        if (fullRole === 'superadmin') {
          role = 'superadmin';
          subRole = null;
        }
      }

      this.logger.log(`Extracted role: ${role}, Sub-role: ${subRole}`);
      return { role, subRole };
    } catch (error) {
      this.logger.warn('Token decoding error:', error);
      return { role: 'student', subRole: null };
    }
  }
}
