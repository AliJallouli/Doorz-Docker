import {Component, Input} from '@angular/core';
import {CurrencyPipe, NgForOf, NgIf, SlicePipe} from '@angular/common';
import {StudyResult} from '../../../../core/models/search.models';
import {Router} from '@angular/router';

@Component({
  selector: 'app-study-results',
  imports: [
    NgForOf,
    NgIf,
    CurrencyPipe,
    SlicePipe
  ],
  templateUrl: './study-results.component.html',
  styleUrl: '../search-results.component.css'
})
export class StudyResultsComponent {
  @Input() results: StudyResult[] = [];

  constructor(
    private router: Router
  ) {}
  viewDetails(id: number): void {
    this.router.navigate(['/studies', id]); // Navigate to a details page
  }
}
