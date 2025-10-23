import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

interface ActionButton {
  action: string;
  labelKey: string;
  iconClass: string;
  styleClass: string;
}

@Component({
  selector: 'app-action-buttons',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  templateUrl: './action-buttons.component.html',
  styleUrls: ['./action-buttons.component.css']
})
export class ActionButtonsComponent {
  @Input() actions: ActionButton[] = [
    {
      action: 'cancel',
      labelKey: 'ACTIONS.CANCEL',
      iconClass: 'fas fa-times',
      styleClass: 'btn-secondary'
    },
    {
      action: 'submit',
      labelKey: 'ACTIONS.SUBMIT',
      iconClass: 'fas fa-save',
      styleClass: 'btn-primary'
    },
    {
      action: 'edit',
      labelKey: 'ACTIONS.EDIT',
      iconClass: 'fas fa-edit',
      styleClass: 'btn-icon-only'
    }
  ];
  @Output() actionClicked = new EventEmitter<string>();

  onActionClick(action: string) {
    this.actionClicked.emit(action);
  }
}
