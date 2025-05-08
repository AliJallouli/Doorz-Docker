import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule, NgClass, NgIf} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-student-job-search',
  templateUrl: './student-job-search.component.html',
  imports: [
    ReactiveFormsModule,
    NgClass, TranslateModule, NgIf,CommonModule


  ],
  styleUrls: ['./../search-form.component.css']
})
export class StudentJobSearchComponent {

  searchForm: FormGroup;
  validationTriggered = false;
  fieldError: { [key: string]: string } = {};
  globalError: string | null = null;

  @Output() searchResults = new EventEmitter<any[]>();

  contractTypes = [
    { contract_type_id: 1, name: 'CDD' },
    { contract_type_id: 2, name: 'CDI' },
    { contract_type_id: 3, name: 'Intérim' }
  ];
  scheduleTypes = [
    { schedule_type_id: 1, name: 'Plein temps' },
    { schedule_type_id: 2, name: 'Temps partiel' },
    { schedule_type_id: 3, name: 'Horaires flexibles' }
  ];

  constructor(private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      keywords: ['', [Validators.required, Validators.minLength(3)]],
      location: ['', Validators.minLength(2)],
      contractType: [''],
      scheduleType: [''],
      minSalary: [null, Validators.min(0)],
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
      { id: 1, title: 'Serveur à temps partiel', location: 'Bruxelles' },
      { id: 2, title: 'Vendeur étudiant', location: 'Liège' }
    ];
    this.searchResults.emit(mockResults);
  }
}
