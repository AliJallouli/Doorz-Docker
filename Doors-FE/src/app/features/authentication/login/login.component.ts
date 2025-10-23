import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { resolveErrorTranslationKey } from '../../../core/errors/error-context-resolver';
import { RouterLink } from '@angular/router';
import { EmailInputComponent } from '../../../shared/inputs/email-input/email-input.component';
import {CallToActionComponent} from '../../shared/call-to-action/call-to-action.component';
import {LoggerService} from '../../../core/services/logger/logger.service';
import {PasswordInputComponent} from '../../../shared/inputs/password-input/password-input.component';
import {UserService} from '../../../core/services/user/user.service';
import {SharedValidators} from '../../../core/utils/Validators/shared-validators';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule, EmailInputComponent, CallToActionComponent, PasswordInputComponent],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  pageType: 'register' | 'login' | 'invitation' = 'login';
  form!: FormGroup;
  errorMessage: string = '';
  fieldError: { [key: string]: string } = {};
  validationTriggered = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private readonly logger: LoggerService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', [Validators.required, SharedValidators.email()]],
      password: ['', Validators.required],
      rememberMe: [false]
    });
  }

  onSubmit(): void {
    this.validationTriggered = true;
    this.errorMessage = '';
    this.fieldError = {};

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.errorMessage = 'LOGIN.ERROR.FRONTEND.EMPTY_FIELDS';
      return;
    }

    const { email, password,rememberMe } = this.form.value;

    this.authService.login(email, password,rememberMe).subscribe({
      next: () => {
        this.userService.loadUser();
        this.authService.authStateReady$.pipe(
        ).subscribe(() => {
          this.logger.log('✅ Login terminé et état synchronisé.');
        });
      },
      error: (err) => {
        this.logger.log(err.key, 'error')
        if (err.key === 'EMAIL_NOT_FOUND' || err.key === 'INVALID_CREDENTIALS'
          || err.key === 'INVALID_LOGIN' ) {
          this.errorMessage = resolveErrorTranslationKey('INVALID_LOGIN');
        } else {
          this.errorMessage = resolveErrorTranslationKey(err.key || 'LOGIN.ERROR.GENERIC');
        }
      }
    });

  }
}
