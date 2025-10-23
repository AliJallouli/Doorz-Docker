import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { PasswordResetService } from '../../../../../core/services/auth/password-reset.service';
import { ToastService } from '../../../../../core/services/toast.service';
import { TranslateService,TranslateModule } from '@ngx-translate/core';
import { resolveErrorTranslationKey } from '../../../../../core/errors/error-context-resolver';
import {PasswordResetRequestDTO} from '../../../../../core/models/auth.models';
import {NgClass, NgIf} from '@angular/common';
import {EmailInputComponent} from '../../../../../shared/inputs/email-input/email-input.component';
import {SharedValidators} from '../../../../../core/utils/Validators/shared-validators';


@Component({
  selector: 'app-reset-password',
  templateUrl: './request-reset-password.component.html',
  imports: [
    ReactiveFormsModule, TranslateModule, NgIf, EmailInputComponent
  ],
  styleUrls: ['./request-reset-password.component.css']
})
export class RequestResetPasswordComponent implements OnInit {
  form!: FormGroup;
  submitted = false;
  success = false;
  loading = false;
  message = '';
  genericMsg = 'RESETPASSWORD.FEEDBACK';


  constructor(
    private fb: FormBuilder,
    private passwordservice: PasswordResetService,
    private toast: ToastService,
    private translate: TranslateService
  ) {}
  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', [Validators.required, SharedValidators.email()]]
    });
  }

  onSubmit(): void {
    this.submitted = true;
    this.message = '';
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    const payload: PasswordResetRequestDTO = {
      email: this.form.value.email
    };

    this.passwordservice.requestPasswordReset(payload).subscribe({
      next: (res) => {
        this.message = this.genericMsg;
        this.success = true;
        this.loading = false;
        this.loading = false;
      },
      error: (err) => {
        this.message = this.genericMsg;

        const key = resolveErrorTranslationKey(err.key);
        if(key==="RESETPASSWORD.ERROR.BACKEND.TOO_MANY_TOKEN_REQUESTS")
        {
          this.message = key;
        }

        this.success = true;
        this.loading = false;
      }
    });
  }
}
