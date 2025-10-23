import { Component } from '@angular/core';
import {ActionButtonsComponent} from "../../../../shared/buttons/action-buttons/action-buttons.component";
import {ChangeEmailFormComponent} from "./change-email-form/change-email-form.component";
import {ChangeNameFormComponent} from "./change-name-form/change-name-form.component";
import {ChangePasswordFormComponent} from "./change-password-form/change-password-form.component";
import {NgIf} from "@angular/common";
import {TranslatePipe} from "@ngx-translate/core";
import {User} from '../../../../core/models/user.model';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {interval, Subscription} from 'rxjs';
import {UserService} from '../../../../core/services/user/user.service';
import {LoggerService} from '../../../../core/services/logger/logger.service';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {SharedValidators} from '../../../../core/utils/Validators/shared-validators';
import {UpdateEmailRequestDto, UpdatePasswordRequestDTO} from '../../../../core/models/auth.models';
import {resolveErrorTranslationKey} from '../../../../core/errors/error-context-resolver';

interface UpdateNameEvent {
  currentPassword: string;
  firstName: string;
  lastName: string;
}

interface UpdateEmailEvent {
  currentPassword: string;
  newEmail: string;
}
@Component({
  selector: 'app-account',
  imports: [
    ActionButtonsComponent,
    ChangeEmailFormComponent,
    ChangeNameFormComponent,
    ChangePasswordFormComponent,
    NgIf,
    TranslatePipe,
  ],
  templateUrl: './account.component.html',
  styleUrl: '../settings.component.css'
})
export class AccountComponent {
  user: User | null = null;
  form: FormGroup = new FormGroup({});
  editSection: 'name' | 'email' | 'password' | null = null;
  fieldError: { [key: string]: string } = {};
  errorMessage: string | null = null;
  errorParams: { [key: string]: number } = {};
  successMessage: string | null = null;
  private remainingSeconds: number = 0;
  private countdownSubscription: Subscription | null = null;

  constructor(
    private userService: UserService,
    private logger: LoggerService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.user$.subscribe(user => {
      this.user = user;
      this.logger.log('Utilisateur mis à jour dans le composant :', user);
      if (this.editSection) {
        if (this.editSection === 'name') {
          this.initNameForm();
        } else if (this.editSection === 'email') {
          this.initEmailForm();
        }
      }
    });

    const params = this.route.snapshot.queryParams;
    this.logger.log('Paramètres de l’URL au snapshot :', params);
    const subsection = params['subsection'];
    if (subsection && ['name', 'email', 'password'].includes(subsection)) {
      this.editSection = subsection as 'name' | 'email' | 'password';
      this.logger.log('Section restaurée depuis l’URL (subsection) :', this.editSection);

      if (this.editSection === 'name') {
        this.initNameForm();
      } else if (this.editSection === 'email') {
        this.initEmailForm();
      } else if (this.editSection === 'password') {
        this.initPasswordForm();
      }
    } else {
      this.editSection = null;
      this.form.reset();
      this.logger.log('Aucune sous-section valide spécifiée dans l’URL, editSection réinitialisé');
    }

    this.userService.loadUser();
    this.cleanUrl();
  }

  ngOnDestroy(): void {
    this.stopCountdown();
  }

  initNameForm(): void {
    this.form = new FormGroup({
      currentPassword: new FormControl('', Validators.required),
      firstName: new FormControl(this.user?.firstName || '', [Validators.required, Validators.minLength(2)]),
      lastName: new FormControl(this.user?.lastName || '', [Validators.required, Validators.minLength(2)])
    });
  }

  initEmailForm(): void {
    this.form = new FormGroup(
      {
        currentPassword: new FormControl('', Validators.required),
        newEmail: new FormControl('', [Validators.required, SharedValidators.email()]),
        confirmEmail: new FormControl('', Validators.required)
      },
      {
        validators: this.emailMatchValidator('newEmail', 'confirmEmail')
      }
    );
  }

