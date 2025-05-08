import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule, NgClass, NgIf} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-event-search',
  templateUrl: './event-search.component.html',
  imports: [
    ReactiveFormsModule,
    NgClass, TranslateModule, NgIf,CommonModule


  ],
  styleUrls: ['./../search-form.component.css']
})
export class EventSearchComponent {
  searchForm: FormGroup;
  validationTriggered = false;
  fieldError: { [key: string]: string } = {};
  globalError: string | null = null;

  @Output() searchResults = new EventEmitter<any[]>();

  entityTypes = [
    { entity_type_id: 1, name: 'Institution' },
    { entity_type_id: 2, name: 'Entreprise' },
    { entity_type_id: 3, name: 'Campus' }
  ];

  constructor(private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      keywords: ['', [Validators.required, Validators.minLength(3)]],
      location: ['', Validators.minLength(2)],
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
        if (control?.invalid && control.touched) {
          this.fieldError[field] = `SHARED.ERROR.FRONTEND.${field.toUpperCase()}_INVALID`;
        }
      });
      return;
    }

    const searchCriteria = this.searchForm.value;
    console.log('Critères de recherche:', searchCriteria);

    // Placeholder pour les résultats (à remplacer par un appel API)
    const mockResults = [
      { id: 1, title: 'Conférence sur l’IA', location: 'Bruxelles', date: '2025-05-10' },
      { id: 2, title: 'Atelier de networking', location: 'Namur', date: '2025-06-15' }
    ];
    this.searchResults.emit(mockResults);
  }
}
