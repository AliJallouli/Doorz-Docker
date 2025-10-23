import {Component, Input} from '@angular/core';
import {CurrencyPipe, DatePipe, NgForOf, NgIf, SlicePipe} from "@angular/common";
import {AidResult, KotResult} from '../../../../core/models/search.models';
import {Router} from '@angular/router';

@Component({
  selector: 'app-kot-results',
    imports: [
        CurrencyPipe,
        DatePipe,
        NgForOf,
        NgIf,
        SlicePipe
    ],
  templateUrl: './kot-results.component.html',
  styleUrl: '../search-results.component.css'
})
export class KotResultsComponent {
  @Input() results: KotResult[] = [];

  constructor(
    private router: Router
  ) {}
  viewDetails(id: number): void {
    this.router.navigate(['/aids', id]); // Navigate to a details page
  }
}
