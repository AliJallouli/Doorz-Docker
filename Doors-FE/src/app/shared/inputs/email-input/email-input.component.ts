import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-email-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule],
  templateUrl: './email-input.component.html',
  styleUrls: ['../name-input/name-input.component.css']
})
export class EmailInputComponent implements OnInit {
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Input() emailControlName = 'email';
  @Input() confirmEmailControlName = 'confirmEmail';
  @Input() submitted = false;
  @Input() readonly = false;
  @Input() showConfirmEmail = false;
  @Input() emailType: 'default' | 'new' = 'default';
  @Input() emailLabelKey?: string;
  @Input() confirmEmailLabelKey?: string;

  ngOnInit() {
    console.log('showConfirmEmail:', this.showConfirmEmail);
    console.log('emailType:', this.emailType);
    console.log('form controls:', this.form.controls);
    console.log('readonly:', this.readonly);
  }

  // Sanitisation du champ email
  sanitizeEmail(): void {
    if (this.readonly) return;
    const value = this.form.get(this.emailControlName)?.value || '';
    // Supprime les espaces en début/fin et les caractères non valides pour un email
    const sanitized = value.trim().replace(/[^a-zA-Z0-9._%+-@]/g, '');
    this.form.get(this.emailControlName)?.setValue(sanitized, { emitEvent: false });
  }

  // Sanitisation du champ confirmEmail
  sanitizeConfirmEmail(): void {
    if (this.readonly) return;
    const value = this.form.get(this.confirmEmailControlName)?.value || '';
    // Supprime les espaces en début/fin et les caractères non valides pour un email
    const sanitized = value.trim().replace(/[^a-zA-Z0-9._%+-@]/g, '');
    this.form.get(this.confirmEmailControlName)?.setValue(sanitized, { emitEvent: false });
  }

  get effectiveEmailLabelKey(): string {
    if (this.emailLabelKey) {
      return this.emailLabelKey;
    }
    switch (this.emailType) {
      case 'new':
        return 'EMAIL_INPUT.LABEL.NEW_EMAIL';
      case 'default':
      default:
        return 'EMAIL_INPUT.LABEL.EMAIL';
    }
  }

  get effectiveEmailPlaceholderKey(): string {
    switch (this.emailType) {
      case 'new':
        return 'EMAIL_INPUT.PLACEHOLDER.NEW_EMAIL';
      case 'default':
      default:
        return 'EMAIL_INPUT.PLACEHOLDER.EMAIL';
    }
  }

  get effectiveRequiredErrorKey(): string {
    switch (this.emailType) {
      case 'new':
        return 'EMAIL_INPUT.ERROR.NEW_EMAIL_REQUIRED';
      case 'default':
      default:
        return 'EMAIL_INPUT.ERROR.EMAIL_REQUIRED';
    }
  }

  get effectiveConfirmEmailLabelKey(): string {
    if (this.confirmEmailLabelKey) {
      return this.confirmEmailLabelKey;
    }
    switch (this.emailType) {
      case 'new':
        return 'EMAIL_INPUT.LABEL.NEW_CONFIRM_EMAIL';
      case 'default':
      default:
        return 'EMAIL_INPUT.LABEL.CONFIRM_EMAIL';
    }
  }

  get effectiveConfirmEmailPlaceholderKey(): string {
    switch (this.emailType) {
      case 'new':
        return 'EMAIL_INPUT.PLACEHOLDER.NEW_CONFIRM_EMAIL';
      case 'default':
      default:
        return 'EMAIL_INPUT.PLACEHOLDER.CONFIRM_EMAIL';
    }
  }

  get effectiveConfirmRequiredErrorKey(): string {
    switch (this.emailType) {
      case 'new':
        return 'EMAIL_INPUT.ERROR.NEW_CONFIRM_EMAIL_REQUIRED';
      case 'default':
      default:
        return 'EMAIL_INPUT.ERROR.CONFIRM_EMAIL_REQUIRED';
    }
  }
}
