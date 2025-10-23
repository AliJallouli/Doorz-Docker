import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { User } from '../../../../../core/models/user.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NameInputComponent } from '../../../../../shared/inputs/name-input/name-input.component';
import { PasswordInputComponent } from '../../../../../shared/inputs/password-input/password-input.component';
import {ActionButtonsComponent} from '../../../../../shared/buttons/action-buttons/action-buttons.component';

interface UpdateNameEvent {
  currentPassword: string;
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'app-change-name-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    NameInputComponent,
    PasswordInputComponent,
    ActionButtonsComponent
  ],
  templateUrl: './change-name-form.component.html',
  styleUrls: ['../../settings.component.css']
})
export class ChangeNameFormComponent implements OnInit {
  @Input() user!: User | null;
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Output() updated = new EventEmitter<UpdateNameEvent | null>();

  @Input() errorMessage: string | null = null;
  @Input() successMessage: string | null = null;
  @Input() errorParams: { [key: string]: number } = {};
  submitted = false;

  ngOnInit(): void {
    console.log('ChangeNameFormComponent - Form controls:', Object.keys(this.form.controls));
    console.log('ChangeNameFormComponent - User:', this.user);
    if (this.user) {
      this.form.patchValue({
        firstName: this.user.firstName,
        lastName: this.user.lastName
      });
    }
  }

  submit() {
    this.submitted = true;
    this.fieldError = {};

    if (this.form.valid) {
      const { currentPassword, firstName, lastName } = this.form.value;
      console.log('Formulaire soumis : ', this.form.value);
      this.updated.emit({ currentPassword, firstName, lastName });
    } else {
      if (this.form.get('currentPassword')?.hasError('required')) {
        this.fieldError['currentPassword'] = 'PASSWORD_INPUT.ERROR.CURRENT_PASSWORD_REQUIRED';
      }
      if (this.form.get('firstName')?.hasError('required')) {
        this.fieldError['firstName'] = 'NAME_INPUT.ERROR.FRONTEND.NEW_FIRSTNAME_REQUIRED';
      }
      if (this.form.get('firstName')?.hasError('minlength')) {
        this.fieldError['firstName'] = 'NAME_INPUT.ERROR.FRONTEND.NEW_FIRSTNAME_MINLENGTH';
      }
      if (this.form.get('lastName')?.hasError('required')) {
        this.fieldError['lastName'] = 'NAME_INPUT.ERROR.FRONTEND.NEW_LASTNAME_REQUIRED';
      }
      if (this.form.get('lastName')?.hasError('minlength')) {
        this.fieldError['lastName'] = 'NAME_INPUT.ERROR.FRONTEND.NEW_LASTNAME_MINLENGTH';
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
