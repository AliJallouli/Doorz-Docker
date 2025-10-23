import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { Subscription } from 'rxjs';
import {
  InvitationService,
  InstitutionType,
  Role,
} from '../../../../core/services/invitation/invitation.service';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { resolveErrorTranslationKey } from '../../../../core/errors/error-context-resolver';
import { EmailInputComponent } from '../../../../shared/inputs/email-input/email-input.component';
import {LoggerService} from '../../../../core/services/logger/logger.service';
import {ApiResponse} from '../../../../core/models/auth.models';



@Component({
  selector: 'app-invitation-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    NgForOf,
    NgClass,
    TranslateModule,
    EmailInputComponent
  ],
  templateUrl: './invitation-form.component.html',
  styleUrls: ['./invitation-form.component.css']
})
export class InvitationFormComponent implements OnInit, OnDestroy {
  invitationForm: FormGroup;
  fieldError: { [key: string]: string } = {};
  validationTriggered = false;

  roles: Role[] = [];

  isSuperadmin: boolean = false;
  currentRole: string | null = null;
  currentSubRole: string | null = null;

  private subscriptions: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private entityService: InvitationService,
    private authService: AuthService,
    private translate: TranslateService,
    private readonly logger: LoggerService
  ) {
    this.invitationForm = this.fb.group({
      entity: [null],
      name: [''],
      institutionTypeId: [null],
      companyNumber: [''],
      invitationEmail: ['', [Validators.required, Validators.email]],
      role: ['']
    });
  }

  ngOnInit(): void {
    this.translate.onLangChange.subscribe(() => {
      if (this.currentRole) {
        this.reloadTranslatedData();
      }
    });

    this.currentRole = this.authService.getCurrentRoleSync();
    this.currentSubRole = this.authService.getCurrentSubRoleSync();
    this.isSuperadmin = this.currentRole === 'superadmin';
    this.updateFormValidators();

    this.subscriptions.add(
      this.authService.getRole().subscribe(role => {
        this.currentRole = role;
        this.isSuperadmin = this.currentRole === 'superadmin';
        this.updateFormValidators();
        if (this.isSuperadmin) {
        } else if (this.currentRole && this.currentSubRole === 'admin') {
          this.loadRoles();
        }
      })
    );

    this.subscriptions.add(
      this.authService.getSubRole().subscribe(subRole => {
        this.currentSubRole = subRole;
        if (!this.isSuperadmin && this.currentRole && this.currentSubRole === 'admin') {
          this.loadRoles();
        }
      })
    );



  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }


  loadRoles(): void {
    if (this.currentRole) {
      this.entityService.getRolesByEntityTypeName(this.currentRole).subscribe({
        next: (data) => (this.roles = data),
        error: (err) => this.logger.error('Error loading roles:', err)
      });
    }
  }

  onSubmit(): void {
    this.validationTriggered = true;
    this.fieldError = {};

    if (this.invitationForm.invalid) {
      this.invitationForm.markAllAsTouched();
      this.toast.show(this.translate.instant('INVITE.ERROR.FRONTEND.MISSING_FIELDS'), 'error');
      return;
    }

    const formValue = this.invitationForm.value;

    if (this.isSuperadmin) {
      if (formValue.entity?.name?.toLowerCase() === 'company') {
        const companyData = {
          Name: formValue.name,
          InvitationEmail: formValue.invitationEmail,
          CompanyNumber: formValue.companyNumber
        };
        this.entityService.inviteCompany(companyData).subscribe({
          next: () => {
            this.toast.show(this.translate.instant('INVITE.SUCCESS.COMPANY'), 'success');
            this.resetForm();
          },
          error: (err) => {
            const key = resolveErrorTranslationKey(err.key);
            if (err.key === 'EMAIL_INVALID' || err.key === 'EMAIL_ALREADY_INVITED') {
              this.fieldError['invitationEmail'] = key;
            } else {
              this.toast.show(this.translate.instant(key), 'error');
            }
            if (err?.field) {
              document.getElementById(err.field)?.focus();
            }
          }
        });
      } else if (formValue.entity?.name?.toLowerCase() === 'institution') {
        const institutionData = {
          Name: formValue.name,
          InstitutionTypeId: formValue.institutionTypeId,
          InvitationEmail: formValue.invitationEmail
        };
        this.entityService.inviteInstitution(institutionData).subscribe({
          next: () => {
            this.toast.show(this.translate.instant('INVITE.SUCCESS.INSTITUTION'), 'success');
            this.resetForm();
          },
          error: (err) => {
            const key = resolveErrorTranslationKey(err.key);
            if (err.key === 'EMAIL_INVALID' || err.key === 'EMAIL_ALREADY_INVITED') {
              this.fieldError['invitationEmail'] = key;
            } else {
              this.toast.show(this.translate.instant(key), 'error');
            }
            if (err?.field) {
              document.getElementById(err.field)?.focus();
            }
          }
        });
      }
    } else {
      const colleagueData = {
        Email: formValue.invitationEmail,
        RoleId: +formValue.role
      };
      this.entityService.inviteColleague(colleagueData).subscribe({
        next: () => {
          this.toast.show(this.translate.instant('INVITE.SUCCESS.COLLEAGUE'), 'success');
          this.resetForm();
        },
        error: (err) => {
          const key = resolveErrorTranslationKey(err.key || err);
          if (err.key === 'EMAIL_INVALID' || err.key === 'EMAIL_ALREADY_INVITED') {
            this.fieldError['invitationEmail'] = key;
          } else {
            this.toast.show(this.translate.instant(key), 'error');
          }
          if (err?.field) {
            document.getElementById(err.field)?.focus();
          }
        }
      });
    }
  }

  private resetForm(): void {
    this.invitationForm.reset({
      entity: null,
      name: '',
      institutionTypeId: null,
      companyNumber: '',
      invitationEmail: '',
      role: ''
    });
    this.validationTriggered = false;
    this.fieldError = {};
  }

  private reloadTranslatedData(): void {
    if (this.isSuperadmin) {
    }

    if (this.currentRole && this.currentSubRole === 'admin') {
      this.loadRoles();
    }

    if (this.isSuperadmin && this.invitationForm.get('entity')?.value?.name?.toLowerCase() === 'institution') {

    }
  }

  private updateFormValidators(): void {
    if (this.isSuperadmin) {
      this.invitationForm.get('entity')?.setValidators([Validators.required]);
      this.invitationForm.get('name')?.setValidators([Validators.required]);
      this.invitationForm.get('role')?.clearValidators();
    } else {
      this.invitationForm.get('entity')?.clearValidators();
      this.invitationForm.get('name')?.clearValidators();
      this.invitationForm.get('companyNumber')?.clearValidators();
      this.invitationForm.get('institutionTypeId')?.clearValidators();
      this.invitationForm.get('role')?.setValidators([Validators.required]);
    }
    this.invitationForm.get('entity')?.updateValueAndValidity();
    this.invitationForm.get('name')?.updateValueAndValidity();
    this.invitationForm.get('role')?.updateValueAndValidity();
  }
  get entityName(): string | null {
    const entity = this.invitationForm.get('entity')?.value;
    return entity?.name?.toLowerCase?.() || (typeof entity === 'string' ? entity.toLowerCase() : null);
  }

}
