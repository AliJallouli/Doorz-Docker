import { Component, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  AbstractControl,
  FormControl,
  ValidatorFn,
  ValidationErrors,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { InvitationService } from '../../../../core/services/invitation/invitation.service';
import {
  ValidateInvitationTokenRequestDTO,
  ValidateInvitationTokenResponseDTO,
} from '../../../../core/models/invite.models';
import { ToastService } from '../../../../core/services/toast.service';
import { RegisterService } from '../../../../core/services/auth/register.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { resolveErrorTranslationKey } from '../../../../core/errors/error-context-resolver';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { LegalConsentComponent } from '../../../shared/legal-consent/legal-consent.component';
import { RegisterFromInviteRequestDTO } from '../../../../core/models/invite.models';
import { LegalConsentDTO } from '../../../../core/models/legal-document.models';
import {PasswordInputComponent} from '../../../../shared/inputs/password-input/password-input.component';
import {EmailInputComponent} from '../../../../shared/inputs/email-input/email-input.component';
import {NameInputComponent} from '../../../../shared/inputs/name-input/name-input.component';
import {UserService} from '../../../../core/services/user/user.service';
import {LoggerService} from '../../../../core/services/logger/logger.service';
import {CallToActionComponent} from '../../../shared/call-to-action/call-to-action.component';

interface RegisterInviteForm {
  email: FormControl<string>;
  firstName: FormControl<string>;
  lastName: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}

interface DocumentType {
  documentId: number;
  name: string;
}

@Component({
  selector: 'app-register-from-invite',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule, LegalConsentComponent, PasswordInputComponent, EmailInputComponent, NameInputComponent, CallToActionComponent],
  templateUrl: './register-from-invite.component.html',
  styleUrls: ['../register.component.css'],
})
export class RegisterFromInviteComponent implements OnInit {
  pageType: 'register' | 'login' | 'invitation' = 'register';
  form!: FormGroup;
  token: string = '';
  invitationInfo?: ValidateInvitationTokenResponseDTO;
  isLoading = true;
  isValid = false;
  messageError: string = '';
  fieldError: Record<string, string> = {};
  globalError: string = '';
  legalConsents: { [key: string]: boolean } = {};
  documentMap: { [key: string]: DocumentType } = {};
  expectedConsentKeys: string[] = [];
  consentError: boolean = false;
  validationTriggered: boolean = false;

