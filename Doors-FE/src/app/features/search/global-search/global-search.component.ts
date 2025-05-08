import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-global-search',
  imports: [
    ReactiveFormsModule,TranslateModule
  ],
  templateUrl: './global-search.component.html',
  styleUrl: './global-search.component.css'
})
export class GlobalSearchComponent {
  searchForm: FormGroup;
  @Output() search = new EventEmitter<{ keywords: string, location: string }>();

  constructor(private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      keywords: [''],
      location: ['']
    });
  }

  onSubmit(): void {
    const formValue = this.searchForm.value;
    this.search.emit({
      keywords: formValue.keywords.trim(),
      location: formValue.location.trim()
    });
  }

}
