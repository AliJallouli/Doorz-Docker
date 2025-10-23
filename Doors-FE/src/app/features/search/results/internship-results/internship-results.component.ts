import {Component, Input} from '@angular/core';
import {CurrencyPipe, DatePipe, NgForOf, NgIf, SlicePipe} from '@angular/common';
import {InternshipResult} from '../../../../core/models/search.models';
import {Router} from '@angular/router';

@Component({
  selector: 'app-internship-results',
  imports: [
    NgForOf,
    NgIf,
    SlicePipe,
    CurrencyPipe,
    DatePipe
  ],
  templateUrl: './internship-results.component.html',
  styleUrl: '../search-results.component.css'
})
export class InternshipResultsComponent {
  @Input() results: InternshipResult[] = [];
  constructor(
    private router: Router
  ) {}
  viewDetails(id: number): void {
    this.router.navigate(['/internships', id]); // Navigate to a details page
  }
}
