import {ValidatorFn, Validators} from '@angular/forms';
import {CustomPatterns} from './patterns';

export class SharedValidators {
  static email(): ValidatorFn {
    return Validators.pattern(CustomPatterns.email);
  }
}
