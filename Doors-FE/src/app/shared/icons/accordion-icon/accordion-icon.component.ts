import {Component, Input} from '@angular/core';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-accordion-icon',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './accordion-icon.component.html',
  styleUrl: './accordion-icon.component.css'
})
export class AccordionIconComponent {
  @Input() type: 'job' | 'internship' | 'study' | 'event' | 'aid' |'kot'= 'job';
}
