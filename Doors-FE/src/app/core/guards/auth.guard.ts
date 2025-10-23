import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map, switchMap, delay } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import {LoggerService} from '../services/logger/logger.service';
import {Role} from '../services/invitation/invitation.service';
import {RoleName} from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private logger: LoggerService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> {
    this.logger.log('🔐 AuthGuard : vérification pour la route', state.url);
    this.logger.log('isAuthenticatedSync:', this.authService.isAuthenticatedSync());
    this.logger.log('Current router URL:', this.router.url);
    this.logger.log('Query params:', route.queryParams);

    this.authService.setPendingRoute(state.url);

    return this.authService.authStateReady$.pipe(
      switchMap((ready) => {
        if (!ready) {
          this.logger.log('⏳ AuthState pas prêt, attente pour :', state.url);
          return this.authService.authStateReady$.pipe(
            map(r => r),
            switchMap((finalReady) => {
              if (!finalReady) {
                this.logger.warn('⛔ AuthState toujours pas prêt, redirection vers login');
                // Inclure les paramètres de requête dans returnUrl
                const queryParams = { ...route.queryParams, returnUrl: state.url };
                this.logger.log('Redirection vers /login avec queryParams:', queryParams);
                return of(this.router.createUrlTree(['/login'], { queryParams }));
              }
              return this.evaluateAuthState(state, route);
            })
          );
        }
        return this.evaluateAuthState(state, route);
      })
    );
  }

  private evaluateAuthState(state: RouterStateSnapshot, route: ActivatedRouteSnapshot): Observable<boolean | UrlTree> {
    if (this.authService.isAuthenticatedSync()) {
      this.logger.log('✅ AuthGuard : utilisateur authentifié localement pour :', state.url);
      if (state.url === '/') {
        const role = this.authService.getCurrentRoleSync();
        if (role) {
          this.logger.log('🔁 Redirection vers la page d’accueil pour le rôle :', role);
          const routes: Record<RoleName, string> = {
            student: '/student-home',
            landlord: '/landlord-home',
            company: '/company-home',
            institution: '/institution-home',
            superadmin: '/superadmin-home'
          };
          const route = routes[role] || '/';
          return of(this.router.createUrlTree([route]));
        }
      }
      return of(true);
    }

    this.logger.log('🔍 AuthGuard : vérification de la session backend pour :', state.url);
    return this.authService.checkForSession().pipe(
      map((isAuthenticated) => {
        if (isAuthenticated) {
          this.logger.log('✅ AuthGuard : session backend active pour :', state.url);
          if (state.url === '/') {
            const role = this.authService.getCurrentRoleSync();
            if (role) {
              this.logger.log('🔁 Redirection vers la page d’accueil pour le rôle :', role);
              const routes: Record<RoleName, string> = {
                student: '/student-home',
                landlord: '/landlord-home',
                company: '/company-home',
                institution: '/institution-home',
                superadmin: '/superadmin-home'
              };
              const route = routes[role] || '/';
              return this.router.createUrlTree([route]);
            }
          }
          return true;
        }

        this.logger.log('🚪 Redirection vers login avec returnUrl =', state.url);
        // Inclure les paramètres de requête
        const queryParams = { ...route.queryParams, returnUrl: state.url };
        this.logger.log('Redirection vers /login avec queryParams:', queryParams);
        return this.router.createUrlTree(['/login'], { queryParams });
      }),
      catchError((err) => {
        this.logger.warn('❌ AuthGuard : erreur lors de la vérification pour :', state.url, err);
        const queryParams = { ...route.queryParams, returnUrl: state.url };
        this.logger.log('Redirection vers /login avec queryParams:', queryParams);
        return of(this.router.createUrlTree(['/login'], { queryParams }));
      })
    );
  }
}
