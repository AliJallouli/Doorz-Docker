import {Component, Input} from '@angular/core';
import {CurrencyPipe, DatePipe, NgForOf, NgIf, SlicePipe} from '@angular/common';
import {AidResult} from '../../../../core/models/search.models';
import {Router} from '@angular/router';

@Component({
  selector: 'app-aid-results',
  imports: [
    NgForOf,
    DatePipe,
    NgIf,
    CurrencyPipe,
    SlicePipe
  ],
  templateUrl: './aid-results.component.html',
  styleUrl: '../search-results.component.css'
})
export class AidResultsComponent {
  @Input() results: AidResult[] = [];

  constructor(
    private router: Router
  ) {}
  viewDetails(id: number): void {
    this.router.navigate(['/aids', id]); // Navigate to a details page
  }
}
