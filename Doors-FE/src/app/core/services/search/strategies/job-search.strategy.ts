import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchStrategy } from './search-strategy.interface';
import { Observable, of } from 'rxjs';
import { JobResult } from '../../../models/search.models';

@Injectable({ providedIn: 'root' })
export class JobSearchStrategy implements SearchStrategy<JobResult> {
  type = 'jobs';

  constructor(private http: HttpClient) {}

  search(query: any): Observable<JobResult[]> {
    const fakeResults: JobResult[] = [
      {
        id: 1,
        title: "Agent de maintenance des robots distributeurs de pizzas",
        company: "MechaFood Industries",
        location: "Cité Technopolis",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Temps partiel",
        salary: 13.25,
        currency: "CRD",
        startDate: "2025-06-01",
        isActive: true,
        description: "Aidez nos robots à servir des pizzas sans explosion de fromage. Formation express fournie (casque anti-mozzarella inclus).",
        language: "fr",
      },
      {
        id: 2,
        title: "Archiviste de données temporelles",
        company: "ChronoCorp",
        location: "Zone Delta, Ligne du Temps B",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Horaire flexible",
        salary: 15.00,
        currency: "CRD",
        startDate: "2025-07-10",
        isActive: true,
        description: "Classez des documents provenant du futur, du passé et parfois des deux en même temps. Esprit logique indispensable.",
        language: "fr",
      },
      {
        id: 3,
        title: "Testeur de réalités virtuelles immersives",
        company: "DreamLab VR",
        location: "Station Holo-3",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Soirée/Weekend",
        salary: 14.75,
        currency: "CRD",
        startDate: "2025-06-15",
        isActive: true,
        description: "Testez des mondes virtuels avant leur mise en ligne. Risque modéré de rester coincé dans une simulation. Café illimité.",
        language: "fr",
      },
      {
        id: 4,
        title: "Opérateur en recyclage de données obsolètes",
        company: "ByteCycle",
        location: "Serveur Central Nébula-9",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Temps plein",
        salary: 11.80,
        currency: "CRD",
        startDate: "2025-07-01",
        isActive: true,
        description: "Supprimez, triez et compressez les données périmées pour leur donner une seconde vie numérique.",
        language: "fr",
      },
      {
        id: 5,
        title: "Technicien en nettoyage de drones",
        company: "SkyClean Solutions",
        location: "Cité Aérienne Gamma",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Soirée/Weekend",
        salary: 12.60,
        currency: "CRD",
        startDate: "2025-06-20",
        isActive: true,
        description: "Nettoyez les drones après leur service de livraison. Attention : certaines unités sont chatouilleuses.",
        language: "fr",
      },
      {
        id: 6,
        title: "Assistant en culture hydroponique lunaire",
        company: "LunaFarm",
        location: "Base Lunaire Alpha",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Temps partiel",
        salary: 13.50,
        currency: "CRD",
        startDate: "2025-06-30",
        isActive: true,
        description: "Aidez à cultiver des laitues dans un environnement sans gravité. Expérience spatiale non requise.",
        language: "fr",
      },
      {
        id: 7,
        title: "Barista en gravité zéro",
        company: "Café Orbital",
        location: "Station Orbitale Beta",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Soirée/Weekend",
        salary: 14.00,
        currency: "CRD",
        startDate: "2025-07-05",
        isActive: true,
        description: "Préparez des cafés sans gravité. Apprenez à faire de la mousse de lait flottante et à servir sans renverser.",
        language: "fr",
      },
      {
        id: 8,
        title: "Guide touristique de réalités parallèles",
        company: "MultiVerse Travels",
        location: "Cité des Portails",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Horaire flexible",
        salary: 16.20,
        currency: "CRD",
        startDate: "2025-08-01",
        isActive: true,
        description: "Accompagnez des visiteurs à travers des univers alternatifs. Bon sens de l’orientation interdimensionnelle requis.",
        language: "fr",
      },
      {
        id: 9,
        title: "Consultant en éthique robotique",
        company: "AI&Moi",
        location: "Ville de Silicium",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Temps partiel",
        salary: 15.75,
        currency: "CRD",
        startDate: "2025-07-10",
        isActive: true,
        description: "Aidez les intelligences artificielles à comprendre les émotions humaines. Formation psychologique incluse.",
        language: "fr",
      },
      {
        id: 10,
        title: "Agent d’accueil dans un musée des futurs possibles",
        company: "Musée Chronos",
        location: "District Historique 12",
        offerType: "Job étudiant",
        contractType: "Étudiant",
        scheduleType: "Temps plein",
        salary: 12.90,
        currency: "CRD",
        startDate: "2025-08-05",
        isActive: true,
        description: "Accueillez les visiteurs venus contempler les futurs qui n’ont pas eu lieu. Tenue rétrofuturiste fournie.",
        language: "fr",
      }
    ];


    const filtered = fakeResults.filter(job =>
      (!query.keywords || job.title.toLowerCase().includes(query.keywords.toLowerCase())) &&
      (!query.location || job.location.toLowerCase().includes(query.location.toLowerCase()))
    );

    return of(filtered);
  }
}
