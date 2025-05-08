import { Component } from '@angular/core';
import { InvitationFormComponent } from './invitation-form/invitation-form.component'; // Ajustez le chemin selon votre structure de fichiers

@Component({
  selector: 'app-invitation-dashboard',
  standalone: true, // Ajouté car on utilise imports
  templateUrl: './invitation-dashboard.component.html',
  imports: [
    InvitationFormComponent
  ],
  styleUrls: ['./invitation-dashboard.component.css'] // Corrigé "styleUrl" en "styleUrls"
})
export class InvitationDashboardComponent {

}
