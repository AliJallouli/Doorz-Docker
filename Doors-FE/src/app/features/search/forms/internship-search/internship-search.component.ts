import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule, NgClass, NgIf} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-internship-search',
  templateUrl: './internship-search.component.html',
  imports: [
    ReactiveFormsModule,
    NgClass, TranslateModule, NgIf,CommonModule


  ],
  styleUrls: ['../search-form.component.css']
})
export class InternshipSearchComponent {
  searchForm: FormGroup;
  validationTriggered = false;
  fieldError: { [key: string]: string } = {};
  globalError: string | null = null;

  @Output() searchResults = new EventEmitter<any[]>();

  studyDomains = [
    { domain_id: 1, name: 'Informatique' },
    { domain_id: 2, name: 'Marketing' },
    { domain_id: 3, name: 'Ingénierie' }
  ];

  constructor(private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      studyDomain: [''],
      minDuration: [null, Validators.min(1)],
      ectsCredits: [null, Validators.min(0)],
      remotePossible: [false]
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
      { id: 1, title: 'Stage en développement web', location: 'Bruxelles', duration: 3 },
      { id: 2, title: 'Stage en marketing digital', location: 'Liège', duration: 6 }
    ];
    this.searchResults.emit(mockResults);
  }
}
