import { Component, OnInit, ChangeDetectorRef, OnDestroy, HostListener } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { StudentJobSearchComponent } from '../search/student-job-search/student-job-search.component';
import { InternshipSearchComponent } from '../search/internship-search/internship-search.component';
import { StudySearchComponent } from '../search/study-search/study-search.component';
import { EventSearchComponent } from '../search/event-search/event-search.component';
import { StudentNeedsSurveyComponent } from '../survey/student-needs-survey/student-needs-survey.component';
import { CallToActionComponent } from '../call-to-action/call-to-action.component';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import {GlobalSearchComponent} from '../search/global-search/global-search.component';

@Component({
  selector: 'app-landingpage',
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    StudentJobSearchComponent,
    InternshipSearchComponent,
    StudySearchComponent,
    EventSearchComponent,
    CallToActionComponent,
    GlobalSearchComponent
  ],
  templateUrl: './landingpage.component.html',
  styleUrls: ['./landingpage.component.css']
})
export class LandingpageComponent implements OnInit, OnDestroy {
  searchResults: any[] = [];
  activePanel: number = -1;
  menuType: string = '';
  isPanelOpen: boolean = true; // 💥 Contrôle l'état visuel du panneau actif
  private routeSubscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.setInitialPanelVisibility(); // 💥 Initialiser l'état visuel

    this.routeSubscription = this.route.params.subscribe(params => {
      const section = params['section'];
      const urlSegments = this.route.snapshot.url;
      this.menuType = urlSegments[0]?.path || '';

      console.log('LandingpageComponent: menuType=', this.menuType, 'section=', section);

      switch (this.menuType) {
        case 'student':
          if (!section) {
            this.activePanel = 0; // Route '/student'
          } else {
            switch (section) {
              case 'jobs': this.activePanel = 0; break;
              case 'internship': this.activePanel = 1; break;
              case 'studies': this.activePanel = 2; break;
              case 'events': this.activePanel = 3; break;
              case 'aid': this.activePanel = 4; break;
              default: this.activePanel = 0;
            }
          }
          break;
        case 'professionnel':
          if (!section) {
            this.activePanel = 10; // Route '/professionnel'
          } else {
            switch (section) {
              case 'recruiter': this.activePanel = 5; break;
              case 'institutions': this.activePanel = 6; break;
              case 'organisations': this.activePanel = 7; break;
              case 'associations': this.activePanel = 8; break;
              case 'mouvement': this.activePanel = 9; break;
              default: this.activePanel = 10;
            }
          }
          break;
        default:
          this.activePanel = 0; // Route racine ('/')
      }

      console.log('LandingpageComponent: activePanel=', this.activePanel);
      this.cdr.detectChanges(); // Forcer la mise à jour du template
    });
  }

  // Détecter le redimensionnement de la fenêtre
  @HostListener('window:resize', ['$event'])
  onResize(event: Event): void {
    this.setInitialPanelVisibility();
  }

  // Définir l'état visuel initial du panneau actif
  setInitialPanelVisibility(): void {
    this.isPanelOpen = window.innerWidth > 574; // 💥 Fermer visuellement sur mobile
    this.cdr.detectChanges();
  }

  togglePanel(index: number): void {
    if (this.activePanel === index) {
      this.isPanelOpen = !this.isPanelOpen; // 💥 Basculer l'état visuel sans changer activePanel
    }
    this.cdr.detectChanges();
  }

  onSearchResults(results: any[]): void {
    this.searchResults = results;
    console.log('Résultats de recherche:', results);
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe(); // Nettoyer l'abonnement
    }
  }
}
