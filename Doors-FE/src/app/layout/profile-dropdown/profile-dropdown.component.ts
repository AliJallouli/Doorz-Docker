import { Component, OnInit, Output, EventEmitter, ChangeDetectorRef, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth.service';
import { UserService } from '../../core/services/user/user.service';
import { User } from '../../core/models/user.model';
import { ApiResponse } from '../../core/models/auth.models';
import { HttpErrorResponse } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { LoggerService } from '../../core/services/logger/logger.service';
import { NgClass } from '@angular/common';
import { Subscription } from 'rxjs';

declare const bootstrap: any;

@Component({
  selector: 'app-profile-dropdown',
  standalone: true,
  imports: [CommonModule, RouterLink, TranslateModule, NgClass],
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.css'],
})
export class ProfileDropdownComponent implements OnInit, OnDestroy {
  @ViewChild('profileDropdown') profileDropdown: ElementRef | undefined;
  @Output() closeLanguageMenu = new EventEmitter<void>();
  @Output() navbarClosed = new EventEmitter<void>();
  isDropdownOpen = false;
  user: User | null = null;
  currentRole: string | null = null;
  currentSubRole: string | null = null;
  private userSubscription: Subscription | undefined;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private logger: LoggerService
  ) {
    this.logger.log('✅ ProfileDropdownComponent CHARGÉ');
  }

  ngOnInit(): void {
    // S'abonner aux mises à jour de l'utilisateur
    this.userSubscription = this.userService.user$.subscribe({
      next: (user: User | null) => {
        this.user = user;
        this.logger.log('👤 Utilisateur mis à jour :', user);
        this.cdr.detectChanges();
      },
      error: (err: Error) => {
        this.logger.error('Erreur lors de la réception de l’utilisateur :', err.message);
      }
    });

    // Charger l'utilisateur initial
    this.userService.getCurrentUser().subscribe({
      next: (response: ApiResponse<User>) => {
        if (response.error) {
          this.logger.warn('⚠️ Erreur chargement user :', response.error);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.logger.error('Erreur HTTP :', err.message);
      }
    });

    this.authService.getRole().subscribe((role) => {
      this.currentRole = role;
      this.logger.log('🎭 Rôle chargé :', role);
      this.cdr.detectChanges();
    });

    this.authService.getSubRole().subscribe((subRole) => {
      this.currentSubRole = subRole;
      this.logger.log('🎭 Sous-rôle chargé :', subRole);
      this.cdr.detectChanges();
    });
  }

  ngOnDestroy(): void {
    // Nettoyer l'abonnement pour éviter les fuites de mémoire
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
      this.logger.log('🧹 Abonnement utilisateur désabonné');
    }
  }

  getInitials(): string {
    const user = this.userService.getCurrentUserSync(); // Utiliser la version synchrone
    if (!user) return '?';
    const f = user.firstName?.[0] ?? '';
    const l = user.lastName?.[0] ?? '';
    return (f + l).toUpperCase() || '?';
  }

  toggleDropdown(event: Event): void {
    event.preventDefault();
    this.isDropdownOpen = !this.isDropdownOpen;
    this.closeNavbar();
    this.closeLanguageMenu.emit();
  }

  private closeNavbar(): void {
    this.navbarClosed.emit();
    this.logger.log('📤 Événement navbarClosed émis');
  }

  goToProfile(): void {
    const role = this.authService.getCurrentRoleSync();
    if (role) {
      this.logger.log('🔁 Redirection vers profil :', role);
      this.router.navigate([`/${role}/settings`]);
      this.isDropdownOpen = false;
      this.cdr.detectChanges();
    } else {
      this.logger.warn('🚫 Rôle non défini, redirection vers /');
      this.router.navigate(['/']);
      this.isDropdownOpen = false;
      this.cdr.detectChanges();
    }
  }

  closeDropdown(): void {
    this.isDropdownOpen = false;
    this.cdr.detectChanges();
  }

  logout(): void {
    this.logger.log('📤 Déconnexion en cours...');
    this.authService.logout().subscribe({
      next: () => {
        this.userService.clearUser();
        this.router.navigate(['/']);
        this.isDropdownOpen = false;
        this.cdr.detectChanges();
      },
      error: (err: HttpErrorResponse) => {
        this.logger.error('Erreur lors de la déconnexion :', err.message);
      }
    });
  }
}
