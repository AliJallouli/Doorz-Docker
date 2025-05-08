import { Component, Input, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ValidatorFn,
  AbstractControl,
  FormControl,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { RegisterService } from '../../../core/services/auth/register.service';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth/auth.service';
import { LegalConsentComponent } from '../legal-consent/legal-consent.component';
import { resolveErrorTranslationKey } from '../../../core/errors/error-context-resolver';
import {
  RoleIdRequestDTO,
  RegisterPublicRequestDTO,
  LoginResponse,
  ApiResponse,
  RoleIdResponseDTO,
} from '../../../models/auth.models';
import { ActivatedRoute } from '@angular/router';
import { LegalConsentDTO } from '../../../models/legal-document.models';
import {PasswordInputComponent} from '../../../layers/password-input/password-input.component';
import {EmailInputComponent} from '../../../layers/email-input/email-input.component';
import {LoggerService} from '../../../core/services/logger/logger.service';

interface RegisterPublicForm {
  firstName: FormControl<string>;
  lastName: FormControl<string>;
  email: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
}

interface DocumentType {
  documentId: number;
  name: string;
}

@Component({
  selector: 'app-register-form-public',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule, LegalConsentComponent, PasswordInputComponent, EmailInputComponent],
  templateUrl: './register-form-public.component.html',
  styleUrls: ['./register-form-public.component.css'],
})
export class RegisterFormPublicComponent implements OnInit {
  @Input() roleName: 'student' | 'privateLandlord' = 'student';
  @ViewChild(LegalConsentComponent) legalConsentComponent!: LegalConsentComponent;

  legalConsents: { [key: string]: boolean } = {
    TermsOfService: false,
    PrivacyPolicy: false,
    LegalNotice: false,
  };
  documentMap: { [key: string]: DocumentType } = {};
  expectedConsentKeys: string[] = [];
  consentError: boolean = false;
  validationTriggered: boolean = false;
  form!: FormGroup;
  globalError: string = '';
  fieldError: Record<string, string> = {};
  loading = true;

  private roleId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService,
    private toast: ToastService,
    private translate: TranslateService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private readonly logger: LoggerService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const role = params.get('roleName');
      this.roleName = role === 'privateLandlord' ? 'privateLandlord' : 'student';
      this.initForm();
      this.loadRoleId();
    });
  }

  private loadRoleId(): void {
    const request: RoleIdRequestDTO = {
      roleName: this.roleName,
      entityTypeName: 'Public',
    };

    this.registerService.getRoleId(request).subscribe({
      next: (res: ApiResponse<RoleIdResponseDTO>) => {
        const roleId = res.data?.roleId;
        if (roleId !== undefined && roleId !== null) {
          this.roleId = roleId;
        } else {
          this.globalError = 'Erreur : roleId manquant';
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        const key = resolveErrorTranslationKey(err.key || 'INTERNAL_SERVER_ERROR');
        this.globalError = key;
        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }

  private initForm(): void {
    this.form = this.fb.group<RegisterPublicForm>(
      {
        firstName: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.minLength(2)],
        }),
        lastName: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.minLength(2)],
        }),
        email: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.email],
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
      {
        validators: this.passwordMatchValidator(),
      }
    );

    // Débogage des changements de valeur
    this.form.get('password')?.valueChanges.subscribe(value => {
      this.logger.log('Password value changed:', value);
      this.logger.log('Password errors:', this.form.get('password')?.errors);
      this.logger.log('Form errors:', this.form.errors);
    });

    this.form.get('confirmPassword')?.valueChanges.subscribe(value => {
      this.logger.log('Confirm password value changed:', value);
      this.logger.log('Confirm password errors:', this.form.get('confirmPassword')?.errors);
      this.logger.log('Form errors:', this.form.errors);
    });
  }
  private passwordMatchValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const group = control as FormGroup;
      const password = group.get('password')?.value;
      const confirm = group.get('confirmPassword')?.value;
      return password === confirm ? null : { passwordMismatch: true };
    };
  }

  areAllConsentsChecked(): boolean {
    return (
      this.expectedConsentKeys.length > 0 &&
      this.expectedConsentKeys.every((key) => this.legalConsents[key] === true)
    );
  }

  onConsentsChanged(consents: { [key: string]: boolean }): void {
    this.legalConsents = { ...this.legalConsents, ...consents };
    this.consentError = this.validationTriggered ? !this.areAllConsentsChecked() : false;
    this.cdr.detectChanges();
  }

  setExpectedConsentKeys(documentTypes: { [key: string]: DocumentType }): void {
    this.documentMap = documentTypes;
    this.expectedConsentKeys = Object.keys(documentTypes);
    this.expectedConsentKeys.forEach((key) => {
      if (!(key in this.legalConsents)) {
        this.legalConsents[key] = false;
      }
    });
    this.cdr.detectChanges();
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
    }

    if (this.form.invalid || !this.roleId || this.consentError) {
      this.form.markAllAsTouched();
      if (!this.roleId) {
        this.globalError = 'Erreur : roleId manquant';
      }
      this.cdr.detectChanges();
      return;
    }

    const legalConsents: LegalConsentDTO[] = this.expectedConsentKeys
      .filter((key) => this.legalConsents[key])
      .map((key) => ({
        documentId: this.documentMap[key].documentId,
      }));

    const payload: RegisterPublicRequestDTO = {
      firstName: this.form.value.firstName || '',
      lastName: this.form.value.lastName || '',
      email: this.form.value.email || '',
      roleId: this.roleId,
      password: this.form.value.password || '',
      legalConsents,
    };

    this.registerService.registerPublic(payload).subscribe({
      next: (res: ApiResponse<LoginResponse>) => {
        if (res.success && res.data) {
          const msg = this.translate.instant(res.success.key);
          this.toast.show(msg, 'success');
          this.authService.handleLoginSuccess(res.data);
          this.resetForm();
        } else if (res.error) {
          const key = resolveErrorTranslationKey(res.error.key);
          const msg = this.translate.instant(key);
          const field = res.error.field;
          if (field && this.form.get(field)) {
            this.fieldError[field] = key;
            this.form.get(field)?.setErrors({ backend: true });
          } else {
            this.globalError = key;
            this.toast.show(msg, 'error');
          }
          this.cdr.detectChanges();
        }
      },
      error: (err) => {
        const key = resolveErrorTranslationKey(err.key || 'INTERNAL_SERVER_ERROR');
        const translated = this.translate.instant(key);
        const field = err.field;
        if (field && this.form.get(field)) {
          this.fieldError[field] = key;
          this.form.get(field)?.setErrors({ backend: true });
        } else {
          this.globalError = key;
          this.toast.show(translated, 'error');
        }
        this.cdr.detectChanges();
      },
    });
  }

  private resetForm(): void {
    this.form.reset();
    this.globalError = '';
    this.fieldError = {};
    this.consentError = false;
    this.validationTriggered = false;
    this.legalConsents = {
      TermsOfService: false,
      PrivacyPolicy: false,
      LegalNotice: false,
    };
    if (this.legalConsentComponent) {
      this.legalConsentComponent.reset();
    }
    this.form.markAsPristine();
    this.form.markAsUntouched();
    this.cdr.detectChanges();
  }

  hasMissingConsents(): boolean {
    return this.expectedConsentKeys.some((key) => !this.legalConsents[key]);
  }
}
