import {ChangeDetectorRef, Component, HostListener, OnInit} from '@angular/core';
import {CallToActionComponent} from '../call-to-action/call-to-action.component';
import {EventSearchComponent} from '../search/event-search/event-search.component';
import {InternshipSearchComponent} from '../search/internship-search/internship-search.component';
import {JsonPipe, NgClass, NgForOf, NgIf} from '@angular/common';
import {StudentJobSearchComponent} from '../search/student-job-search/student-job-search.component';
import {StudentNeedsSurveyComponent} from '../survey/student-needs-survey/student-needs-survey.component';
import {StudySearchComponent} from '../search/study-search/study-search.component';
import {TranslatePipe} from '@ngx-translate/core';
import {ActivatedRoute} from '@angular/router';
import {GlobalSearchComponent} from "../search/global-search/global-search.component";

@Component({
  selector: 'app-student-home',
  imports: [
    EventSearchComponent,
    InternshipSearchComponent,
    JsonPipe,
    NgForOf,
    NgIf,
    StudentJobSearchComponent,
    StudySearchComponent,
    TranslatePipe,
    GlobalSearchComponent,
    NgClass
  ],
  templateUrl: './student-home.component.html',
  styleUrl: './student-home.component.css'
})
export class StudentHomeComponent implements OnInit {
  searchResults: any[] = [];
  activePanel: number = -1; // Aucun panneau affiché par défaut
  isPanelOpen: boolean = true;

  constructor(private route: ActivatedRoute,private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    // Lire le paramètre 'section' depuis les route parameters
    this.route.params.subscribe(params => {
      const section = params['section'];
      switch (section) {
        case 'jobs':
          this.activePanel = 0; // Recherche de job étudiant
          break;
        case 'internship':
          this.activePanel = 1; // Recherche de stage
          break;
        case 'studies':
          this.activePanel = 2; // Recherche d'études
          break;
        case 'events':
          this.activePanel = 3; // Recherche d'événements
          break;
        case 'aid':
          this.activePanel = 4; // Sondage sur les besoins
          break;
        default:
          this.activePanel = 0; // Aucun panneau affiché
      }
    });
  }

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
}
