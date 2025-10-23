import { Component, Input } from '@angular/core';
import { NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import { StudentJobResultsComponent } from '../student-job-results/student-job-results.component';
import { InternshipResultsComponent } from '../internship-results/internship-results.component';
import { StudyResultsComponent } from '../study-results/study-results.component';
import { EventResultsComponent } from '../event-results/event-results.component';
import { TranslateModule } from '@ngx-translate/core';
import {AidResultsComponent} from '../aid-results/aid-results.component';
import {KotResultsComponent} from '../kot-results/kot-results.component';

@Component({
  selector: 'app-selector-result-search',
  standalone: true,
  imports: [
    NgSwitch,
    NgSwitchCase,
    StudentJobResultsComponent,
    InternshipResultsComponent,
    StudyResultsComponent,
    EventResultsComponent,
    TranslateModule,
    AidResultsComponent,
    KotResultsComponent
  ],
  templateUrl: './selector-result-search.component.html',
  styleUrls: ['./selector-result-search.component.css']
})
export class SelectorResultSearchComponent {
  @Input() searchResults: any[] = [];
  @Input() searchType!: string;
  @Input() searchTitle: string = '';
}
