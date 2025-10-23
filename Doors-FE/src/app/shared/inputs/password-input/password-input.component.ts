import { Component, Input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-password-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule],
  templateUrl: './password-input.component.html',
  styleUrls: ['../name-input/name-input.component.css']
})
export class PasswordInputComponent {
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Input() passwordControlName = 'password';
  @Input() confirmPasswordControlName = 'confirmPassword';
  @Input() submitted = false;
  @Input() showPassword = true;
  @Input() showConfirmPassword = false;
  @Input() passwordType: 'default' | 'current' | 'new' = 'default';
  @Input() passwordLabelKey?: string;
  @Input() confirmPasswordLabelKey?: string;
  @Input() placeholder?: string;

  // Clé de traduction pour le label du champ mot de passe
  get effectivePasswordLabelKey(): string {
    if (this.passwordLabelKey) {
      return this.passwordLabelKey;
    }
    switch (this.passwordType) {
      case 'current':
        return 'PASSWORD_INPUT.LABEL.CURRENT_PASSWORD';
      case 'new':
        return 'PASSWORD_INPUT.LABEL.NEW_PASSWORD';
      case 'default':
      default:
        return 'PASSWORD_INPUT.LABEL.PASSWORD';
    }
  }

  // Clé de traduction pour le placeholder du champ mot de passe
  get effectivePasswordPlaceholderKey(): string {
    if (this.placeholder) {
      return this.placeholder;
    }
    switch (this.passwordType) {
      case 'current':
        return 'PASSWORD_INPUT.PLACEHOLDER.CURRENT_PASSWORD';
      case 'new':
        return 'PASSWORD_INPUT.PLACEHOLDER.NEW_PASSWORD';
      case 'default':
      default:
        return 'PASSWORD_INPUT.PLACEHOLDER.PASSWORD';
    }
  }

  // Clé de traduction pour l'erreur "required" du champ mot de passe
  get effectiveRequiredErrorKey(): string {
    switch (this.passwordType) {
      case 'current':
        return 'PASSWORD_INPUT.ERROR.CURRENT_PASSWORD_REQUIRED';
      case 'new':
        return 'PASSWORD_INPUT.ERROR.NEW_PASSWORD_REQUIRED';
      case 'default':
      default:
        return 'PASSWORD_INPUT.ERROR.PASSWORD_REQUIRED';
    }
  }

  // Clé de traduction pour l'erreur "minlength" du champ mot de passe
  get effectiveMinlengthErrorKey(): string {
    switch (this.passwordType) {
      case 'current':
        return 'PASSWORD_INPUT.ERROR.CURRENT_PASSWORD_MINLENGTH';
      case 'new':
        return 'PASSWORD_INPUT.ERROR.NEW_PASSWORD_MINLENGTH';
      case 'default':
      default:
        return 'PASSWORD_INPUT.ERROR.PASSWORD_MINLENGTH';
    }
  }

  // Clé de traduction pour l'erreur "pattern" du champ mot de passe
  get effectivePatternErrorKey(): string {
    switch (this.passwordType) {
      case 'current':
        return 'PASSWORD_INPUT.ERROR.CURRENT_PASSWORD_PATTERN';
      case 'new':
        return 'PASSWORD_INPUT.ERROR.NEW_PASSWORD_PATTERN';
      case 'default':
      default:
        return 'PASSWORD_INPUT.ERROR.PASSWORD_PATTERN';
    }
  }

  // Clé de traduction pour le label du champ de confirmation
  get effectiveConfirmPasswordLabelKey(): string {
    if (this.confirmPasswordLabelKey) {
      return this.confirmPasswordLabelKey;
    }
    switch (this.passwordType) {
      case 'new':
        return 'PASSWORD_INPUT.LABEL.NEW_CONFIRM_PASSWORD';
      case 'current':
      case 'default':
      default:
        return 'PASSWORD_INPUT.LABEL.CONFIRM_PASSWORD';
    }
  }

  // Clé de traduction pour l'erreur "required" du champ de confirmation
  get effectiveConfirmRequiredErrorKey(): string {
    switch (this.passwordType) {
      case 'new':
        return 'PASSWORD_INPUT.ERROR.NEW_CONFIRM_PASSWORD_REQUIRED';
      case 'current':
      case 'default':
      default:
        return 'PASSWORD_INPUT.ERROR.CONFIRM_PASSWORD_REQUIRED';
    }
  }
}
