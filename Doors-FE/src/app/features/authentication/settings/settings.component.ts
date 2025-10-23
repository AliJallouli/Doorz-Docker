import { Component } from '@angular/core';
import {AccountComponent} from './account/account.component';

@Component({
  selector: 'app-settings',
  imports: [
    AccountComponent
  ],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

}
