import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { User } from '../../../../../core/models/user.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { EmailInputComponent } from '../../../../../shared/inputs/email-input/email-input.component';
import { PasswordInputComponent } from '../../../../../shared/inputs/password-input/password-input.component';
import {ActionButtonsComponent} from '../../../../../shared/buttons/action-buttons/action-buttons.component';

interface UpdateEmailEvent {
  currentPassword: string;
  newEmail: string;
}

@Component({
  selector: 'app-change-email-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    EmailInputComponent,
    PasswordInputComponent,
    ActionButtonsComponent
  ],
  templateUrl: './change-email-form.component.html',
  styleUrls: ['../../settings.component.css']
})
export class ChangeEmailFormComponent implements OnInit {
  @Input() user!: User | null;
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Output() updated = new EventEmitter<UpdateEmailEvent | null>();
  @Input() errorParams: { [key: string]: number } = {};
  @Input() errorMessage: string | null = null;
  submitted = false;

  ngOnInit(): void {
    console.log('ChangeEmailFormComponent - Form controls:', Object.keys(this.form.controls));
    console.log('ChangeEmailFormComponent - User:', this.user);
  }

  submit() {
    this.submitted = true;
    this.fieldError = {};

    if (this.form.valid) {
      const { currentPassword, newEmail } = this.form.value;
      console.log('Formulaire soumis : ', this.form.value);
      this.updated.emit({ currentPassword, newEmail });
    } else {
      if (this.form.get('currentPassword')?.hasError('required')) {
        this.fieldError['currentPassword'] = 'PASSWORD_INPUT.ERROR.CURRENT_PASSWORD_REQUIRED';
      }
      if (this.form.get('newEmail')?.hasError('required')) {
        this.fieldError['newEmail'] = 'EMAIL_INPUT.ERROR.NEW_EMAIL_REQUIRED';
      }
      if (this.form.get('newEmail')?.hasError('email')) {
        this.fieldError['newEmail'] = 'EMAIL_INPUT.ERROR.EMAIL_INVALID';
      }
      if (this.form.get('confirmEmail')?.hasError('required')) {
        this.fieldError['confirmEmail'] = 'EMAIL_INPUT.ERROR.NEW_CONFIRM_EMAIL_REQUIRED';
      }
      if (this.form.hasError('emailMismatch')) {
        this.fieldError['confirmEmail'] = 'EMAIL_INPUT.ERROR.EMAIL_MISMATCH';
      }
    }
  }

  handleAction(action: string) {
    switch (action) {
      case 'submit':
        this.submit();
        break;
      case 'cancel':
        this.form.reset();
        this.submitted = false;
        this.fieldError = {};
        this.updated.emit(null);
        break;
      default:
        console.warn('Action non gérée :', action);
    }
  }
}