  @ViewChild(LegalConsentComponent) legalConsentComponent?: LegalConsentComponent;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private invitationService: InvitationService,
    private toast: ToastService,
    private translate: TranslateService,
    private authService: AuthService,
    private registerService: RegisterService,
    private userService: UserService,
    private readonly logger: LoggerService,
  ) {}

  ngOnInit(): void {
    this.isLoading = true;

    const params = this.route.snapshot.queryParams;
    const urlToken = params['token'] ? decodeURIComponent(params['token']) : null;

    if (urlToken) {
      sessionStorage.setItem('inviteToken', urlToken);
      this.token = urlToken;
      this.removeTokenFromUrl();
    } else {
      this.token = sessionStorage.getItem('inviteToken') || '';
    }

    if (!this.token) {
      this.messageError = this.translate.instant('INVITE.TOKEN.MISSING');
      this.toast.show(this.messageError, 'error');
      this.isLoading = false;
      return;
    }

    const payload: ValidateInvitationTokenRequestDTO = { invitationToken: this.token };
    this.invitationService.validateToken(payload).subscribe({
      next: (response) => {
        if (response.data) {
          this.invitationInfo = response.data;
          this.isValid = true;
          this.initForm();
        } else {
          this.messageError = this.translate.instant(
            response.error?.key || 'INVITE.TOKEN.INVALID'
          );
          this.toast.show(this.messageError, 'error');
        }
        this.isLoading = false;
      },
      error: (err) => {
        const key = resolveErrorTranslationKey(err.errorKey || 'INTERNAL_SERVER_ERROR');
        this.messageError = this.translate.instant(key);
        this.isLoading = false;
      },
    });
  }

  private removeTokenFromUrl(): void {
    this.router.navigate([], {
      queryParams: {},
      replaceUrl: true,
    });
  }

  initForm(): void {
    this.form = this.fb.group<RegisterInviteForm>(
      {
        email: new FormControl(this.invitationInfo?.email ||'', {
          nonNullable: true,
          validators: [Validators.required, Validators.email],
        }),
        firstName: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.minLength(2)],
        }),
        lastName: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.minLength(2)],
        }),
        password: new FormControl('', {
          nonNullable: true,
          validators: [
            Validators.required,
            Validators.minLength(6),
            Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).+$/),
          ],
        }),
        confirmPassword: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
      },
      { validators: this.passwordMatchValidator() }
    );
  }

  private passwordMatchValidator(): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const password = group.get('password')?.value;
      const confirm = group.get('confirmPassword')?.value;
      return password === confirm ? null : { passwordMismatch: true };
    };
  }

  onConsentsChanged(consents: { [key: string]: boolean }): void {
    this.legalConsents = { ...this.legalConsents, ...consents };
    this.consentError = this.validationTriggered ? !this.areAllConsentsChecked() : false;
  }

  setExpectedConsentKeys(documentTypes: { [key: string]: DocumentType }): void {
    this.documentMap = documentTypes;
    this.expectedConsentKeys = Object.keys(documentTypes);
  }

  areAllConsentsChecked(): boolean {
    return this.expectedConsentKeys.length > 0 && this.expectedConsentKeys.every((key) => this.legalConsents[key]);
  }

  hasMissingConsents(): boolean {
    return this.expectedConsentKeys.some((key: string) => !this.legalConsents[key]);
  }

  onSubmit(): void {
    this.globalError = '';
    this.fieldError = {};
    this.consentError = false;
    this.validationTriggered = true;

    if (this.legalConsentComponent) {
      this.legalConsentComponent.triggerValidation();
    }

    if (!this.areAllConsentsChecked()) {
      this.consentError = true;
      const msg = this.translate.instant('REGISTER.ALL_CONSENTS_REQUIRED');
      this.toast.show(msg, 'error');
    }

    if (this.form.invalid || !this.invitationInfo || this.consentError) {
      this.form.markAllAsTouched();
      return;
    }

    const legalConsents: LegalConsentDTO[] = this.expectedConsentKeys
      .filter((key) => this.legalConsents[key])
      .map((key) => ({
        documentId: this.documentMap[key].documentId,
      }));

    const payload: RegisterFromInviteRequestDTO = {
      firstName: this.form.value.firstName || '',
      lastName: this.form.value.lastName || '',
      invitationToken: this.token,
      email: this.invitationInfo.email,
      roleId: this.invitationInfo.roleId,
      entityId: this.invitationInfo.entityId,
      entityTypeId: this.invitationInfo.entityTypeId,
      password: this.form.value.password,
      legalConsents,
    };

    const isAdminInvite = this.invitationInfo.invitationType === 'EntityAdmin';
    const register$ = isAdminInvite
      ? this.registerService.registerAdminFromInvite(payload)
      : this.registerService.registerColleagueFromInvite(payload);

    register$.subscribe({
      next: (response) => {
        if (response.success) {
          sessionStorage.removeItem('inviteToken');
          const successKey = this.translate.instant(response.success.key);
          this.toast.show(successKey, 'success');
          this.authService.handleLoginSuccess(response.data,true);
          this.userService.loadUser();
          this.authService.authStateReady$.subscribe(() => {
            this.logger.log('✅ Inscription terminée et état d’authentification synchronisé.');
          });
        } else if (response.error) {
          const key = resolveErrorTranslationKey(response.error.key);
          const msg = this.translate.instant(key);
          if (response.error.field) {
            this.fieldError[response.error.field] = key;
            this.form.get(response.error.field)?.setErrors({ backend: true });
          } else {
            this.globalError = key;
          }
        }
      },
      error: (err) => {
        const key = resolveErrorTranslationKey(err.key || 'INTERNAL_SERVER_ERROR');
        const translated = this.translate.instant(key);
        const field = err.field || err?.error?.field || null;

        if (field) {
          this.fieldError[field] = key;
          this.form.get(field)?.setErrors({ backend: true });
        } else {
          this.globalError = key;
          this.toast.show(translated, 'error');
        }
      },
    });
  }
}
