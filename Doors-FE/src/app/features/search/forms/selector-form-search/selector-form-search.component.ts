import {ChangeDetectorRef, Component, EventEmitter, HostListener, Input, OnInit, Output} from '@angular/core';
import {NgClass, NgForOf, NgIf, NgSwitch, NgSwitchCase} from '@angular/common';
import {AccordionIconComponent} from '../../../../shared/icons/accordion-icon/accordion-icon.component';
import {StudentJobSearchComponent} from '../student-job-search/student-job-search.component';
import {InternshipSearchComponent} from '../internship-search/internship-search.component';
import {StudySearchComponent} from '../study-search/study-search.component';
import {EventSearchComponent} from '../event-search/event-search.component';
import {ActivatedRoute} from '@angular/router';
import {TranslateModule} from '@ngx-translate/core';
import {SEARCH_ITEMS} from '../../../../core/constants/search-item';
import {AidSearchComponent} from '../aid-search/aid-search.component';
import {KotSearchComponent} from '../kot-search/kot-search.component';

@Component({
  selector: 'app-selector-form-search',
  imports: [
    NgIf,
    NgClass,
    AccordionIconComponent,
    StudentJobSearchComponent,
    InternshipSearchComponent,
    StudySearchComponent,
    EventSearchComponent,
    TranslateModule,
    NgSwitch,
    NgSwitchCase,
    NgForOf,
    AidSearchComponent,
    KotSearchComponent
  ],
  templateUrl: './selector-form-search.component.html',
  styleUrl: './selector-form-search.component.css'
})
export class SelectorFormSearchComponent implements OnInit {

  @Input() activePanel: number = -1;
  @Input() isPanelVisible: boolean = true;

  @Input() searchTitle: string ='';
  @Input() searchType: string='';
  @Output() panelChange = new EventEmitter<number>();
  @Output() searchResultsChange = new EventEmitter<any[]>();
  searchResults: any[] = [];
  isPanelOpen: boolean = true;
  searchItems = SEARCH_ITEMS;
  hasUserInteracted = false;


  constructor(private route: ActivatedRoute, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.setInitialPanelVisibility(); // 👈 Appelé dès le début
  }

  togglePanel(index: number): void {
    this.hasUserInteracted = true;

    if (this.activePanel === index) {
      this.isPanelOpen = !this.isPanelOpen;
    } else {
      this.activePanel = index;
      this.isPanelOpen = true;
      this.panelChange.emit(this.activePanel);
    }

    this.cdr.detectChanges();
  }


  onSearchResults(results: any[]): void {
    this.searchResults = results;
    this.searchResultsChange.emit(results);
    this.cdr.detectChanges();
  }
  @HostListener('window:resize')
  onResize(): void {
    if (!this.hasUserInteracted) {
      this.setInitialPanelVisibility();
    }
  }

  setInitialPanelVisibility(): void {
    this.isPanelOpen = window.innerWidth > 574;
  }
}
