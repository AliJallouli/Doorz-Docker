import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { TranslatedSpokenLanguageDTO } from '../../../models/language.models';
import { LanguageService } from '../../../core/services/language/language.service';
import { AuthService } from '../../../core/services/auth/auth.service';
import { ContactMessageTypeDTO, CreateContactMessageDTO } from '../../../models/support.models';
import { ContactService } from '../../../core/services/support/contact.service';
import { Subscription } from 'rxjs';
import { LegalConsentComponent } from '../../register/legal-consent/legal-consent.component';
import { PhoneInputComponent } from '../../../layers/phone-input/phone-input.component';
import { EmailInputComponent } from '../../../layers/email-input/email-input.component';
import {resolveErrorTranslationKey} from '../../../core/errors/error-context-resolver';
import {LoggerService} from '../../../core/services/logger/logger.service'; // Importez ici

interface DocumentType {
  documentId: number;
  name: string;
}

@Component({
  selector: 'app-contact-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    LegalConsentComponent,
    PhoneInputComponent,
    EmailInputComponent // Ajoutez ici
  ],
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit, OnDestroy {
  @ViewChild(LegalConsentComponent) legalConsentComponent!: LegalConsentComponent;

  contactForm!: FormGroup;
  submitted = false;
  successMessage = '';
  consentError = false;
  consentsTouched = false;
  hasInteracted = false;
  validationTriggered = false;
  fieldError: { [key: string]: string } = {};
  isLoggedIn = false;
  languages: TranslatedSpokenLanguageDTO[] = [];
  contactMessageTypes: ContactMessageTypeDTO[] = [];
  private langChangeSub!: Subscription;
  showSubjectField = false;
  otherTypeId: number | null = null;
  selectedTypeDescription: string | null = null;
  selectedTypeKeyName: string | null = null;
  selectedType: ContactMessageTypeDTO | undefined;
  legalConsents: { [key: string]: boolean } = {
    TermsOfService: false,
    PrivacyPolicy: false,
    LegalNotice: false
  };
  documentMap: { [key: string]: DocumentType } = {};
  expectedConsentKeys: string[] = [];
  requiredConsentKey: string | null = null;

  constructor(
    private fb: FormBuilder,
    private languageService: LanguageService,
    private contactService: ContactService,
    private authService: AuthService,
    private translateService: TranslateService,
    private readonly logger: LoggerService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticatedSync();

    const fullNameValidators = this.isLoggedIn ? [] : [Validators.required];
    const emailValidators = this.isLoggedIn ? [] : [Validators.required, Validators.email];

    this.contactForm = this.fb.group({
      fullName: [null as string | null, fullNameValidators],
      email: [null as string | null, emailValidators],
      subject: [null as string | null],
      message: [null as string | null, [Validators.required, Validators.minLength(10)]],
      languageId: [null as number | null, [Validators.required]],
      contactMessageTypeId: [null as number | null, Validators.required],
      countryCode: ['+32', Validators.required],
      phoneNumber: ['', [Validators.pattern(/^[0-9\s]{6,15}$/)]],
    });

    // Charger les langues
    this.loadLanguages(this.languageService.getLanguage());

    // Charger les types de message
    this.loadMessageTypes();

    // Écouter les changements de langue
    this.langChangeSub = this.translateService.onLangChange.subscribe((event) => {
      const newLang = event.lang;
      const currentTypeId = this.contactForm.get('contactMessageTypeId')?.value;
      this.updateLanguage(newLang);
      this.loadMessageTypes(currentTypeId);
    });
  }
  get f(): { [key: string]: AbstractControl } {
    return this.contactForm.controls;
  }

  isFieldTouched(field: string): boolean {
    const control = this.contactForm.get(field);
    return control ? control.touched : false;
  }

  isFieldInvalid(field: string): boolean {
    const control = this.contactForm.get(field);
    return control ? control.invalid : false;
  }

  onTypeChange(): void {
    const selectedTypeId = this.contactForm.get('contactMessageTypeId')?.value;

    const selectedType = this.contactMessageTypes.find(t => t.contactMessageTypeId === selectedTypeId);

    if (selectedType) {
      this.selectedType = selectedType;
      this.selectedTypeKeyName = selectedType.keyName || null;
      this.selectedTypeDescription = selectedType.description || null;
      this.showSubjectField = selectedType.contactMessageTypeId === this.otherTypeId;
    } else {
      this.selectedType = undefined;
      this.selectedTypeKeyName = null;
      this.selectedTypeDescription = null;
      this.showSubjectField = false;
    }

    this.updateSubjectFieldValidation();
  }

  ngOnDestroy(): void {
    this.langChangeSub?.unsubscribe();
  }

  loadLanguages(lang: string): void {
    this.languageService.getAvailableLanguages().subscribe({
      next: (response) => {
        this.languages = response.data || [];
        this.updateLanguage(lang);
      },
      error: () => {
        this.languages = [];
        this.contactForm.patchValue({ languageId: null });
      }
    });
  }

  updateLanguage(lang: string): void {
    if (!this.languages.length) {
      return;
    }

    const selectedLang = this.languages.find(l => l.code === lang);
    if (selectedLang) {
      this.contactForm.patchValue({ languageId: selectedLang.languageId }, { emitEvent: false });
    } else if (this.languages.length) {
      this.contactForm.patchValue({ languageId: this.languages[0].languageId }, { emitEvent: false });
    } else {
      this.contactForm.patchValue({ languageId: null }, { emitEvent: false });
    }
  }

  loadMessageTypes(restoreTypeId?: number): void {
    this.logger.log('loadMessageTypes called with restoreTypeId:', restoreTypeId);
    this.logger.log('Current selectedTypeKeyName:', this.selectedTypeKeyName);
    this.logger.log('Current form contactMessageTypeId:', this.contactForm.get('contactMessageTypeId')?.value);


    this.contactService.getMessageTypes().subscribe({
      next: (response) => {
        this.contactMessageTypes = (response.data || []).map(t => ({
          ...t,
          contactMessageTypeId: +t.contactMessageTypeId // 👈 FORCÉ en number
        }));

        // Définir otherTypeId pour le type "other"
        const otherType = this.contactMessageTypes.find(t => t.keyName === 'other');
        this.otherTypeId = otherType ? otherType.contactMessageTypeId : null;

        // Récupérer l'ID ou le keyName actuel pour la restauration
        const currentTypeId = restoreTypeId ?? this.contactForm.get('contactMessageTypeId')?.value;
        let selectedType: ContactMessageTypeDTO | undefined;

        if (currentTypeId) {
          // Essayer de trouver le type par ID
          selectedType = this.contactMessageTypes.find(t => t.contactMessageTypeId === currentTypeId);
          if (!selectedType && this.selectedTypeKeyName) {
            // Si pas trouvé par ID, essayer par keyName
            selectedType = this.contactMessageTypes.find(t => t.keyName === this.selectedTypeKeyName);
          }
        }

        if (selectedType) {
          // Type trouvé, restaurer la sélection
          this.contactForm.patchValue({ contactMessageTypeId: selectedType.contactMessageTypeId }, { emitEvent: false });
          this.selectedType = selectedType;
          this.selectedTypeKeyName = selectedType.keyName || null;
          this.selectedTypeDescription = selectedType.description || null;
          this.showSubjectField = selectedType.contactMessageTypeId === this.otherTypeId;
          this.updateSubjectFieldValidation();
        } else {
          // Aucun type correspondant, définir le type par défaut
          this.setDefaultType();
        }
      },
      error: () => {
        this.contactMessageTypes = [];
        this.contactForm.patchValue({ contactMessageTypeId: null });
        this.setDefaultType();
      }
    });
    this.logger.log(
      'Form value =', this.contactForm.value.contactMessageTypeId,
      'typeof =', typeof this.contactForm.value.contactMessageTypeId
    );

    this.logger.log(
      'Option types =', this.contactMessageTypes.map(t => [t.contactMessageTypeId, typeof t.contactMessageTypeId])
    );
    this.logger.log('TYPE ID types:', this.contactMessageTypes.map(t => [t.contactMessageTypeId, typeof t.contactMessageTypeId]));

  }
  onSubmit(): void {
    this.submitted = true;
    this.hasInteracted = true;
    this.consentsTouched = true;
    this.validationTriggered = true;
    this.fieldError = {};
    this.contactForm.markAllAsTouched();

    if (this.legalConsentComponent) {
      this.legalConsentComponent.triggerValidation();
    }

    if (this.requiredConsentKey && this.legalConsents[this.requiredConsentKey] !== true) {
      this.consentError = true;
      return;
    }

    if (this.contactForm.invalid) {
      return;
    }

    let fullPhone: string | null = null;
    if (this.f['countryCode'].value && this.f['phoneNumber'].value?.trim() !== '') {
      const cleanPhone = this.f['phoneNumber'].value.replace(/\s/g, '');
      fullPhone = this.f['countryCode'].value + cleanPhone;
    }

    const formData: CreateContactMessageDTO = {
      fullName: this.isLoggedIn ? '' : (this.f['fullName'].value || ''),
      email: this.isLoggedIn ? '' : (this.f['email'].value || ''),
      subject: this.showSubjectField ? (this.f['subject'].value || '') : '',
      message: this.f['message'].value || '',
      languageId: this.f['languageId'].value || 0,
      contactMessageTypeId: this.f['contactMessageTypeId'].value || 0,
      phone: fullPhone && fullPhone.trim() !== '' ? fullPhone : undefined,
    };

    if (formData.subject == null) {
      formData.subject = '';
    }

    this.contactService.sendMessage(formData).subscribe({
      next: (res) => {
        this.successMessage = res.success?.key || 'CONTACT.MESSAGE_SENT_SUCCESS';
        this.resetFormState();
      },
      error: (err) => {
        // Gérer les erreurs backend spécifiques au champ email
        if (err.key === 'EMAIL_INVALID' || err.key === 'EMAIL_NOT_FOUND') {
          this.fieldError['email'] = resolveErrorTranslationKey(err.key);
        } else {
          this.successMessage = ''; // Réinitialiser le message de succès
          this.fieldError['general'] = resolveErrorTranslationKey(err.key || 'CONTACT.ERROR.GENERIC');
        }
      }
    });
  }

  private resetFormState(): void {
    this.submitted = false;
    this.consentError = false;
    this.consentsTouched = false;
    this.hasInteracted = false;
    this.validationTriggered = false; // Réinitialiser après soumission
    this.fieldError = {}; // Réinitialiser les erreurs
    this.legalConsents = {
      TermsOfService: false,
      PrivacyPolicy: false,
      LegalNotice: false
    };

    const currentLang = this.languageService.getLanguage();
    const selectedLang = this.languages.find(l => l.code === currentLang);

    const currentCountryCode = this.contactForm.value.countryCode || '+32';

    this.contactForm.reset({
      fullName: null,
      email: null,
      subject: null,
      message: null,
      languageId: selectedLang ? selectedLang.languageId : null,
      contactMessageTypeId: null,
      countryCode: currentCountryCode,
      phoneNumber: ''
    });

    this.setDefaultType();
    this.contactForm.markAsPristine();
    this.contactForm.markAsUntouched();
    this.contactForm.updateValueAndValidity();
  }

  private setDefaultType(): void {
    if (this.contactMessageTypes.length) {
      const defaultType = this.contactMessageTypes[0];
      this.contactForm.patchValue({ contactMessageTypeId: defaultType.contactMessageTypeId }, { emitEvent: false });
      this.selectedType = defaultType;
      this.selectedTypeKeyName = defaultType.keyName || null;
      this.selectedTypeDescription = defaultType.description || null;
      this.showSubjectField = defaultType.contactMessageTypeId === this.otherTypeId;
      this.updateSubjectFieldValidation();
    } else {
      this.selectedType = undefined;
      this.selectedTypeKeyName = null;
      this.selectedTypeDescription = null;
      this.showSubjectField = false;
      this.contactForm.patchValue({ contactMessageTypeId: null }, { emitEvent: false });
    }
  }

  private updateSubjectFieldValidation(): void {
    if (this.showSubjectField) {
      this.contactForm.get('subject')?.setValidators([Validators.required]);
    } else {
      this.contactForm.get('subject')?.clearValidators();
    }
    this.contactForm.get('subject')?.updateValueAndValidity();
  }

  onConsentsChanged(consents: { [key: string]: boolean }): void {
    this.legalConsents = { ...this.legalConsents, ...consents };
    this.consentError = this.requiredConsentKey && this.validationTriggered ? this.legalConsents[this.requiredConsentKey] !== true : false;
  }

  setExpectedConsentKeys(documentTypes: { [key: string]: DocumentType }): void {
    this.documentMap = documentTypes;
    this.expectedConsentKeys = Object.keys(documentTypes);
    this.requiredConsentKey = this.expectedConsentKeys[0] || null;
  }

  sanitizeFullName(): void {
    const value = this.contactForm.get('fullName')?.value || '';
    const sanitized = value.replace(/[^a-zA-ZÀ-ÿ\u00C0-\u017F\s'-]/g, '');
    this.contactForm.get('fullName')?.setValue(sanitized, { emitEvent: false });
  }
  compareById = (a: number | null, b: number | null): boolean => {
    return a === b;
  };

}
