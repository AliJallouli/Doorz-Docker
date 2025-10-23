import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { resolveErrorTranslationKey } from '../../../../core/errors/error-context-resolver';
import { EmailConfirmationService } from '../../../../core/services/auth/email-confirmation.service';
import { ToastService } from '../../../../core/services/toast.service';
import { ConfirmEmailRequestDTO } from '../../../../core/models/invite.models';
import { ApiResponse } from '../../../../core/models/auth.models';
import { NgClass, NgIf } from '@angular/common';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { EmailInputComponent } from '../../../../shared/inputs/email-input/email-input.component';
import {LoggerService} from '../../../../core/services/logger/logger.service';
import {OtpInputComponent} from '../../../../shared/inputs/otp-input/otp-input.component';

@Component({
  selector: 'app-confirm-email',
  standalone: true,
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css'],
  imports: [NgClass, NgIf, TranslateModule, ReactiveFormsModule, EmailInputComponent, OtpInputComponent]
})
export class ConfirmEmailComponent implements OnInit, OnDestroy {
  isLoading = true;
  success = false;
  resultMessage = '';
  showResend = false;
  showOtpForm = false;
  resendForm!: FormGroup;
  otpForm!: FormGroup;
  loadingResend = false;
  loadingOtp = false;
  isLoggedIn = false;
  otpTooManyAttempts = false;
  remainingWaitTimeSeconds = 0;
  countdownTimer: any;
  remainingResends = 0;
  otpAttemptsLeft = 0;
  countdown: number = 0;
  countdownInterval: any = null;
  fieldError: { [key: string]: string } = {};
  validationTriggered = false;

  private lastToken!: string;

  constructor(
    private route: ActivatedRoute,
    private emailConfirmationService: EmailConfirmationService,
    private toast: ToastService,
    private router: Router,
    private fb: FormBuilder,
    private translate: TranslateService,
    private authService: AuthService,
    private readonly logger: LoggerService
  ) {}

  startCountdown(seconds: number): void {
    this.countdown = seconds;

    if (this.countdownInterval) {
      clearInterval(this.countdownInterval);
    }

    this.countdownInterval = setInterval(() => {
      this.countdown--;

      if (this.countdown <= 0) {
        clearInterval(this.countdownInterval);
        this.countdownInterval = null;
      }
    }, 1000);
  }

