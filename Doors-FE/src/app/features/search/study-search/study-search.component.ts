import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgClass, NgForOf, NgIf} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-study-search',
  templateUrl: './study-search.component.html',
  imports: [
    ReactiveFormsModule,
    NgClass, TranslateModule, NgIf, NgForOf
  ],
  styleUrls: ['./../search-form.component.css']
})
export class StudySearchComponent {
  searchForm: FormGroup;
  validationTriggered = false;
  fieldError: { [key: string]: string } = {};
  globalError: string | null = null;

  @Output() searchResults = new EventEmitter<any[]>();

  studyDomains = [
    { domain_id: 1, name: 'Informatique' },
    { domain_id: 2, name: 'Droit' },
    { domain_id: 3, name: 'Médecine' }
  ];
  degreeTypes = [
    { degree_type_id: 1, name: 'Bachelier' },
    { degree_type_id: 2, name: 'Master' },
    { degree_type_id: 3, name: 'Doctorat' }
  ];
  deliveryModes = [
    { delivery_mode_id: 1, name: 'Présentiel' },
    { delivery_mode_id: 2, name: 'À distance' },
    { delivery_mode_id: 3, name: 'Hybride' }
  ];

  constructor(private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      keywords: ['', [Validators.required, Validators.minLength(3)]],
      studyDomain: [''],
      location: ['', Validators.minLength(2)],
      degreeType: [''],
      deliveryMode: [''],
      maxCost: [null, Validators.min(0)]
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
      { id: 1, title: 'Bachelier en informatique', location: 'Bruxelles', cost: 1000 },
      { id: 2, title: 'Master en droit', location: 'Louvain-la-Neuve', cost: 1500 }
    ];
    this.searchResults.emit(mockResults);
  }
}
