import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ValidationErrors,
  AbstractControl,
  FormControl,
  ValidatorFn, ReactiveFormsModule
} from '@angular/forms';
import { TranslateService,TranslateModule } from '@ngx-translate/core';

import { ActivatedRoute } from '@angular/router';
import { PasswordResetService } from '../../../../core/services/auth/password-reset.service';
import { ToastService } from '../../../../core/services/toast.service';
import { resolveErrorTranslationKey } from '../../../../core/errors/error-context-resolver';
import {
  ConfirmPasswordResetRequestDTO,
  ConfirmPasswordResetResponseDTO, ValidatePasswordResetTokenAndOTPRequestDTO,
  ValidatePasswordResetTokenRequestDTO,
  ValidatePasswordResetTokenResponseDTO
} from '../../../../models/auth.models';
import {NgClass, NgIf} from '@angular/common';
import {PasswordInputComponent} from '../../../../layers/password-input/password-input.component';

@Component({
  selector: 'app-reset-password-form',
  standalone: true,
  templateUrl: './reset-password-form.component.html',
  imports: [
    ReactiveFormsModule,
    NgIf,
    TranslateModule,
    PasswordInputComponent
  ],
  styleUrls: ['./reset-password-form.component.css']
})
export class ResetPasswordFormComponent implements OnInit {
  form!: FormGroup;
  submitted = false;
  isLoading = true;
  token = '';
  errorMessage = '';
  showOtpForm = false;
  showForm = false;
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private passwordservice: PasswordResetService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    const token = decodeURIComponent(<string>this.route.snapshot.queryParamMap.get('token'));
    this.token = token;

    if (!this.token) {
      this.errorMessage = 'RESETPASSWORD.ERROR.INVALID_LINK';
      this.isLoading = false;
      return;
    }

    const request: ValidatePasswordResetTokenRequestDTO = { token: this.token };

    this.passwordservice.validateResetPasswordToken(request).subscribe({
      next: (res) => {
        if (res.data?.email) {
          this.initOtpForm(res.data.email); // ➕ init form uniquement avec OTP au début
          this.showOtpForm = true;
        } else {
          this.errorMessage = resolveErrorTranslationKey(res.error?.key || 'RESETPASSWORD.ERROR.INVALID_TOKEN');
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = resolveErrorTranslationKey(err.key);
        this.isLoading = false;
      }
    });
  }
  initOtpForm(email: string): void {
    this.form = this.fb.group({
      email: new FormControl({ value: email, disabled: true }, [Validators.required, Validators.email]),
      otp: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(6)])
    });
  }

  initPasswordForm(email: string): void {
    this.form = this.fb.group(
      {
        email: new FormControl({ value: email, disabled: true }, [Validators.required, Validators.email]),
        newPassword: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).+$/)
        ]),
        confirmPassword: new FormControl('', Validators.required)
      },
      { validators: this.passwordMatchValidator() }
    );
  }


  private passwordMatchValidator(): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const password = group.get('newPassword')?.value;
      const confirm = group.get('confirmPassword')?.value;
      return password === confirm ? null : { passwordMismatch: true };
    };
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid || !this.token) {
      return;
    }

    const payload: ConfirmPasswordResetRequestDTO = {
      token: this.token,
      email: this.form.get('email')?.value,
      newPassword: this.form.get('newPassword')?.value
    };

    this.passwordservice.resetPassword(payload).subscribe({
      next: (res) => {
        this.successMessage = res.success?.key || res.data?.key || 'RESETPASSWORD.SUCCESS';
        this.toast.show(this.successMessage, 'success');
        this.showForm = false;
        this.errorMessage='';
      },
      error: (err) => {
        const key = resolveErrorTranslationKey(err.key);
        this.errorMessage = key;
        this.toast.show(this.errorMessage, 'error');
      }
    });
  }

  validateOtp(): void {
    if (this.form.invalid || !this.token) {
      return;
    }

    const otpCode = this.form.get('otp')?.value?.toString().trim();

    const payload: ValidatePasswordResetTokenAndOTPRequestDTO = {
      Token: this.token,
      Otp: otpCode
    };
    this.passwordservice.validateResetPasswordOtp(payload).subscribe({
      next: (res) => {
        this.initPasswordForm(res.data?.email || '');
        this.showOtpForm = false;
        this.showForm = true;
      },
      error: (err) => {
        const key = resolveErrorTranslationKey(err.key);
        this.errorMessage = key;
        this.toast.show(key, 'error');
      }
    });
  }

}
