import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {InstitutionType, InvitationService} from '../../../../core/services/invitation/invitation.service';
import {TranslateModule, TranslateService} from '@ngx-translate/core';
import {LoggerService} from '../../../../core/services/logger/logger.service';
import {RadioInputComponent} from '../../../../shared/inputs/radio-input/radio-input.component';
import {NgClass, NgForOf, NgIf} from '@angular/common';
import {EmailInputComponent} from '../../../../shared/inputs/email-input/email-input.component';
import {CreateInvitationRequestDto} from '../../../../core/models/invite.models';
import {SharedValidators} from '../../../../core/utils/Validators/shared-validators';
import {resolveErrorTranslationKey} from '../../../../core/errors/error-context-resolver';
import {ApiResponse} from '../../../../core/models/auth.models';
import {CallToActionComponent} from '../../../shared/call-to-action/call-to-action.component';

interface EntityType {
  name: string;
  nameTranslation: string;

}
interface RadioOption {
  label: string;
  value: string | number;
}
@Component({
  selector: 'app-invitation-request',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RadioInputComponent,
    NgIf,
    TranslateModule,
    NgClass,
    EmailInputComponent,
    NgForOf,
    CallToActionComponent
  ],
  templateUrl: './invitation-request.component.html',
  styleUrls: ['../register.component.css']
})
export class InvitationRequestComponent implements OnInit {
  pageType: 'register' | 'login' | 'invitation' = 'invitation';
  form!: FormGroup;
  submitted = false;
  fieldError: { [key: string]: string } = {};
  globalError: string | null = null;
  entityTypes: EntityType[] | undefined = [];
  entityTypeOptions: RadioOption[] = [];
  institutionTypes: InstitutionType[] = [];

  constructor(
    private fb: FormBuilder,
    private invitationService: InvitationService,
    private readonly logger: LoggerService,
  private translate: TranslateService,
  ) {}

  ngOnInit(): void {

    this.translate.onLangChange.subscribe(() => {

        this.loadEntityTypes();
        this.loadInstitutionTypes()
    });
    this.form = this.fb.group({
      entityTypeName: ['', Validators.required],
      name: ['', Validators.required],
      invitationEmail: ['', [Validators.required, SharedValidators.email()]],
      companyNumber: [''],
      institutionTypeId: ['']
    });

    this.form.get('entityTypeName')?.valueChanges.subscribe((type) => {
      const companyNumberControl = this.form.get('companyNumber');
      const institutionTypeIdControl = this.form.get('institutionTypeId');

      if (type === 'Company') {
        companyNumberControl?.setValidators([
          Validators.required,
          Validators.pattern(/^\d{4}\.\d{3}\.\d{3}$/)
        ]);
      } else {
        companyNumberControl?.clearValidators();
        companyNumberControl?.setValue('');
      }
      companyNumberControl?.updateValueAndValidity();

      if (type === 'Institution') {
        institutionTypeIdControl?.setValidators([Validators.required]);
      } else {
        institutionTypeIdControl?.clearValidators();
        institutionTypeIdControl?.setValue(null);
      }
      institutionTypeIdControl?.updateValueAndValidity();
    });



    this.loadEntityTypes();
    this.loadInstitutionTypes();
  }

  loadInstitutionTypes(): void {
    this.invitationService.getInstitutionTypes().subscribe({
      next: (data) => (this.institutionTypes = data),
      error: (err) => this.logger.error('Error loading institution types:', err)
    });
  }
  onSubmit(): void {
    this.submitted = true;
    this.fieldError = {};
    this.globalError = null;

    if (this.form.invalid) return;

    const raw = this.form.value;

    const payload: CreateInvitationRequestDto = {
      ...raw,
      companyNumber: raw.companyNumber?.trim() || null,
      institutionTypeId: raw.institutionTypeId || null
    };


    this.invitationService.submitInvitationRequest(payload).subscribe({
      next: (response) => {
        this.logger.log('Demande envoyée avec succès', response);
        // Optionnel : afficher un message ou rediriger
      },
      error: (err) => {
        this.logger.error('Erreur lors de la soumission de la demande', err);
        this.globalError = resolveErrorTranslationKey(err.key);

      }
    });
  }


  loadEntityTypes(): void {
    this.invitationService.getEntityTypes().subscribe({
      next: (response) => {
        console.log('Types d\'entités reçus :', response.data);
        this.entityTypes = response.data ?? [];
        this.entityTypeOptions = (this.entityTypes ?? []).map((type) => ({
          value: type.name,
          label: type.nameTranslation
        }));
      },
      error: (error) => {
        console.error('Erreur lors du chargement des types d\'entités :', error);
        console.error('Détails de l\'erreur :', error.status, error.error);
      }
    });
  }



}
