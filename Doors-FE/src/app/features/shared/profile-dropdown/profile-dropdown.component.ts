import { Component, OnInit, ElementRef, ViewChild, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth.service';
import { UserService } from '../../../core/services/user/user.service';
import { User } from '../../../models/user.model';
import { ApiResponse } from '../../../models/auth.models';
import { HttpErrorResponse } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import {LoggerService} from '../../../core/services/logger/logger.service';

// Déclarer Bootstrap comme variable globale (si chargé via CDN)
declare const bootstrap: any;

@Component({
  selector: 'app-profile-dropdown',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule, RouterLink],
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.css']
})
export class ProfileDropdownComponent implements OnInit {
  @ViewChild('profileDropdown') profileDropdown: ElementRef | undefined; // Référence au bouton toggle
  @Output() navbarClosed = new EventEmitter<void>(); // Événement pour notifier la fermeture de la navbar
  user: User | null = null;
  currentRole: string | null = null;
  currentSubRole: string | null = null;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router,
    private readonly logger: LoggerService

  ) {
    this.logger.log('✅ ProfileDropdownComponent CHARGÉ');
  }

  ngOnInit(): void {
    // Récupérer l'utilisateur
    this.userService.getCurrentUser().subscribe({
      next: (response: ApiResponse<User>) => {
        if (response.data) {
          this.user = response.data;
          this.logger.log('👤 Utilisateur chargé :', this.user);
        } else {
          this.logger.warn('⚠️ Erreur chargement user :', response.error);
        }
      },
      error: (err: HttpErrorResponse) => {
        this.logger.error('Erreur HTTP :', err.message);
      }
    });

    // Récupérer le rôle
    this.authService.getRole().subscribe(role => {
      this.currentRole = role;
      this.logger.log('🎭 Rôle chargé :', role);
    });

    // Récupérer le sous-rôle
    this.authService.getSubRole().subscribe(subRole => {
      this.currentSubRole = subRole;
      this.logger.log('🎭 Sous-rôle chargé :', subRole);
    });
  }

  getInitials(): string {
    if (!this.user) return '?';
    const f = this.user.firstName?.[0] ?? '';
    const l = this.user.lastName?.[0] ?? '';
    return (f + l).toUpperCase() || '?';
  }

  goToProfile(): void {
    this.router.navigate(['/profile']);
  }

  logout(): void {
    this.logger.log('📤 Déconnexion en cours...');
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
      }
    });
  }

  toggleDropdown(): void {
    this.closeNavbar(); // Ferme la navbar avant d'ouvrir le menu
    // Bootstrap gère l'ouverture/fermeture du dropdown via data-bs-toggle
  }

  private closeNavbar(): void {
    this.navbarClosed.emit(); // Émettre l'événement pour fermer la navbar
    this.logger.log('📤 Événement navbarClosed émis');
  }

  closeDropdown(): void {
    this.logger.log('Croix cliquée'); // Pour débogage
    if (this.profileDropdown) {
      const dropdownElement = this.profileDropdown.nativeElement as HTMLElement;
      try {
        // Essayer l'API Bootstrap
        if (typeof bootstrap !== 'undefined' && bootstrap.Dropdown) {
          const dropdown = bootstrap.Dropdown.getInstance(dropdownElement);
          if (dropdown) {
            dropdown.hide();
            this.logger.log('Menu fermé via Bootstrap');
            return;
          }
        }
        // Solution de secours : simuler un clic
        dropdownElement.click();
        this.logger.log('Menu fermé via clic simulé');
      } catch (error) {
        this.logger.error('Erreur lors de la fermeture du dropdown :', error);
        // Dernier recours : manipuler le DOM
        dropdownElement.classList.remove('show');
        dropdownElement.setAttribute('aria-expanded', 'false');
        const dropdownMenu = dropdownElement.nextElementSibling as HTMLElement;
        if (dropdownMenu) {
          dropdownMenu.classList.remove('show');
        }
        this.logger.log('Menu fermé via manipulation DOM');
      }
    } else {
      this.logger.warn('profileDropdown non trouvé');
    }
  }
}
