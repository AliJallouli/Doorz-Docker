import { Component, Input } from '@angular/core';
import {FormGroup, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {NgClass, NgIf} from '@angular/common';

@Component({
  selector: 'app-otp-input',
  templateUrl: './otp-input.component.html',
  imports: [
    ReactiveFormsModule, TranslateModule, NgClass, NgIf
  ],
  styleUrls: ['./otp-input.component.css']
})
export class OtpInputComponent {
  @Input() form!: FormGroup;
  @Input() submitted = false;
  @Input() otpControlName = 'otp';
  @Input() fieldError: Record<string, string> = {};
}
