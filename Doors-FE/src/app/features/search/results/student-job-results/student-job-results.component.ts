import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CurrencyPipe, DatePipe, NgForOf, NgIf, SlicePipe} from '@angular/common';
import {JobResult} from '../../../../core/models/search.models';
import {Router} from '@angular/router';

@Component({
  selector: 'app-student-job-results',
  imports: [
    NgForOf,
    NgIf,
    CurrencyPipe,
    SlicePipe,
    DatePipe
  ],
  templateUrl: './student-job-results.component.html',
  styleUrl: '../search-results.component.css'
})
export class StudentJobResultsComponent {
  @Input() results: JobResult[] = [];
  @Output() searchResults = new EventEmitter<any[]>();

  constructor(
    private router: Router
  ) {}

  viewDetails(id: number): void {
    this.router.navigate(['/jobs', id]); // Navigate to a details page
  }
}
