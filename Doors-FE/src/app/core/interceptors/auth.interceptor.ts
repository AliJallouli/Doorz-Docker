import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { catchError, filter, switchMap, take, throwError, of, BehaviorSubject } from 'rxjs';
import {LoggerService} from '../services/logger/logger.service';

const isRefreshing$ = new BehaviorSubject<boolean>(false);
const refreshTokenSubject$ = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  const authService = inject(AuthService);
  const token = authService.getToken();
  const isLogoutRequest = req.url.includes('/auth/logout');
  const logger = inject(LoggerService);

  logger.log('Interceptor - URL:', req.url);
  logger.log('Interceptor - Token disponible:', token ? 'Oui' : 'Non');

  const clonedReq = token && !isLogoutRequest
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(clonedReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status !== 401 || isLogoutRequest) {
        return throwError(() => error);
      }

      if (!isRefreshing$.value) {
        isRefreshing$.next(true);
        refreshTokenSubject$.next(null);

        return authService.refreshToken().pipe(
          switchMap(res => {
            refreshTokenSubject$.next(res.accessToken);
            return next(
              req.clone({ setHeaders: { Authorization: `Bearer ${res.accessToken}` } })
            );
          }),
          catchError(err => {
            authService.clearAuthState(true);
            return throwError(() => err);
          }),
          // Toujours remettre à false
          switchMap(response => {
            isRefreshing$.next(false);
            return of(response);
          })
        );
      } else {
        return refreshTokenSubject$.pipe(
          filter(token => token !== null),
          take(1),
          switchMap(newToken => {
            return next(
              req.clone({ setHeaders: { Authorization: `Bearer ${newToken!}` } })
            );
          })
        );
      }
    })
  );
};