  initPasswordForm(): void {
    this.form = new FormGroup(
      {
        currentPassword: new FormControl('', Validators.required),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).+$/)
        ]),
        confirmPassword: new FormControl('', Validators.required)
      },
      {
        validators: this.passwordMatchValidator
      }
    );
  }

  private emailMatchValidator(emailControlName: string, confirmEmailControlName: string): ValidatorFn {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const email = formGroup.get(emailControlName)?.value;
      const confirmEmail = formGroup.get(confirmEmailControlName)?.value;

      if (!email || !confirmEmail) return null;

      return email === confirmEmail ? null : { emailMismatch: true };
    };
  }

  private passwordMatchValidator: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    if (!password || !confirmPassword) return null;

    return password === confirmPassword ? null : { passwordMismatch: true };
  };

  onNameUpdated(event: UpdateNameEvent | null): void {
    if (!event) {
      this.closeEditSection();
      return;
    }

    if (
      event.firstName.trim() === this.user?.firstName?.trim() &&
      event.lastName.trim() === this.user?.lastName?.trim()
    ) {
      this.logger.log('Aucun changement détecté pour le nom et le prénom');
      this.errorMessage = 'ACCOUNT.NO_CHANGES';
      this.errorParams = {};
      return;
    }

    const payload = {
      currentPassword: event.currentPassword,
      newFirstName: event.firstName,
      newLastName: event.lastName
    };

    this.userService.updateName(payload).subscribe({
      next: () => {
        this.logger.log('Nom mis à jour avec succès');
        this.successMessage = 'ACCOUNT.NAME_UPDATE_SUCCESS';
        this.errorMessage = null;
        this.errorParams = {};
        this.stopCountdown();
        this.closeEditSection();
      },
      error: (err) => this.handleError(err)
    });
  }

  onEmailUpdated(event: UpdateEmailEvent | null): void {
    if (!event) {
      this.closeEditSection();
      return;
    }

    if (event.newEmail.trim() === this.user?.email?.trim()) {
      this.logger.log('Aucun changement détecté pour l’email');
      this.errorMessage = 'ACCOUNT.NO_CHANGES';
      this.errorParams = {};
      return;
    }

    this.errorMessage = null;
    this.errorParams = {};
    this.successMessage = null;

    const payload: UpdateEmailRequestDto = {
      currentPassword: event.currentPassword,
      newEmail: event.newEmail
    };

    this.userService.updateEmail(payload).subscribe({
      next: () => {
        this.logger.log('Email mis à jour avec succès');
        this.successMessage = 'ACCOUNT.EMAIL_UPDATE_SUCCESS';
        this.stopCountdown();
        this.closeEditSection();
      },
      error: (err) => this.handleError(err)
    });
  }

  onPasswordUpdated(event: { currentPassword: string; newPassword: string } | null): void {
    if (!event) {
      this.closeEditSection();
      return;
    }

    this.errorMessage = null;
    this.errorParams = {};
    this.successMessage = null;

    const payload: UpdatePasswordRequestDTO = {
      currentPassword: event.currentPassword,
      newPassword: event.newPassword
    };

    this.userService.updatePassword(payload).subscribe({
      next: () => {
        this.logger.log('Mot de passe mis à jour');
        this.successMessage = 'RESETPASSWORD.SUCCESS';
        this.stopCountdown();
        this.closeEditSection();
      },
      error: (err) => this.handleError(err)
    });
  }

  onDeleteAccount(): void {
    // TODO: Implémenter la suppression
    this.logger.log('Suppression du compte annulée');
  }

  toggleEdit(section: 'name' | 'email' | 'password'): void {
    if (this.editSection === section) {
      this.closeEditSection();
      return;
    }
    this.errorMessage = null;
    this.errorParams = {};
    this.successMessage = null;
    this.stopCountdown();

    this.editSection = section;
    this.logger.log('Section ouverte :', section);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { subsection: section, section: 'account' },
      queryParamsHandling: 'merge',
      replaceUrl: true
    }).catch(err => this.logger.error('Erreur de navigation :', err));

    if (section === 'name') {
      this.initNameForm();
    } else if (section === 'email') {
      this.initEmailForm();
    } else if (section === 'password') {
      this.initPasswordForm();
    }

    this.cleanUrl();
  }

  private handleError(err: any): void {
    this.logger.warn('Erreur capturée :', err);
    console.log('Erreur brute:', err);
    console.log('err.key:', err.key);
    if (err.key === 'RATE_LIMIT_EXCEEDED') {
      const remainingSeconds = err.extraData?.remainingSeconds || 0;
      this.setRateLimitError(remainingSeconds);
    } else {
      this.errorMessage = resolveErrorTranslationKey(err.key || 'SHARED.ERROR.GENERIC');
      this.errorParams = {};
      this.fieldError = err.field ? { [err.field]: this.errorMessage } : {};
      this.stopCountdown();
    }
  }

  private setRateLimitError(remainingSeconds: number): void {
    this.remainingSeconds = remainingSeconds;
    this.updateErrorParams();
    this.startCountdown();
  }

  private updateErrorParams(): void {
    const days = Math.floor(this.remainingSeconds / 86400);
    const hours = Math.floor((this.remainingSeconds % 86400) / 3600);
    const minutes = Math.floor((this.remainingSeconds % 3600) / 60);
    const seconds = this.remainingSeconds % 60;

    this.errorParams = { days, hours, minutes, seconds };
    if (days > 0) {
      this.errorMessage = 'ACCOUNT.ERROR.BACKEND.RATE_LIMIT_EXCEEDED_WITH_TIME';
    } else if (hours > 0) {
      this.errorMessage = 'ACCOUNT.ERROR.BACKEND.RATE_LIMIT_EXCEEDED_WITH_HOURS';
    } else {
      this.errorMessage = 'ACCOUNT.ERROR.BACKEND.RATE_LIMIT_EXCEEDED_WITH_MINUTES';
    }
    this.logger.log('Rate limit error set:', { errorMessage: this.errorMessage, errorParams: this.errorParams });
  }

  private startCountdown(): void {
    this.stopCountdown(); // Arrête tout timer existant
    this.countdownSubscription = interval(1000).subscribe(() => {
      if (this.remainingSeconds > 0) {
        this.remainingSeconds--;
        this.updateErrorParams();
      } else {
        this.stopCountdown();
        this.errorMessage = null;
        this.errorParams = {};
        this.logger.log('Compte à rebours terminé');
      }
    });
  }

  private stopCountdown(): void {
    if (this.countdownSubscription) {
      this.countdownSubscription.unsubscribe();
      this.countdownSubscription = null;
    }
  }

  private closeEditSection(): void {
    this.editSection = null;
    this.form.reset();
    this.fieldError = {};
    this.errorMessage = null;
    this.errorParams = {};
    this.stopCountdown();
    this.cleanUrl();
    this.logger.log('Section fermée, formulaire réinitialisé');
  }

  private cleanUrl(): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { section: null, subsection: null },
      queryParamsHandling: 'merge',
      replaceUrl: true
    }).catch(err => this.logger.error('Erreur de navigation lors du nettoyage de l’URL :', err));
    this.logger.log('URL nettoyée, paramètres section et subsection supprimés');
  }
}
