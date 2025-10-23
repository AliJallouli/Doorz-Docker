import { Component, Input, OnInit } from '@angular/core';
import {FormGroup, ReactiveFormsModule} from '@angular/forms';
import {NgClass, NgIf} from '@angular/common';
import {TranslateModule} from '@ngx-translate/core';

@Component({
  selector: 'app-phone-input',
  templateUrl: './phone-input.component.html',
  imports: [
    ReactiveFormsModule,
    NgClass,
    TranslateModule,
    NgIf
  ],
  styleUrls: ['./phone-input.component.css']
})
export class PhoneInputComponent implements OnInit {
  @Input() parentForm!: FormGroup;
  @Input() isLoggedIn = false;
  @Input() hideLabel = false;

  ngOnInit() {
    if (!this.parentForm || !this.parentForm.get('countryCode') || !this.parentForm.get('phoneNumber')) {
      console.error('PhoneInputComponent: parentForm doit contenir countryCode et phoneNumber');
    }
  }

  isFieldTouched(field: string): boolean {
    return this.parentForm.get(field)?.touched || false;
  }

  isFieldInvalid(field: string): boolean {
    return this.parentForm.get(field)?.invalid || false;
  }

  sanitizePhoneNumber() {
    const phoneControl = this.parentForm.get('phoneNumber');
    let value = phoneControl?.value || '';
    value = value.replace(/\D/g, '');
    phoneControl?.setValue(value, { emitEvent: false });
  }
}
