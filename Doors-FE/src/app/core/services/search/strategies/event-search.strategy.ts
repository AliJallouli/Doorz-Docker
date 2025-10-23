import { Injectable } from '@angular/core';
import { SearchStrategy } from './search-strategy.interface';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { EventResult } from '../../../models/search.models';

@Injectable({ providedIn: 'root' })
export class EventSearchStrategy implements SearchStrategy<EventResult> {
  type = 'events';

  constructor() {}

  search(query: any): Observable<EventResult[]> {
    const fakeResults: EventResult[] = [
      {
        id: 1,
        title: "Conférence intergalactique du Savoir 2025",
        organizer: "Université Cosmique d’Orion",
        location: "Station Orbitale Alpha-7",
        startDate: "2025-11-12",
        endDate: "2025-11-14",
        description: "Trois jours de conférences fictives autour des nouvelles formes d’apprentissage quantique et des hologrammes éducatifs.",
        isActive: true,
        language: "fr",
        registrationRequired: true,
        registrationLink: "https://orion-university.fake/register",
        isPublic: true,
      },
      {
        id: 2,
        title: "Hackathon des Robots Caféinés",
        organizer: "TechnoBeans Corp.",
        location: "Centre Virtuel ByteCity",
        startDate: "2025-10-01",
        endDate: "2025-10-03",
        description: "Un hackathon totalement imaginaire où les robots développent des applications pour distribuer du café intelligent.",
        isActive: true,
        language: "fr",
        registrationRequired: true,
        registrationLink: "https://technobeans.fake/hackathon2025",
        isPublic: true,
      },
      {
        id: 3,
        title: "Festival du Code Absurdistan",
        organizer: "Société des Développeurs Anonymes",
        location: "Parc de la Syntaxe, Absurdistan",
        startDate: "2025-08-10",
        endDate: "2025-08-12",
        description: "Festival fictif célébrant le code inutile mais élégant. Concours de boucles infinies et bugs poétiques au programme.",
        isActive: true,
        language: "fr",
        registrationRequired: false,
        registrationLink: undefined,
        isPublic: true,
      },
      {
        id: 4,
        title: "Salon des Inventeurs du Futur Alternatif",
        organizer: "Institut ChronoLab",
        location: "Complexe Temporel, Zone Delta",
        startDate: "2025-09-22",
        endDate: undefined,
        description: "Découvrez les inventions imaginaires issues de réalités parallèles : tournevis gravitationnel, frigo à énergie sombre et bien plus.",
        isActive: true,
        language: "fr",
        registrationRequired: true,
        registrationLink: "https://chronolab.fake/inventeurs",
        isPublic: true,
      },
      {
        id: 5,
        title: "Atelier d’écriture pour intelligences artificielles",
        organizer: "Club Littéraire Neuronal",
        location: "Campus Virtuel Synapse",
        startDate: "2025-07-05",
        endDate: undefined,
        description: "Séance d'entraînement fictive pour les IA qui rêvent d’écrire des romans de science-fiction. Réservé aux processeurs créatifs.",
        isActive: true,
        language: "fr",
        registrationRequired: false,
        registrationLink: undefined,
        isPublic: true,
      },
      {
        id: 6,
        title: "Workshop sur la communication interespèces",
        organizer: "Planète Babel",
        location: "Centre Polylingua, Secteur 42",
        startDate: "2025-10-19",
        endDate: undefined,
        description: "Atelier expérimental pour apprendre à dialoguer avec les dauphins, les plantes et les serveurs Linux.",
        isActive: true,
        language: "fr",
        registrationRequired: true,
        registrationLink: "https://planete-babel.fake/interespes",
        isPublic: true,
      },
      {
        id: 7,
        title: "Foire aux stages galactiques",
        organizer: "JobSphere Orion",
        location: "Dôme Spatial Delta-Prime",
        startDate: "2025-12-01",
        endDate: "2025-12-02",
        description: "Rencontrez des entreprises interstellaires à la recherche de stagiaires pour explorer de nouveaux mondes de données.",
        isActive: true,
        language: "fr",
        registrationRequired: false,
        registrationLink: undefined,
        isPublic: true,
      },
      {
        id: 8,
        title: "Journée portes ouvertes de l’Académie du Chaos Contrôlé",
        organizer: "Institut du Hasard Structuré",
        location: "Laboratoire C-137",
        startDate: "2025-06-28",
        endDate: undefined,
        description: "Découvrez comment maîtriser le chaos et créer l’ordre à partir de bugs imprévisibles. Démonstrations et conférences absurdes.",
        isActive: true,
        language: "fr",
        registrationRequired: false,
        registrationLink: undefined,
        isPublic: true,
      },
      {
        id: 9,
        title: "Colloque sur les algorithmes poétiques",
        organizer: "Université des Arts Numériques",
        location: "Musée du Code, Cybertown",
        startDate: "2025-09-11",
        endDate: "2025-09-12",
        description: "Deux jours pour explorer la beauté du code à travers la poésie, la musique et le bug intentionnel.",
        isActive: true,
        language: "fr",
        registrationRequired: true,
        registrationLink: "https://artsnum.fake/colloque2025",
        isPublic: true,
      },
      {
        id: 10,
        title: "Conférence mondiale sur les univers parallèles du web",
        organizer: "Consortium Multivers Web",
        location: "Serveur Central Nébuleuse-9",
        startDate: "2025-08-30",
        endDate: "2025-08-31",
        description: "Explorez les réalités alternatives du Web3.0 et du Web-quantique à travers des présentations fictives et immersives.",
        isActive: true,
        language: "fr",
        registrationRequired: false,
        registrationLink: undefined,
        isPublic: true,
      }
    ];


    const filtered = fakeResults.filter(event =>
      (!query.keywords || event.title.toLowerCase().includes(query.keywords.toLowerCase())) &&
      (!query.location || event.location.toLowerCase().includes(query.location.toLowerCase()))
    );

    return of(filtered).pipe(delay(300));
  }
}
