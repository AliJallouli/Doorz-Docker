import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import {AuthService} from '../services/auth/auth.service';
import {LoggerService} from '../services/logger/logger.service';

@Injectable({
  providedIn: 'root'
})
export class NoAuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private logger: LoggerService) {}

  canActivate(): Observable<boolean> {
    return this.authService.isLoggedIn().pipe(
      take(1),
      map(isAuthenticated => {
        if (isAuthenticated) {
          const role = this.authService.getCurrentRoleSync();
          const redirectRoute = role ? `/${role}-home` : '/';
          this.logger.log('🚪 NoAuthGuard : utilisateur authentifié, redirection vers', redirectRoute);
          this.router.navigate([redirectRoute]);
          return false;
        }
        this.logger.log('✅ NoAuthGuard : accès autorisé, utilisateur non authentifié');
        return true;
      })
    );
  }
}
