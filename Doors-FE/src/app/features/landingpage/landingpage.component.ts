import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import {SearchMemoryService} from '../../core/services/search/search-memory.service';
import {SearchContext, SearchContextService} from '../../core/services/search/search-context.service';
import {StudentLandingpageComponent} from './student-landingpage/student-landingpage.component';
import {ProfessionnelLandingpageComponent} from './professionnel-landingpage/professionnel-landingpage.component';
import {CallToActionComponent} from '../shared/call-to-action/call-to-action.component';

@Component({
  selector: 'app-landingpage',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    CallToActionComponent,
    StudentLandingpageComponent,
    ProfessionnelLandingpageComponent,
    CallToActionComponent
  ],
  templateUrl: './landingpage.component.html',
  styleUrls: ['./landingpage.component.css']
})
export class LandingpageComponent implements OnInit, OnDestroy {
  activePanel: number = -1;
  menuType: string = '';
  searchResults: any[] = [];
  private routeSubscription: Subscription | undefined;
  searchTitle: string='';
  searchType: string='';

  constructor(
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
  private searchMemory: SearchMemoryService,
    private searchContext: SearchContextService,

  ) {}

  ngOnInit(): void {
    this.routeSubscription = this.route.params.subscribe(params => {
      const section = params['section'];
      const urlSegments = this.route.snapshot.url;
      this.menuType = urlSegments[0]?.path || '';

      console.log('LandingpageComponent: menuType=', this.menuType, 'section=', section);

      // Plus besoin de gérer searchType, searchTitle ou activePanel
      this.cdr.detectChanges();
    });
  }

  private getStudentPanelIndex(section: string): number {
    const context = this.searchContext.getStudentContext(section);
    return this.setSearchContext(context);
  }

  private getProfessionalPanelIndex(section: string): number {
    const context = this.searchContext.getProfessionalContext(section);
    return this.setSearchContext(context);
  }


  onPanelChange(panel: number): void {
    this.activePanel = panel;
    this.cdr.detectChanges();
  }

  onSearchResults(results: any[]): void {
    this.searchResults = results;
    this.searchMemory.setResults(this.searchType, results);
    console.log('Résultats de recherche:', results);
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  private setSearchContext(context: SearchContext): number {
    this.searchType = context.searchType;
    this.searchTitle = context.searchTitle;
    console.log('🧠 Récupération depuis cache :', this.searchMemory.getResults(this.searchType));
    if (this.searchMemory.hasResults(this.searchType)) {
      this.searchResults = this.searchMemory.getResults(this.searchType);
    } else {
      this.searchResults = []; // Laisser vide jusqu'à recherche
    }

    return context.activePanel;
  }


}
