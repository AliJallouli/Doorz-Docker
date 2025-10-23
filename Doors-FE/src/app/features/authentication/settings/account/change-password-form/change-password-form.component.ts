import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PasswordInputComponent } from '../../../../../shared/inputs/password-input/password-input.component';
import { User } from '../../../../../core/models/user.model';
import { NgClass, NgIf } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import {ActionButtonsComponent} from '../../../../../shared/buttons/action-buttons/action-buttons.component';

@Component({
  selector: 'app-change-password-form',
  standalone: true,
  imports: [
    FormsModule,
    PasswordInputComponent,
    ReactiveFormsModule,
    TranslateModule,
    ActionButtonsComponent,
    NgIf
  ],
  templateUrl: './change-password-form.component.html',
  styleUrls: ['../../settings.component.css']
})
export class ChangePasswordFormComponent implements OnInit {
  @Input() user!: User | null;
  @Output() updated = new EventEmitter<{ currentPassword: string; newPassword: string } | null>();
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Input() passwordControlName = 'password';
  @Input() confirmPasswordControlName = 'confirmPassword';
  @Input() errorMessage: string | null = null;
  @Input() errorParams: { [key: string]: number } = {};
  submitted = false;


  ngOnInit(): void {
    console.log('ChangePasswordFormComponent - Form controls:', Object.keys(this.form.controls));
  }

  submit(): void {
    this.submitted = true;
    this.fieldError = {};

    if (this.form.invalid || this.form.hasError('passwordMismatch')) {
      console.warn('Submission bloquée : formulaire invalide ou mots de passe non correspondants');
      if (this.form.get('currentPassword')?.hasError('required')) {
        this.fieldError['currentPassword'] = 'PASSWORD_INPUT.ERROR.CURRENT_PASSWORD_REQUIRED';
      }
      if (this.form.get('password')?.hasError('required')) {
        this.fieldError['password'] = 'PASSWORD_INPUT.ERROR.NEW_PASSWORD_REQUIRED';
      }
      if (this.form.get('password')?.hasError('minlength')) {
        this.fieldError['password'] = 'PASSWORD_INPUT.ERROR.NEW_PASSWORD_MINLENGTH';
      }
      if (this.form.get('password')?.hasError('pattern')) {
        this.fieldError['password'] = 'PASSWORD_INPUT.ERROR.NEW_PASSWORD_PATTERN';
      }
      if (this.form.get('confirmPassword')?.hasError('required')) {
        this.fieldError['confirmPassword'] = 'PASSWORD_INPUT.ERROR.NEW_CONFIRM_PASSWORD_REQUIRED';
      }
      if (this.form.hasError('passwordMismatch')) {
        this.fieldError['confirmPassword'] = 'PASSWORD_INPUT.ERROR.PASSWORD_MISMATCH';
      }
      return;
    }

    this.updated.emit({
      currentPassword: this.form.value.currentPassword,
      newPassword: this.form.value.password
    });
  }

  onCancel(): void {
    this.form.reset();
    this.submitted = false;
    this.fieldError = {};
    this.updated.emit(null);
  }

  handleAction(action: string): void {
    switch (action) {
      case 'submit':
        this.submit();
        break;
      case 'cancel':
        this.onCancel();
        break;
      default:
        console.warn('Action non gérée :', action);
    }
  }
}
