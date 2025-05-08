import { Component, OnInit, ChangeDetectorRef, HostListener } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Router, RouterLink } from '@angular/router';
import { ProfileDropdownComponent } from '../profile-dropdown/profile-dropdown.component';
import { NgIf, NgFor, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../../../core/services/language/language.service';
import {LoggerService} from '../../../core/services/logger/logger.service';

interface MenuItem {
  label: string;
  link?: string;
  subMenu?: MenuItem[];
}

interface Language {
  code: string;
  display: string;
}

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [ProfileDropdownComponent, NgIf, NgFor, RouterLink, FormsModule, TranslateModule, NgClass],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isAuthenticated = false;
  currentRole: string | null = null;
  currentSubRole: string | null = null;
  currentLang: string = 'fr';
  requestor: string = 'student';
  screenWidth: number = window.innerWidth;
  isNavbarOpen: boolean = false;
  isPublishDropdownOpen: boolean = false;
  isClosingMenu: boolean = false; // Pour gérer l'animation de la croix

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.screenWidth = window.innerWidth;
    if (this.screenWidth >= 992 && this.isNavbarOpen) {
      this.isNavbarOpen = false; // Fermer le menu en mode bureau uniquement si ouvert
      this.isPublishDropdownOpen = false;
      this.isClosingMenu = false;
      this.cdr.detectChanges();
    }
  }

  isWiderThan394px(): boolean {
    return this.screenWidth >= 394;
  }

  languages: Language[] = [
    { code: 'fr', display: 'Fr' },
    { code: 'nl', display: 'Nl' },
    { code: 'en', display: 'En' },
    { code: 'de', display: 'De' },
  ];

  publicStudentMenu: MenuItem[] = [
    { label: 'NAV.STUDENTJOB', link: 'student/jobs' },
    { label: 'NAV.INTERSHIP', link: 'student/internship' },
    { label: 'NAV.STUDIES', link: 'student/studies' },
    { label: 'NAV.AIDES', link: 'student/aid' },
    { label: 'NAV.EVENTS', link: 'student/events' },
  ];

  publicProfessionalMenu: MenuItem[] = [
    { label: 'NAV.RECRUITER', link: 'professionnel/recruiter' },
    { label: 'NAV.INSTITUTIONS', link: 'professionnel/institutions' },
    { label: 'NAV.ORGANISATIONS', link: 'professionnel/organisations' },
    { label: 'NAV.ASSOCIATIONS', link: 'professionnel/associations' },
    { label: 'NAV.MOUVEMENT', link: 'professionnel/mouvement' },
  ];

  studentMenu: MenuItem[] = [
    { label: 'NAV.STUDENT.DASHBOARD', link: '/student-home' },
    { label: 'NAV.STUDENTJOB', link: '/student-home/jobs' },
    { label: 'NAV.INTERSHIP', link: '/student-home/internship' },
    { label: 'NAV.STUDIES', link: '/student-home/studies' },
    { label: 'NAV.AIDES', link: '/student-home/aid' },
    { label: 'NAV.EVENTS', link: '/student-home/events' },
  ];

  professionalMenu: MenuItem[] = [];

  publishSubMenu: MenuItem[] = [
    { label: 'NAV.STUDENTJOB', link: '/publish/job' },
    { label: 'NAV.INTERSHIP', link: '/publish/internship' },
    { label: 'NAV.EVENTS', link: '/publish/event' },
  ];

  superadminMenu: MenuItem[] = [
    { label: 'NAV.SUPERADMIN.DASHBOARD', link: '/superadmin-home' },
    { label: 'NAV.SUPERADMIN.INVITE', link: '/superadmin/invitations' },
  ];

  constructor(
    private authService: AuthService,
    private router: Router,
    private langService: LanguageService,
    private translateService: TranslateService,
    private cdr: ChangeDetectorRef,
    private readonly logger: LoggerService
  ) {}

  ngOnInit(): void {
    // Authentification
    this.authService.isLoggedIn().subscribe((isAuth) => {
      this.isAuthenticated = isAuth;
      this.cdr.detectChanges();
    });

    // Rôle et sous-rôle
    this.authService.getRole().subscribe((role) => {
      this.currentRole = role;
      this.updateProfessionalMenu();
      this.cdr.detectChanges();
    });

    this.authService.getSubRole().subscribe((subRole) => {
      this.currentSubRole = subRole;
      this.cdr.detectChanges();
    });

    // Initialiser requestor depuis localStorage
    const storedRequestor = localStorage.getItem('requestor');
    this.requestor = storedRequestor || 'student';

    // Synchroniser requestor avec l'URL actuelle
    const currentUrl = this.router.url;
    if (currentUrl.startsWith('/student') || currentUrl.includes('/student/')) {
      this.requestor = 'student';
      localStorage.setItem('requestor', this.requestor);
    } else if (currentUrl.startsWith('/professionnel') || currentUrl.includes('/professionnel/')) {
      this.requestor = 'professionnel';
      localStorage.setItem('requestor', this.requestor);
    }

    // Initialiser la langue
    this.currentLang = this.langService.getLanguage();
    this.translateService.use(this.currentLang);
    this.langService.currentLang$.subscribe((lang) => {
      this.currentLang = lang;
      this.translateService.use(lang);
      this.cdr.detectChanges();
    });
  }

  navigateToHome(): void {
    let targetRoute = '/';
    if (this.requestor === 'student') {
      targetRoute = '/student';
    } else if (this.requestor === 'professionnel') {
      targetRoute = '/professionnel';
    } else {
      const currentUrl = this.router.url;
      if (currentUrl.startsWith('/student') || currentUrl.includes('/student/')) {
        targetRoute = '/student';
      } else if (currentUrl.startsWith('/professionnel') || currentUrl.includes('/professionnel/')) {
        targetRoute = '/professionnel';
      }
    }
    this.router.navigate([targetRoute]);
    this.closeNavbar();
  }

  isProfessionalRole(): boolean {
    return ['company', 'institution', 'association', 'public_organisation', 'landlord'].includes(this.currentRole || '');
  }

  updateProfessionalMenu(): void {
    const publishMenu: MenuItem = {
      label: 'NAV.PUBLISH',
      subMenu: this.publishSubMenu,
    };

    if (this.currentRole === 'company') {
      this.professionalMenu = [
        { label: 'NAV.COMPANY.HOME', link: '/company-home' },
        publishMenu,
      ];
    } else if (this.currentRole === 'institution') {
      this.professionalMenu = [
        { label: 'NAV.INSTITUTION.HOME', link: '/institution-home' },
        publishMenu,
      ];
    } else if (this.currentRole === 'association') {
      this.professionalMenu = [
        { label: 'NAV.ASSOCIATION.HOME', link: '/association-home' },
        publishMenu,
      ];
    } else if (this.currentRole === 'public_organisation') {
      this.professionalMenu = [
        { label: 'NAV.PUBLIC_ORGANISATION.HOME', link: '/public-organisation-home' },
        publishMenu,
      ];
    } else if (this.currentRole === 'landlord' && this.currentSubRole === 'privateLandlord') {
      this.professionalMenu = [
        { label: 'NAV.LANDLORD.DASHBOARD', link: '/landlord-home' },
        publishMenu,
      ];
    } else {
      this.professionalMenu = [];
    }
  }

  getAdminInviteLink(): string {
    switch (this.currentRole) {
      case 'company':
        return '/company/invitations';
      case 'institution':
        return '/institution/invitations';
      case 'association':
        return '/association/invitations';
      case 'public_organisation':
        return '/public-organisation/invitations';
      default:
        return '#';
    }
  }

  redirectToLogin(): void {
    this.router.navigate(['/login']);
    this.closeNavbar();
  }

  clickProfessionel(): void {
    this.requestor = 'professionnel';
    localStorage.setItem('requestor', this.requestor);
    this.closeNavbar();
  }

  clickStudent(): void {
    this.requestor = 'student';
    localStorage.setItem('requestor', this.requestor);
    this.closeNavbar();
  }

  changeLang(lang: string, event?: Event): void {
    event?.preventDefault();
    localStorage.setItem('lang', lang);
    this.currentLang = lang;
    this.langService.setLanguage(lang);
    this.translateService.use(lang);
  }

  toggleNavbar(): void {
    this.isNavbarOpen = !this.isNavbarOpen;
    if (!this.isNavbarOpen) {
      this.isClosingMenu = true;
      this.isPublishDropdownOpen = false;
      setTimeout(() => {
        this.isClosingMenu = false;
        this.cdr.detectChanges();
      }, 200); // Délai pour l'animation (0.2s)
    } else {
      this.isClosingMenu = false;
    }
    this.cdr.detectChanges();
  }

  closeNavbar(): void {
    if (this.isNavbarOpen) {
      this.isNavbarOpen = false;
      this.isClosingMenu = true;
      this.isPublishDropdownOpen = false;
      setTimeout(() => {
        this.isClosingMenu = false;
        this.cdr.detectChanges();
      }, 200); // Délai pour l'animation
    }
    this.cdr.detectChanges();
  }

  onNavbarClosed(): void {
    this.closeNavbar(); // Appeler la méthode closeNavbar() lorsque l'événement est reçu
    this.logger.log('📤 Navbar fermée via événement depuis ProfileDropdown');
  }

  togglePublishDropdown(event: Event): void {
    if (this.screenWidth < 992) {
      event.preventDefault();
      this.isPublishDropdownOpen = !this.isPublishDropdownOpen;
      this.cdr.detectChanges();
    }
  }

  getCurrentLanguageDisplay(): string {
    const currentLanguage = this.languages.find(lang => lang.code === this.currentLang);
    return currentLanguage ? currentLanguage.display : this.languages[0].display;
  }
}
