import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map, switchMap, delay } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import {LoggerService} from '../services/logger/logger.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router,private logger: LoggerService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> {
    this.logger.log('🔐 AuthGuard : attente que authState soit prêt');

    return this.authService.authStateReady$.pipe(
      // ne continue que quand authStateReady est true
      switchMap((ready) => {
        if (!ready) {
          this.logger.log('⏳ AuthState pas encore prêt, on attend...');
          return this.authService.authStateReady$.pipe(
            // ✅ on ne prend que le premier TRUE
            map(r => r),
            switchMap((finalReady) => {
              if (!finalReady) {
                this.logger.warn('⛔ AuthState toujours pas prêt, fallback vers login');
                return of(this.router.createUrlTree(['/login'], {
                  queryParams: { returnUrl: state.url }
                }));
              }
              return this.evaluateAuthState(state);
            })
          );
        }

        return this.evaluateAuthState(state);
      })
    );
  }

  private evaluateAuthState(state: RouterStateSnapshot): Observable<boolean | UrlTree> {
    if (this.authService.isAuthenticatedSync()) {
      this.logger.log('✅ AuthGuard : utilisateur authentifié localement');
      return of(true);
    }

    this.logger.log('🔍 AuthGuard : tentative de récupération de session');

    return this.authService.checkForSession().pipe(
      map((isAuthenticated) => {
        if (isAuthenticated) {
          this.logger.log('✅ AuthGuard : session backend active');
          return true;
        }

        this.logger.log('🚪 Redirection vers login avec returnUrl =', state.url);
        return this.router.createUrlTree(['/login'], {
          queryParams: { returnUrl: state.url }
        });
      }),
      catchError((err) => {
        this.logger.warn('❌ AuthGuard : erreur session, redirection', err);
        return of(this.router.createUrlTree(['/login'], {
          queryParams: { returnUrl: state.url }
        }));
      })
    );
  }


}
