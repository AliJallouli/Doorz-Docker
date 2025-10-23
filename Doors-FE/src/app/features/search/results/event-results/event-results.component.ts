import {Component, Input} from '@angular/core';
import {DatePipe, NgForOf, NgIf, SlicePipe} from '@angular/common';
import {EventResult} from '../../../../core/models/search.models';
import {Router} from '@angular/router';

@Component({
  selector: 'app-event-results',
  imports: [
    DatePipe,
    NgForOf,
    NgIf,
    SlicePipe
  ],
  templateUrl: './event-results.component.html',
  styleUrl: '../search-results.component.css'
})
export class EventResultsComponent {
  @Input() results: EventResult[] = [];

  constructor(
    private router: Router
  ) {}
  viewDetails(id: number): void {
    this.router.navigate(['/events', id]); // Navigate to a details page
  }
}
