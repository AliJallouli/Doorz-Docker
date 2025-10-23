import { Component, Input } from '@angular/core';
import {NgClass, NgSwitch, NgSwitchCase, NgSwitchDefault} from '@angular/common';

@Component({
  selector: 'app-search-icon',
  standalone: true,
  templateUrl: './search-icon.component.html',
  imports: [
    NgSwitchCase,
    NgSwitchDefault,
    NgSwitch,
    NgClass
  ],
  styleUrls: ['./search-icon.component.css']
})
export class SearchIconComponent {
  @Input() type: 'job' | 'internship' | 'study' | 'event' | 'aid' | 'kot' = 'job';
}
