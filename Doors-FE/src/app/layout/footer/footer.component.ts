import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { NgFor } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../../core/services/language/language.service';

interface MenuItem {
  label: string;
  link: string;
}

interface Language {
  code: string;
  display: string;
}

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [RouterLink, NgFor, TranslateModule],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent implements OnInit {
  currentYear: number = new Date().getFullYear();
  commonMenu: MenuItem[] = [
    { label: 'NAV.ABOUT', link: '#' },
    { label: 'NAV.FEATURES', link: '#' },
    { label: 'NAV.CONTACT', link: '/contact' },
  ];
  languages: Language[] = [
    { code: 'fr', display: 'Fr' },
    { code: 'nl', display: 'Nl' },
    { code: 'en', display: 'En' },
    { code: 'de', display: 'De' },
  ];

  constructor(
    private router: Router,
    private langService: LanguageService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    // Synchroniser la langue
    const currentLang = this.langService.getLanguage();
    this.translateService.use(currentLang);
    this.langService.currentLang$.subscribe((lang) => {
      this.translateService.use(lang);
    });
  }

  changeLang(lang: string, event: Event): void {
    event.preventDefault();
    localStorage.setItem('lang', lang);
    this.translateService.use(lang);
  }

  scrollToTop(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
