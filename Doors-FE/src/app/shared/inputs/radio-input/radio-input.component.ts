import {Component, Input} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from '@angular/forms';
import {TranslateModule} from '@ngx-translate/core';
import {NgForOf, NgIf} from '@angular/common';

export interface RadioOption {
  label: string;
  value: string | number;
}

@Component({
  selector: 'app-radio-input',
  imports: [
    ReactiveFormsModule, TranslateModule, NgIf, NgForOf
  ],
  templateUrl: './radio-input.component.html',
  styleUrl: './radio-input.component.css'
})
export class RadioInputComponent {
  @Input() form!: FormGroup;
  @Input() controlName!: string;
  @Input() options: RadioOption[] = [];

  @Input() labelKey!: string;
  @Input() submitted = false;
  @Input() fieldError: { [key: string]: string } = {};

  @Input() requiredErrorKey = 'OPTION_INPUT.REQUIRED';


}
