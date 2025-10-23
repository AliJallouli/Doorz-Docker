import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NgClass, NgIf, CommonModule } from '@angular/common';

@Component({
  selector: 'app-name-input',
  standalone: true,
  imports: [CommonModule, TranslateModule, NgClass, NgIf, ReactiveFormsModule],
  templateUrl: './name-input.component.html',
  styleUrls: ['./name-input.component.css']
})
export class NameInputComponent implements OnInit {
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Input() firstNameControlName = 'firstName';
  @Input() lastNameControlName = 'lastName';
  @Input() fullNameControlName = 'fullName';
  @Input() submitted = false;
  @Input() mode: 'split' | 'full' = 'split';
  @Input() nameType: 'default' | 'new' = 'default';
  @Input() readonly: boolean = false;

  ngOnInit() {
    console.log('NameInputComponent - mode:', this.mode);
    console.log('NameInputComponent - nameType:', this.nameType);
    console.log('NameInputComponent - readonly:', this.readonly);
    console.log('NameInputComponent - form controls:', Object.keys(this.form.controls));
  }

  // Sanitisation du champ fullName (mode full)
  sanitizeFullName(): void {
    if (this.readonly) return;
    const value = this.form.get(this.fullNameControlName)?.value || '';
    const sanitized = value.replace(/[^a-zA-ZÀ-ÿ\u00C0-\u017F\s'-]/g, '');
    this.form.get(this.fullNameControlName)?.setValue(sanitized, { emitEvent: false });
  }

  // Sanitisation du champ firstName (mode split)
  sanitizeFirstName(): void {
    if (this.readonly) return;
    const value = this.form.get(this.firstNameControlName)?.value || '';
    const sanitized = value.replace(/[^a-zA-ZÀ-ÿ\u00C0-\u017F\s'-]/g, '');
    this.form.get(this.firstNameControlName)?.setValue(sanitized, { emitEvent: false });
  }

  // Sanitisation du champ lastName (mode split)
  sanitizeLastName(): void {
    if (this.readonly) return;
    const value = this.form.get(this.lastNameControlName)?.value || '';
    const sanitized = value.replace(/[^a-zA-ZÀ-ÿ\u00C0-\u017F\s'-]/g, '');
    this.form.get(this.lastNameControlName)?.setValue(sanitized, { emitEvent: false });
  }

  // Label pour fullName
  get effectiveFullNameLabelKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.LABEL.NEW_FULLNAME' : 'NAME_INPUT.LABEL.FULLNAME';
  }

  // Placeholder pour fullName
  get effectiveFullNamePlaceholderKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.PLACEHOLDER.NEW_FULLNAME' : 'NAME_INPUT.PLACEHOLDER.FULLNAME';
  }

  // Erreur required pour fullName
  get effectiveFullNameRequiredErrorKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.ERROR.FRONTEND.NEW_FULLNAME_REQUIRED' : 'NAME_INPUT.ERROR.FRONTEND.FULLNAME_REQUIRED';
  }

  // Label pour firstName
  get effectiveFirstNameLabelKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.LABEL.NEW_FIRSTNAME' : 'NAME_INPUT.LABEL.FIRSTNAME';
  }

  // Placeholder pour firstName
  get effectiveFirstNamePlaceholderKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.PLACEHOLDER.NEW_FIRSTNAME' : 'NAME_INPUT.PLACEHOLDER.FIRSTNAME';
  }

  // Erreur required pour firstName
  get effectiveFirstNameRequiredErrorKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.ERROR.FRONTEND.NEW_FIRSTNAME_REQUIRED' : 'NAME_INPUT.ERROR.FRONTEND.FIRSTNAME_REQUIRED';
  }

  // Erreur minlength pour firstName
  get effectiveFirstNameMinlengthErrorKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.ERROR.FRONTEND.NEW_FIRSTNAME_MINLENGTH' : 'NAME_INPUT.ERROR.FRONTEND.FIRSTNAME_MINLENGTH';
  }

  // Label pour lastName
  get effectiveLastNameLabelKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.LABEL.NEW_LASTNAME' : 'NAME_INPUT.LABEL.LASTNAME';
  }

  // Placeholder pour lastName
  get effectiveLastNamePlaceholderKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.PLACEHOLDER.NEW_LASTNAME' : 'NAME_INPUT.PLACEHOLDER.LASTNAME';
  }

  // Erreur required pour lastName
  get effectiveLastNameRequiredErrorKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.ERROR.FRONTEND.NEW_LASTNAME_REQUIRED' : 'NAME_INPUT.ERROR.FRONTEND.LASTNAME_REQUIRED';
  }

  // Erreur minlength pour lastName
  get effectiveLastNameMinlengthErrorKey(): string {
    return this.nameType === 'new' ? 'NAME_INPUT.ERROR.FRONTEND.NEW_LASTNAME_MINLENGTH' : 'NAME_INPUT.ERROR.FRONTEND.LASTNAME_MINLENGTH';
  }
}
