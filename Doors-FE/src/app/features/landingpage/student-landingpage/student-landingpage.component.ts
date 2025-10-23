import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentJobResultsComponent } from '../../search/results/student-job-results/student-job-results.component';
import { InternshipResultsComponent } from '../../search/results/internship-results/internship-results.component';
import { StudyResultsComponent } from '../../search/results/study-results/study-results.component';
import { EventResultsComponent } from '../../search/results/event-results/event-results.component';
import {AidResult, EventResult, InternshipResult, JobResult, StudyResult} from '../../../core/models/search.models';
import {JobSearchStrategy} from '../../../core/services/search/strategies/job-search.strategy';
import {InternshipSearchStrategy} from '../../../core/services/search/strategies/internship-search.strategy';
import {StudySearchStrategy} from '../../../core/services/search/strategies/study-search-strategy';
import {AidSearchStrategy} from '../../../core/services/search/strategies/aid-search.strategy';
import {EventSearchStrategy} from '../../../core/services/search/strategies/event-search.strategy';
import {AidResultsComponent} from '../../search/results/aid-results/aid-results.component';
import {Router} from '@angular/router';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-student-landingpage',
  standalone: true,
  imports: [
    CommonModule,
    StudentJobResultsComponent,
    InternshipResultsComponent,
    StudyResultsComponent,
    EventResultsComponent,
    AidResultsComponent,
    TranslateModule
  ],
  templateUrl: './student-landingpage.component.html',
  styleUrls: ['./student-landingpage.component.css']
})
export class StudentLandingpageComponent implements OnInit {
  studentJobsResult: JobResult[] = [];
  internshipsResult: InternshipResult[] = [];
  studiesResult: StudyResult[] = [];
  aidsResult: AidResult[] = [];
  eventsResult: EventResult[] = [];

  constructor(
    private jobSearchStrategy: JobSearchStrategy,
    private internshipSearchStrategy: InternshipSearchStrategy,
    private studySearchStrategy: StudySearchStrategy,
    private aidSearchStrategy: AidSearchStrategy,
    private eventSearchStrategy: EventSearchStrategy,
    private router: Router
  ) {}

  ngOnInit(): void {
    const emptyQuery = { keywords: '', location: '' };

    this.jobSearchStrategy.search(emptyQuery).subscribe(results => {
      this.studentJobsResult = results.slice(0, 6);
    });

    this.internshipSearchStrategy.search(emptyQuery).subscribe(results => {
      this.internshipsResult = results.slice(0, 6);
    });

    this.studySearchStrategy.search(emptyQuery).subscribe(results => {
      this.studiesResult = results.slice(0, 6);
    });

    this.aidSearchStrategy.search(emptyQuery).subscribe(results => {
      this.aidsResult = results.slice(0, 6);
    });

    this.eventSearchStrategy.search(emptyQuery).subscribe(results => {
      this.eventsResult = results.slice(0, 6);
    });
  }
  // Méthode pour gérer les clics sur les titres
  onTitleClick(category: string): void {
    // Rediriger vers une URL basée sur la catégorie
    this.router.navigate([`/student/${category}`]);
  }
}
