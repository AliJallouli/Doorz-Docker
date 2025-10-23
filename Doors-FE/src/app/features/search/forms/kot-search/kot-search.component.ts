import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgClass, NgForOf, NgIf} from "@angular/common";
import {TranslatePipe} from "@ngx-translate/core";
import {EventResult} from '../../../../core/models/search.models';
import {SearchService} from '../../../../core/services/search/search.service';

@Component({
  selector: 'app-kot-search',
  imports: [
    FormsModule,
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    TranslatePipe,
    NgClass
  ],
  templateUrl: './kot-search.component.html',
  styleUrls: ['../search-form.component.css']
})
export class KotSearchComponent {
  searchForm: FormGroup;
  validationTriggered = false;
  fieldError: { [key: string]: string } = {};
  globalError: string | null = null;

  @Output() searchResults = new EventEmitter<EventResult[]>(); // Type as EventResult[]

  entityTypes = [
    { entity_type_id: 1, name: 'Institution' },
    { entity_type_id: 2, name: 'Entreprise' },
    { entity_type_id: 3, name: 'Campus' }
  ];

  constructor(
    private fb: FormBuilder,
    private searchService: SearchService
  ) {
    this.searchForm = this.fb.group({
      startDate: [null],
      entityType: [''],
      isPublic: [true]
    });
  }

  onSubmit(): void {
    this.validationTriggered = true;
    this.fieldError = {};
    this.globalError = null;

    if (this.searchForm.invalid) {
      Object.keys(this.searchForm.controls).forEach(field => {
        const control = this.searchForm.get(field);
        if (control?.invalid && control?.touched) {
          this.fieldError[field] = `SHARED.ERROR.FRONTEND.${field.toUpperCase()}_INVALID`;
        }
      });
      return;
    }

    const searchCriteria = {
      startDate: this.searchForm.value.startDate,
      entityType: this.searchForm.value.entityType,
      isPublic: this.searchForm.value.isPublic
    };
    console.log('Critères de recherche:', searchCriteria);

    // Call SearchService with type 'events'
    this.searchService.search('events', searchCriteria).subscribe({
      next: (results: EventResult[]) => { // Explicitly type as EventResult[]
        this.searchResults.emit(results);
      },
      error: () => {
        this.globalError = 'SEARCH.EVENT.ERROR.FAILED';
        console.error('Search error:');
      }
    });
  }
}
