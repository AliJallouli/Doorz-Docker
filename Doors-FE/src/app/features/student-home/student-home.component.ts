import { Component, OnInit, HostListener, ChangeDetectorRef } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute } from '@angular/router';
import { SearchComponent } from '../search/search.component';
import {SearchContextService} from '../../core/services/search/search-context.service';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-student-home',
  standalone: true,
  imports: [
    TranslateModule,
  ],
  templateUrl: './student-home.component.html',
  styleUrls: ['./student-home.component.css']
})
export class StudentHomeComponent implements OnInit {
  searchResults: any[] = [];
  activePanel: number = -1;
  searchTitle: string='';
  searchType: string='';

  constructor(private route: ActivatedRoute, private cdr: ChangeDetectorRef,private searchContext: SearchContextService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const section = params['section'];
      const context = this.searchContext.getStudentContext(section);

      this.searchTitle = context.searchTitle;
      this.activePanel = context.activePanel;
      this.searchType = context.searchType;

      this.cdr.detectChanges();
    });
  }


  onPanelChange(panel: number): void {
    this.activePanel = panel;
  }

  onSearchResults(results: any[]): void {
    this.searchResults = results;
    console.log('Résultats de recherche:', results);
  }
}
