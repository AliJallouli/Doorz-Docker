import { Component, EventEmitter, Output, ChangeDetectorRef, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SelectorFormSearchComponent } from './forms/selector-form-search/selector-form-search.component';
import { GlobalSearchComponent } from './global-search/global-search.component';
import { SelectorResultSearchComponent } from './results/selector-result-search/selector-result-search.component';
import { SearchMemoryService } from '../../core/services/search/search-memory.service';
import { SearchContext, SearchContextService } from '../../core/services/search/search-context.service';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    SelectorFormSearchComponent,
    GlobalSearchComponent,
    SelectorResultSearchComponent,
  ],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {
  activePanel: number = -1;
  searchTitle: string = '';
  searchType: string = '';
  menuType: string = '';
  searchResults: any[] = [];
  isPanelOpen: boolean = true;

  @Output() panelChange = new EventEmitter<number>();
  @Output() searchResultsChange = new EventEmitter<any[]>();
  @Output() searchTitleChange = new EventEmitter<string>();

  private routeSubscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private searchMemory: SearchMemoryService,
    private searchContext: SearchContextService
  ) {}

  ngOnInit(): void {
    this.routeSubscription = this.route.params.subscribe(params => {
      const section = params['section'];
      const urlSegments = this.route.snapshot.url;
      this.menuType = urlSegments[0]?.path || '';

      console.log('SearchComponent: menuType=', this.menuType, 'section=', section);

      if (this.menuType === 'student' || this.menuType === 'student-home') {
        const context = this.searchContext.getStudentContext(section || 'jobs');
        this.searchType = context.searchType;
        this.searchTitle = context.searchTitle;
        this.activePanel = context.activePanel;

        // Émettre le titre pour que le parent puisse l'utiliser si nécessaire
        this.searchTitleChange.emit(this.searchTitle);

        // Charger les résultats depuis le cache si disponibles
        if (this.searchMemory.hasResults(this.searchType)) {
          this.searchResults = this.searchMemory.getResults(this.searchType);
          this.searchResultsChange.emit(this.searchResults);
        }
      } else if (this.menuType === 'professionnel') {
        const context = this.searchContext.getProfessionalContext(section);
        this.searchType = context.searchType;
        this.searchTitle = context.searchTitle;
        this.activePanel = context.activePanel;

        // Émettre le titre pour que le parent puisse l'utiliser si nécessaire
        this.searchTitleChange.emit(this.searchTitle);

        // Charger les résultats depuis le cache si disponibles
        if (this.searchMemory.hasResults(this.searchType)) {
          this.searchResults = this.searchMemory.getResults(this.searchType);
          this.searchResultsChange.emit(this.searchResults);
        }
      } else {
        // Cas par défaut : aucun contexte de recherche actif
        this.searchType = 'none';
        this.searchTitle = '';
        this.activePanel = -1;
        this.searchResults = [];
        this.searchTitleChange.emit(this.searchTitle);
        this.searchResultsChange.emit(this.searchResults);
      }

      console.log('SearchComponent: activePanel=', this.activePanel);
      this.cdr.detectChanges();
    });
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  onGlobalSearchResults(results: any[]): void {
    this.searchResults = results;
    this.searchResultsChange.emit(results);
    this.searchMemory.setResults(this.searchType, results);
    this.cdr.detectChanges();
  }

  onPanelChange(event: number): void {
    this.activePanel = event;
    this.panelChange.emit(event);
    this.cdr.detectChanges();
  }

  onSearchResults(results: any[]): void {
    this.searchResults = results;
    this.searchResultsChange.emit(results);
    this.searchMemory.setResults(this.searchType, results);
    this.cdr.detectChanges();
  }
}