  ngOnInit(): void {
    this.resendForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });

    this.otpForm = this.fb.group({
      codeOtp: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]]
    });

    const token = decodeURIComponent(this.route.snapshot.queryParamMap.get('token') || '');
    if (!token) {
      this.resultMessage = 'CONFIRMEMAIL.ERROR.FRONTEND.INVALID_LINK';
      this.isLoading = false;
      this.logger.warn('[Init] Token invalide ou manquant');
      return;
    }

    this.lastToken = token;
    const request: ConfirmEmailRequestDTO = { token, codeOtp: null as any };
    this.logger.log('[Init] Token détecté :', token);

    this.authService.isLoggedIn().subscribe(logged => {
      this.isLoggedIn = logged;
      this.logger.log('[Init] Utilisateur connecté ?', logged);

      this.emailConfirmationService.validateToken(request).subscribe({
        next: (res: ApiResponse<any>) => {
          this.logger.log('[ValidateToken] Réponse success :', res);

          if (res.success) {
            this.resultMessage = 'CONFIRMEMAIL.OTP_REQUIRED';
            this.showOtpForm = true;
            this.showResend = true;
            const meta = res.data?.metadata;
            this.remainingResends = meta?.remainingResends ?? this.remainingResends;
            this.otpAttemptsLeft = meta?.otpAttemptsLeft ?? this.otpAttemptsLeft;
            this.logger.log('[Success] metadata :', meta);
          } else if (res.error?.key === 'OTP_TOO_MANY_ATTEMPTS') {
            this.logger.warn('[OTP_TOO_MANY_ATTEMPTS] Reçu via success false :', res.error);

            const extra = res.error.extraData;
            this.otpTooManyAttempts = true;
            this.showOtpForm = logged;
            this.showResend = (extra?.['remainingResends'] ?? 0) > 0;
            this.remainingResends = extra?.['remainingResends'] ?? 0;
            this.otpAttemptsLeft = extra?.['otpAttemptsLeft'] ?? 0;
            this.resultMessage = resolveErrorTranslationKey(res.error.key);

            this.logger.log('[OTP_TOO_MANY_ATTEMPTS] logged=', logged, '| remainingResends=', this.remainingResends, '| otpAttemptsLeft=', this.otpAttemptsLeft);
          } else {
            this.logger.warn('[Error non OTP_TOO_MANY_ATTEMPTS] Utilisation de handleBackendError()');
            this.handleBackendError(res.error?.key, res.error?.extraData);
          }

          this.isLoading = false;
        },

        error: (err) => {
          this.logger.error('[ValidateToken] Erreur HTTP :', err);

          if (err.key === 'OTP_TOO_MANY_ATTEMPTS') {
            const extra = err.extraData;
            this.otpTooManyAttempts = true;
            this.showOtpForm = this.isLoggedIn;
            this.showResend = (extra?.['remainingResends'] ?? 0) > 0;
            this.remainingResends = extra?.['remainingResends'] ?? 0;
            this.otpAttemptsLeft = extra?.['otpAttemptsLeft'] ?? 0;
            this.resultMessage = resolveErrorTranslationKey(err.key);

            this.logger.warn('[OTP_TOO_MANY_ATTEMPTS - Error] logged=', this.isLoggedIn, '| remainingResends=', this.remainingResends, '| otpAttemptsLeft=', this.otpAttemptsLeft);
          } else {
            this.logger.warn('[Error] Utilisation de handleBackendError()');
            this.handleBackendError(err.key, err.extraData);
          }

          this.isLoading = false;
        }
      });
    });

    this.router.navigate([], { queryParams: {}, replaceUrl: true });
  }

  private handleBackendError(key?: string, extra?: any): void {
    const resolvedKey = resolveErrorTranslationKey(key || 'EMAIL.CONFIRMATION.FAILED');
    this.resultMessage = resolvedKey;

    this.remainingResends = extra?.remainingResends ?? 0;
    this.otpAttemptsLeft = extra?.otpAttemptsLeft ?? 0;

    // Réinitialiser fieldError pour éviter d'afficher des erreurs obsolètes
    this.fieldError = {};

    switch (key) {
      case 'EMAIL_CONFIRMATION_TOKEN_EXPIRED':
      case 'EMAIL_CONFIRMATION_INVALID_TOKEN':
        this.showResend = this.remainingResends > 0;
        this.showOtpForm = false;
        this.otpTooManyAttempts = false;
        break;

      case 'EMAIL_ALREADY_CONFIRMED':
        this.success = true;
        this.showResend = false;
        this.showOtpForm = false;
        this.otpTooManyAttempts = false;
        break;

      case 'OTP_TOO_MANY_ATTEMPTS':
        this.otpTooManyAttempts = true;
        this.showOtpForm = true;

        if (this.isLoggedIn && this.remainingResends > 0) {
          this.showResend = true;
        } else if (!this.isLoggedIn && this.remainingResends > 0) {
          this.showResend = true;
        } else {
          this.showResend = false;
        }
        break;

      case 'TOO_MANY_OTP_RESENDS':
        this.otpTooManyAttempts = true;
        this.showOtpForm = false;
        this.showResend = false;
        const seconds = extra?.['remainingWaitTimeSeconds'] ?? 0;
        this.startCountdownAndValidateAfter(seconds);
        break;

      case 'OTP_INVALID':
        this.otpTooManyAttempts = false;
        this.showOtpForm = true;
        break;

      default:
        this.showResend = this.remainingResends > 0;
        break;
    }
  }

  submitOtp(): void {
    if (this.otpForm.invalid) {
      this.otpForm.markAllAsTouched();
      return;
    }

    const request: ConfirmEmailRequestDTO = {
      token: this.lastToken,
      codeOtp: this.otpForm.value.codeOtp
    };

    this.loadingOtp = true;

    this.emailConfirmationService.confirmEmail(request).subscribe({
      next: (res: ApiResponse<any>) => {
        if (res.success) {
          this.success = true;
          this.showOtpForm = false;
          this.resultMessage = this.isLoggedIn
            ? 'CONFIRMEMAIL.SUCCESS_CONNECTED'
            : 'CONFIRMEMAIL.SUCCESS_DISCONNECTED';
        } else if (res.error?.key) {
          this.handleBackendError(res.error.key, res.error.extraData);

          if (res.error.key === 'TOO_MANY_OTP_RESENDS') {
            const seconds = res.error.extraData?.['remainingWaitTimeSeconds'] ?? 0;
            if (seconds > 0) {
              this.startCountdownAndValidateAfter(seconds);
            }
          }
        }

        this.loadingOtp = false;
      },
      error: (err) => {
        this.handleBackendError(err.key, err.extraData);

        if (err.key === 'TOO_MANY_OTP_RESENDS') {
          const seconds = err.extraData?.['remainingWaitTimeSeconds'] ?? 0;
          if (seconds > 0) {
            this.startCountdownAndValidateAfter(seconds);
          }
        }

        this.loadingOtp = false;
      }
    });
  }

  resendEmail(): void {
    this.validationTriggered = true; // Activer la validation lors de la soumission

    if (this.resendForm.invalid) {
      this.resendForm.markAllAsTouched();
      return;
    }

    this.loadingResend = true;
    const email = this.resendForm.value.email;

    this.emailConfirmationService.resendOtpCodeWithEmail(email).subscribe({
      next: (res) => {
        this.otpTooManyAttempts = false;
        this.validationTriggered = false; // Réinitialiser après succès
        this.fieldError = {}; // Réinitialiser les erreurs
        const meta = (res as any).meta ?? (res as any).data;
        this.remainingResends = meta?.remainingResends ?? this.remainingResends;
        this.otpAttemptsLeft = meta?.otpAttemptsLeft ?? this.otpAttemptsLeft;

        const request: ConfirmEmailRequestDTO = { token: this.lastToken, codeOtp: null as any };
        this.emailConfirmationService.validateToken(request).subscribe({
          next: (r: ApiResponse<any>) => {
            if (r.success) {
              this.showOtpForm = true;
              this.resultMessage = 'CONFIRMEMAIL.OTP_REQUIRED';
            } else {
              this.handleBackendError(r.error?.key, r.error?.extraData);
            }
          }
        });

        this.loadingResend = false;
      },
      error: (err) => {
        this.loadingResend = false;
        // Gérer les erreurs backend spécifiques au champ email
        if (err.key) {
          this.fieldError['email'] = resolveErrorTranslationKey(err.key); // Associer l'erreur au champ email
        }
        this.toast.show(this.translate.instant(resolveErrorTranslationKey(err.key)), 'error');
        this.handleBackendError(err.key, err.extraData);
      }
    });
  }

  resendOtpOnly(): void {
    this.loadingResend = true;

    this.emailConfirmationService.resendOtpCode().subscribe({
      next: (res) => {
        this.otpTooManyAttempts = false;
        const meta = (res as any).meta ?? (res as any).data;
        this.remainingResends = meta?.remainingResends ?? this.remainingResends;
        this.otpAttemptsLeft = meta?.otpAttemptsLeft ?? this.otpAttemptsLeft;

        const request: ConfirmEmailRequestDTO = { token: this.lastToken, codeOtp: null as any };
        this.emailConfirmationService.validateToken(request).subscribe({
          next: (r: ApiResponse<any>) => {
            if (r.success) {
              this.showOtpForm = true;
              this.resultMessage = 'CONFIRMEMAIL.OTP_REQUIRED';
            } else {
              this.handleBackendError(r.error?.key, r.error?.extraData);
            }
          }
        });

        this.loadingResend = false;
      },
      error: (err) => {
        this.toast.show(this.translate.instant(resolveErrorTranslationKey(err.key)), 'error');
        this.handleBackendError(err.key, err.extraData);
        this.loadingResend = false;
      }
    });
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }

  startCountdownAndValidateAfter(seconds: number): void {
    this.remainingWaitTimeSeconds = seconds;

    this.countdownTimer = setInterval(() => {
      this.remainingWaitTimeSeconds--;

      if (this.remainingWaitTimeSeconds <= 0) {
        clearInterval(this.countdownTimer);
        this.validateTokenAfterCountdown();
      }
    }, 1000);
  }

  validateTokenAfterCountdown(): void {
    const request: ConfirmEmailRequestDTO = { token: this.lastToken, codeOtp: null as any };

    this.emailConfirmationService.validateToken(request).subscribe({
      next: (res: ApiResponse<any>) => {
        if (res.success) {
          this.resultMessage = 'CONFIRMEMAIL.OTP_REQUIRED';
          this.showOtpForm = true;
          this.showResend = true;

          const meta = res.data?.metadata;
          this.remainingResends = meta?.remainingResends ?? this.remainingResends;
          this.otpAttemptsLeft = meta?.otpAttemptsLeft ?? this.otpAttemptsLeft;

          this.logger.log('[AutoValidate] ✅ Success - metadata :', meta);
        } else {
          this.handleBackendError(res.error?.key, res.error?.extraData);
        }
      },
      error: (err) => {
        this.logger.error('[AutoValidate] ❌ Erreur HTTP :', err);
        this.handleBackendError(err.key, err.extraData);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.countdownTimer) {
      clearInterval(this.countdownTimer);
    }
    if (this.countdownInterval) {
      clearInterval(this.countdownInterval);
    }
  }
}
