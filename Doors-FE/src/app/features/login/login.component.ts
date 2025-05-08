import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { resolveErrorTranslationKey } from '../../core/errors/error-context-resolver';
import { RouterLink } from '@angular/router';
import { EmailInputComponent } from '../../layers/email-input/email-input.component';
import {CallToActionComponent} from '../call-to-action/call-to-action.component';
import {LoggerService} from '../../core/services/logger/logger.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule, EmailInputComponent, CallToActionComponent],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  errorMessage: string = '';
  fieldError: { [key: string]: string } = {};
  validationTriggered = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private readonly logger: LoggerService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
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

    const { email, password } = this.form.value;

    this.authService.login(email, password).subscribe({
      next: () => {
        this.authService.authStateReady$.pipe(
          // On attend que l’état soit bien synchronisé
          // (si nécessaire tu peux ajouter un timeout)
        ).subscribe(() => {
          this.logger.log('✅ Login terminé et état synchronisé.');
        });
      },
      error: (err) => {
        if (err.key === 'EMAIL_NOT_FOUND' || err.key === 'INVALID_CREDENTIALS') {
          this.fieldError['email'] = resolveErrorTranslationKey(err.key);
        } else {
          this.errorMessage = resolveErrorTranslationKey(err.key || 'LOGIN.ERROR.GENERIC');
        }
      }
    });

  }
}
