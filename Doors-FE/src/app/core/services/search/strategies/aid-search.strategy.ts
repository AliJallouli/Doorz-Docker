import { Injectable } from '@angular/core';
import { SearchStrategy } from './search-strategy.interface';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { AidResult } from '../../../models/search.models';

@Injectable({ providedIn: 'root' })
export class AidSearchStrategy implements SearchStrategy<AidResult> {
  type = 'aid';

  constructor() {}

  search(query: any): Observable<AidResult[]> {
    const fakeResults: AidResult[] = [
      {
        id: 1,
        title: "Programme Nova Études",
        description: "Subvention fictive destinée aux étudiants de la galaxie Nova pour financer leurs repas quantiques.",
        location: "Interplanétaire",
        provider: "Agence Universelle de l’Éducation",
        amount: 842.57,
        currency: "CRD",
        applicationDeadline: "2025-09-15",
        isActive: true,
        language: "fr",
      },
      {
        id: 2,
        title: "Prime Luminis",
        description: "Aide expérimentale accordée aux apprenants du futur souhaitant développer des technologies à énergie solaire inversée.",
        location: "Système Solaire",
        provider: "Institut Luminis",
        amount: 320.00,
        currency: "CRD",
        applicationDeadline: "2025-10-21",
        isActive: true,
        language: "fr",
      },
      {
        id: 3,
        title: "Fonds Alpha Beta",
        description: "Soutien symbolique aux étudiants cherchant à améliorer leurs compétences en logique abstraite appliquée.",
        location: "Virtuelle",
        provider: "Fondation AlphaBeta",
        amount: 1500.99,
        currency: "CRD",
        applicationDeadline: "2025-12-01",
        isActive: true,
        language: "fr",
      },
      {
        id: 4,
        title: "Subvention Pixel Académie",
        description: "Aide pour les développeurs apprentis qui souhaitent coder en 16 bits sur des machines rétrofuturistes.",
        location: "Communauté virtuelle",
        provider: "Académie Pixel",
        amount: 480.75,
        currency: "CRD",
        applicationDeadline: "2025-11-20",
        isActive: true,
        language: "fr",
      },
      {
        id: 5,
        title: "Bourse du Café Infini",
        description: "Allocation mensuelle pour les étudiants qui passent plus de 10 heures par jour à étudier tout en buvant du café quantique.",
        location: "Planète Caféïne",
        provider: "Consortium du Café Infini",
        amount: 999.99,
        currency: "CRD",
        applicationDeadline: "2025-09-05",
        isActive: false,
        language: "fr",
      }
    ];


    const filtered = fakeResults.filter(aid =>
      (!query.keywords || aid.title.toLowerCase().includes(query.keywords.toLowerCase())) &&
      (!query.location || aid.location.toLowerCase().includes(query.location.toLowerCase()))
    );

    return of(filtered).pipe(delay(300));
  }
}
