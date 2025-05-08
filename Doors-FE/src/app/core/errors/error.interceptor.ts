import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const apiError = error.error?.error || error.error;

      const errorKey = apiError?.key || 'INTERNAL_SERVER_ERROR';
      const field = apiError?.field || null;
      const extraData = apiError?.extraData || apiError?.extensions?.extraData || null;

      console.error('HTTP Error:', {
        url: req.url,
        status: error.status,
        errorKey,
        field,
        extraData
      });

      return throwError(() => ({
        key: errorKey,
        message: errorKey,
        field,
        httpStatus: error.status,
        extraData // ✅ maintenant c’est bien transmis au frontend
      }));
    })
  );
};
