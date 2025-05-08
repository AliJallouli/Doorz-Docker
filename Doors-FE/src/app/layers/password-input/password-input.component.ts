import { Component, Input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-password-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslateModule],
  templateUrl: './password-input.component.html',
  styleUrls: ['./password-input.component.css']
})
export class PasswordInputComponent {
  @Input() form!: FormGroup;
  @Input() fieldError: { [key: string]: string } = {};
  @Input() passwordControlName = 'password';
  @Input() confirmPasswordControlName = 'confirmPassword';
  @Input() submitted = false;
}
