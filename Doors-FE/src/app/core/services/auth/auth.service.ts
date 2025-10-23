import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpBackend, HttpClient, HttpErrorResponse } from '@angular/common/http';
import {NavigationEnd, Router} from '@angular/router';
import {
  BehaviorSubject,
  catchError,
  delay,
  EMPTY, filter,
  finalize,
  map,
  Observable,
  of,
  shareReplay,
  switchMap, take,
  tap,
  throwError
} from 'rxjs';
import { ApiResponse, LoginRequest, LoginResponse } from '../../models/auth.models';
import { JwtHelperService } from '@auth0/angular-jwt';
import {LoggerService} from '../logger/logger.service';
import {UserService} from '../user/user.service';

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
  private recentLogout: boolean = false; // Géré en mémoire
  private lastRefresh: number = 0;
  private authInitInProgress: boolean = true;
  private pendingRoute: string | null = null; // Route demandée temporaire
  private pendingLogin: LoginResponse | null = null;

  private accessToken: string | null = null;
  private authHttp: HttpClient;

  constructor(
    private readonly userService: UserService,
    private readonly router: Router,
    httpBackend: HttpBackend,
    private readonly logger: LoggerService,
  ) {
    this.authHttp = new HttpClient(httpBackend);

    if (!document.cookie || document.cookie.trim() === '') {
      this.logger.log('🔧 Pas de cookies → recentLogout à false');
      this.recentLogout = false;
    }
    this.logger.log('AuthService initialized, recentLogout:', this.recentLogout);
    this.initializeAuthState();
    this.setupVisibilityListener();
    this.setupBroadcastListener();
  }

  // Nouvelle méthode pour stocker la route demandée
  public setPendingRoute(route: string): void {
    this.pendingRoute = route;
    this.logger.log('📍 Route en attente enregistrée :', route);
  }



  private setupBroadcastListener(): void {
    this.broadcastChannel.onmessage = (event) => {
      const data = event.data;
      this.logger.log('📡 Message reçu via BroadcastChannel:', data);

      if (data === 'logout') {
        this.logger.log('📡 Déconnexion détectée dans un autre onglet');
        this.recentLogout = true;
        this.clearAuthState(false, false);
        // Ne pas rediriger automatiquement, laisser la logique de Visibilité gérer
        return;
      }

      if (data?.type === 'login') {
        this.logger.log('📡 Connexion détectée dans un autre onglet, synchronisation...');
        const receivedToken = data.token;
        if (receivedToken && !this.jwtHelper.isTokenExpired(receivedToken)) {
          this.logger.log('✅ Token valide reçu, mise à jour de l’état');
          const loginResponse: LoginResponse = {
            accessToken: receivedToken,
            refreshToken: ''
          };
          if (document.visibilityState === 'visible') {
            this.logger.log('✅ Onglet visible, traitement immédiat');
            this.handleLoginSuccess(loginResponse, false);
            this.authReady$.next(true);
          } else {
            this.logger.log('ℹ Non visible, stockage du jeton');
            this.accessToken = receivedToken;
            this.isAuthenticated$.next(true);
            const { role, subRole } = this.extractRoleFromToken(receivedToken);
            this.currentRole$.next(role);
            this.currentSubRole$.next(subRole);
            this.authReady$.next(true);
            this.pendingLogin = loginResponse;
            this.logger.log('🔄 Connexion en attente définie:', loginResponse);
          }
        } else {
          this.logger.warn('⚠️ Token invalide ou absent');
          if (document.visibilityState === 'visible') {
            this.refreshToken().subscribe({
              next: () => this.logger.log('✅ État synchronisé'),
              error: () => {
                this.logger.warn('❌ Échec de synchronisation');
                this.clearAuthState(false, false);
              }
            });
          } else {
            this.logger.log('ℹ Non visible, attente de visibilité');
            this.isAuthenticated$.next(false);
          }
        }
      } else if (data === 'refresh') {
        this.logger.log('📡 Rafraîchissement détecté');
        const token = this.getToken();
        if (token && !this.jwtHelper.isTokenExpired(token)) {
          this.logger.log('✅ Token local valide');
          this.authReady$.next(true);
          return;
        }
        if (document.visibilityState === 'visible') {
          this.refreshToken().subscribe({
            next: () => this.logger.log('✅ Rafraîchissement réussi'),
            error: () => {
              this.logger.warn('❌ Échec du rafraîchissement');
              this.clearAuthState(false, false);
            }
          });
        } else {
          this.logger.log('ℹ Non visible, attente de visibilité');
        }
      } else {
        this.logger.warn('⚠️ Message inconnu:', data);
      }
    };
  }

  checkForSession(): Observable<boolean> {
    this.logger.log('🔍 Envoi de la requête /api/check-session avec withCredentials: true');
    this.logger.log('Current router URL:', this.router.url);
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
        this.logger.error('❌ Erreur lors de la vérification de session:', {
          status: error.status,
          statusText: error.statusText,
          message: error.message,
          error: error.error,
          routerUrl: this.router.url
        });
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
    this.logger.log('🔄 Initialisation de l’état d’authentification...');
    this.logger.log('Cookies actuels:', document.cookie);
    this.logger.log('recentLogout:', this.recentLogout);
    this.logger.log('Token local:', this.getToken());
    this.logger.log('Current router URL:', this.router.url);

    this.authInitInProgress = true;

    // Réinitialiser complètement l’état
    this.isAuthenticated$.next(false);
    this.currentRole$.next(null);
    this.currentSubRole$.next(null);
    this.accessToken = null;
    this.pendingLogin = null;
    this.recentLogout = false; // Réinitialiser recentLogout
    this.pendingRoute = null;

    // Attendre que le routeur ait résolu la route initiale
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      take(1)
    ).subscribe(() => {
      this.logger.log('✅ Route initiale résolue:', this.router.url);

      const token = this.getToken();
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        this.logger.log('✅ Token local valide, restauration immédiate');
        const { role, subRole } = this.extractRoleFromToken(token);
        this.isAuthenticated$.next(true);
        this.currentRole$.next(role);
        this.currentSubRole$.next(subRole);
        this.userService.loadUser();
        this.scheduleTokenRefresh(token);
        this.authReady$.next(true);
        this.authInitInProgress = false;
        return;
      }

      this.logger.log('🔄 Tentative de rafraîchissement du token');
      this.refreshToken().pipe(
        tap((response) => this.logger.log('✅ Rafraîchissement initial réussi:', response)),
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
    });
  }

  private setupVisibilityListener(): void {
    document.addEventListener('visibilitychange', () => {
      if (document.visibilityState !== 'visible') return;

      this.logger.log('👁️ Onglet redevient actif → vérification de l’état');
      console.log('Onglet redevient actif, route actuelle:', this.router.url);

      const currentRoute = this.router.url.split('?')[0];
      const normalizedRoute = currentRoute.replace(/\/[^\/]+$/, '/:section').replace(/\/[^\/]+$/, '/:roleName');

      // Vérifier si une connexion est en attente
      if (this.pendingLogin) {
        this.logger.log('🔄 Traitement d’une connexion en attente');
        this.handleLoginSuccess(this.pendingLogin, false);
        this.pendingLogin = null;
        return;
      }

      // En mode déconnecté sur une route publique, ne rien faire
      if (!this.isAuthenticated$.value && this.isPublicRoute(currentRoute, normalizedRoute)) {
        this.logger.log('ℹ️ Mode déconnecté sur une route publique, aucune action nécessaire');
        return;
      }

      if (this.recentLogout) {
        this.logger.log('ℹ️ Déconnexion récente détectée');
        this.clearAuthState(false, false);
        if (!this.isPublicRoute(currentRoute, normalizedRoute)) {
          this.logger.log('🚪 Redirection vers / depuis une route non publique');
          this.router.navigate(['/']).catch(err => this.logger.error('Navigation error:', err));
        } else {
          this.logger.log('ℹ️ Déconnexion sur une route publique, pas de redirection');
        }
        return;
      }

      const token = this.getToken();
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        this.logger.log('✅ Token local valide, restauration de l’état');
        this.isAuthenticated$.next(true);
        this.authReady$.next(true);
        const { role, subRole } = this.extractRoleFromToken(token);
        this.currentRole$.next(role);
        this.currentSubRole$.next(subRole);

        this.userService.loadUser();
        this.logger.log('📤 Chargement des données utilisateur déclenché');

        if (!this.isPublicRoute(currentRoute, normalizedRoute)) {
          this.logger.log('ℹ️ Connecté sur une route privée, pas de redirection :', currentRoute);
          return;
        }

        if (this.isPublicRoute(currentRoute, normalizedRoute) && currentRoute !== '/login' && currentRoute !== '/') {
          this.logger.log('ℹ️ Connecté sur une route publique, pas de redirection :', currentRoute);
          return;
        }

        if (role && (currentRoute === '/login' || currentRoute === '/')) {
          this.logger.log('🔁 Redirection vers la page d’accueil pour le rôle :', role);
          this.redirectToHome(role);
        }
        return;
      }

      this.logger.log('🔄 Vérification de session pour synchronisation');
      this.checkForSession().subscribe({
        next: (isAuthenticated) => {
          if (isAuthenticated) {
            this.logger.log('✅ Session active détectée, tentative de rafraîchissement');
            this.refreshToken().subscribe({
              next: () => this.logger.log('✅ Rafraîchissement réussi'),
              error: () => {
                this.logger.warn('❌ Échec du rafraîchissement');
                this.clearAuthState(false, false);
              }
            });
          } else {
            this.logger.log('ℹ️ Aucune session active, réinitialisation');
            this.isAuthenticated$.next(false);
            this.authReady$.next(true);
          }
        },
        error: () => {
          this.logger.warn('❌ Échec de la vérification de session');
          this.clearAuthState(false, false);
        }
      });
    });
  }

  login(email: string, password: string,rememberMe :boolean, retryCount = 0): Observable<LoginResponse> {
    const MAX_RETRIES = 1;
    if (!email || !password) {
      return throwError(() => ({
        key: 'MissingCredentials',
        message: 'Email et mot de passe sont requis.',
        field: null
      }));
    }

    const request: LoginRequest = { Email: email, Password: password,RememberMe : rememberMe };
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
            switchMap(() => this.login(email, password,rememberMe, retryCount + 1))
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
        this.logger.log(error.error.error.key);

        const backendError = error?.error?.error || error?.error || error;

        const parsed = {
          key: backendError?.key || 'UnknownError',
          message: backendError?.key || 'Erreur inconnue',
          field: backendError?.field || null,
          extraData: backendError?.extraData || null,
          httpStatus: error.status || null
        };

        this.logger.error('❌ Erreur de connexion  :', parsed);
        return throwError(() => parsed);
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
    const isPublic = publicRoutes.includes(route) || publicRoutes.includes(normalized);
    this.logger.log(`🔍 Vérification isPublicRoute: route=${route}, normalized=${normalized}, isPublic=${isPublic}`);
    return isPublic;
  }

  refreshToken(): Observable<LoginResponse> {
    this.logger.log('🔄 Tentative de rafraîchissement du token...');
    this.logger.log('Cookies actuels:', document.cookie);
    this.logger.log('recentLogout:', this.recentLogout);
    this.logger.log('isAuthenticated:', this.isAuthenticated$.value);

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

    // Supprimer la restriction de visibilité pour l'initialisation
    if (document.visibilityState !== 'visible' && !this.authInitInProgress) {
      this.logger.log('🚫 Onglet non actif — report du rafraîchissement');
      return EMPTY;
    }

    const attemptRefresh = (retryCount = 0, maxRetries = 2): Observable<LoginResponse> => {
      const startTime = Date.now();
      this.logger.log(`🔄 Envoi de la requête /refresh, tentative ${retryCount + 1}/${maxRetries}`);
      return this.authHttp.post<ApiResponse<LoginResponse>>(
        this.refreshUrl,
        {},
        { withCredentials: true }
      ).pipe(
        tap(() => this.logger.log(`🔄 Requête /refresh terminée en ${Date.now() - startTime}ms`)),
        map(response => {
          if (!response.data?.accessToken) {
            this.logger.warn('❌ Aucun token d’accès retourné');
            throw new Error('Aucun token d’accès retourné');
          }
          return response.data;
        }),
        tap(loginResponse => {
          this.logger.log('✅ Token rafraîchi avec succès:', loginResponse);
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
            cookies: document.cookie,
            retryCount
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
              this.clearAuthState(false, false);
              return throwError(() => ({
                key: 'NO_REFRESH_TOKEN',
                message: 'Aucune session active'
              }));
            }
            this.clearAuthState(false, false);
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
    this.logger.log('📴 Tentative de déconnexion...');
    this.logger.log('Cookies actuels:', document.cookie);
    return this.authHttp.post<void>(this.logoutUrl, {}, { withCredentials: true }).pipe(
      tap(() => {
        this.logger.log('🔁 Déconnexion effectuée');
        this.recentLogout = true;
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

    this.userService.loadUser();
    this.logger.log('📤 Chargement des données utilisateur déclenché');

    this.recentLogout = false;
    this.logger.log('🗑️ recentLogout réinitialisé');

    this.scheduleTokenRefresh(response.accessToken);

    this.logger.log('✅ Connexion réussie, cookies actuels :', document.cookie);
    this.logger.log('Rôle:', role, 'Sub-rôle:', subRole);
    this.logger.log('sendBroadcast:', sendBroadcast, 'suppressRedirectOnce:', this.suppressRedirectOnce);

    if (sendBroadcast) {
      this.logger.log('📡 Diffusion de l’événement login avec token:', response.accessToken);
      this.broadcastChannel.postMessage({ type: 'login', token: response.accessToken });
    }
    this.authReady$.next(true);
    this.suppressRedirectOnce = true;

    const currentUrlTree = this.router.parseUrl(this.router.url);
    const returnUrl = currentUrlTree.queryParams['returnUrl'] || this.pendingRoute;

    this.logger.log('Current route:', this.router.url, 'Return URL:', returnUrl, 'Pending route:', this.pendingRoute);

    const currentRoute = this.router.url.split('?')[0];
    const normalizedRoute = currentRoute.replace(/\/[^\/]+$/, '/:section').replace(/\/[^\/]+$/, '/:roleName');
    this.logger.log('🔍 Route actuelle:', currentRoute, 'Normalisée:', normalizedRoute, 'isPublic:', this.isPublicRoute(currentRoute, normalizedRoute));

    if (returnUrl) {
      this.logger.log('🔁 Redirection vers returnUrl :', returnUrl);
      this.router.navigateByUrl(returnUrl).then(success => {
        this.logger.log(success ? '✅ Navigation réussie vers returnUrl' : '⚠️ Échec de navigation vers returnUrl');
      }).catch(err => this.logger.error('❌ Erreur de navigation vers returnUrl:', err));
      this.pendingRoute = null;
      return;
    }

    if (currentRoute === '/confirm-email' && this.router.url.includes('token')) {
      this.logger.log('ℹ️ Route confirm-email avec token détectée, pas de redirection');
      this.pendingRoute = null;
      return;
    }

    if (currentRoute === '/login') {
      this.logger.log('🔁 Connexion depuis /login, redirection vers la page d’accueil pour le rôle :', role);
      this.redirectToHome(role);
      this.pendingRoute = null;
      return;
    }

    if (this.isPublicRoute(currentRoute, normalizedRoute) && currentRoute !== '/' && currentRoute !== '/login') {
      this.logger.log('ℹ️ Rechargement ou rafraîchissement sur une route publique, pas de redirection :', currentRoute);
      this.pendingRoute = null;
      return;
    }

    if (!sendBroadcast && this.isPublicRoute(currentRoute, normalizedRoute)) {
      this.logger.log('🔁 Synchronisation via BroadcastChannel, redirection vers la page d’accueil pour le rôle :', role);
      this.redirectToHome(role);
      this.pendingRoute = null;
      return;
    }

    this.logger.log('🔁 Redirection vers la page d’accueil pour le rôle :', role);
    this.redirectToHome(role);
    this.pendingRoute = null;
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
    }
  }



  clearAuthState(shouldRedirect: boolean = false, broadcastLogout: boolean = true): void {
    if (this.refreshTokenTimeout) {
      clearTimeout(this.refreshTokenTimeout);
      this.refreshTokenTimeout = null;
    }
    this.isAuthenticated$.next(false);
    this.currentRole$.next(null);
    this.currentSubRole$.next(null);
    this.accessToken = null;
    this.pendingRoute = null;
    this.pendingLogin = null;

    this.logger.log('🔒 Auth state cleared', { shouldRedirect, broadcastLogout, currentPath: this.router.url });

    if (shouldRedirect && !this.suppressRedirectOnce && !this.authInitInProgress) {
      const currentRoute = this.router.url.split('?')[0];
      const normalizedRoute = currentRoute.replace(/\/[^\/]+$/, '/:section').replace(/\/[^\/]+$/, '/:roleName');
      if (!this.isPublicRoute(currentRoute, normalizedRoute)) {
        this.logger.log('🚪 Redirection immédiate vers /login depuis la route :', currentRoute);
        this.router.navigate(['/login'], { queryParams: { returnUrl: currentRoute } })
          .catch(err => this.logger.error('Navigation error:', err));
      } else {
        this.logger.log('ℹ️ Déjà sur une route publique, pas de redirection', { currentRoute, normalizedRoute });
      }
    } else {
      this.suppressRedirectOnce = false;
      this.logger.log('🚫 Redirection supprimée :', { suppressRedirectOnce: this.suppressRedirectOnce, authInitInProgress: this.authInitInProgress });
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
    this.logger.log('🚀 Tentative de redirection vers:', route, 'pour le rôle:', role);
    this.router.navigate([route]).then(success => {
      this.logger.log(success ? '✅ Navigation réussie vers:' : '⚠️ Échec de navigation vers:', route);
    }).catch(err => this.logger.error('❌ Erreur de navigation:', err));
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

  private shouldAllowRefresh(): boolean {
    const token = this.getToken();

    // 1. Si token local encore valide, pas besoin de refresh
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.logger.log('✅ Token local encore valide, pas besoin de refresh');
      return false;
    }

    // 2. Si token expiré mais utilisateur connu comme connecté en mémoire → oui
    if (this.isAuthenticated$.value === true) {
      this.logger.log('ℹ️ Token expiré mais utilisateur marqué comme connecté, refresh autorisé');
      return true;
    }

    // 3. Sinon (aucun token, pas d’état mémoire) → refuse
    this.logger.log('🚫 Refresh refusé — pas d’état utilisateur connecté');
    return false;
  }

}
