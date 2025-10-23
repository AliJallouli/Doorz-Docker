import { Component } from '@angular/core';

@Component({
  selector: 'app-superadmin-home',
  standalone: true, // Ajoutez ceci si ce n’est pas déjà présent
  imports: [], // Ajoutez le composant ici
  templateUrl: './superadmin-home.component.html',
  styleUrls: ['./superadmin-home.component.css'] // Correction : styleUrls, pas styleUrl
})
export class SuperadminHomeComponent {
}
