import {provideAnimations} from '@angular/platform-browser/animations';

if (
  window.location.protocol === 'http:' &&
  window.location.hostname === 'localhost'
) {
  window.location.href = window.location.href.replace('http:', 'https:');
}


import { bootstrapApplication } from '@angular/platform-browser';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app/app.routes';
import { AuthService } from './app/core/services/auth/auth.service';
import { authInterceptor } from './app/core/interceptors/auth.interceptor';
import { errorInterceptor } from './app/core/errors/error.interceptor';
import { langInterceptor } from './app/core/interceptors/lang.interceptor';
import { TranslateService } from '@ngx-translate/core';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import {LanguageService} from './app/core/services/language/language.service';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './i18n/', '.json');  // Correct chemin relatif
}

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([authInterceptor, errorInterceptor, langInterceptor])
    ),
    provideAnimations(),
    AuthService,
    importProvidersFrom(
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient],
        },
      })
    ),
  ],
}).then(appRef => {
  const injector = appRef.injector;
  const translate = appRef.injector.get(TranslateService);
  const langService = appRef.injector.get(LanguageService);

  // Initialisation synchrone
  translate.setDefaultLang('fr');
  translate.use(langService.getLanguage());
}).catch(err => console.error(err));

// Fonction pour récupérer la route en fonction du rôle
function getRouteForRole(role: string | null): string {
  const routesMap: Record<string, string> = {
    student: '/student-home',
    landlord: '/landlord-home',
    company: '/company-home',
    institution: '/institution-home',
    superadmin: '/superadmin-home',
  };
  return routesMap[role as string] || '/login'; // Fallback vers /login si rôle inconnu
}
