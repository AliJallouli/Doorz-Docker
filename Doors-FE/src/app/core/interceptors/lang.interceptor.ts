import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LanguageService } from '../services/language/language.service';

export const langInterceptor: HttpInterceptorFn = (req, next) => {
  const lang = inject(LanguageService).getLanguage();

  const modified = req.clone({
    setHeaders: {
      'Accept-Language': lang
    }
  });

  return next(modified);
};
