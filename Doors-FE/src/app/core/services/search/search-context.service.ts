// core/services/search/search-context.service.ts
import { Injectable } from '@angular/core';

export interface SearchContext {
  searchType: string;
  searchTitle: string;
  activePanel: number;
}

@Injectable({ providedIn: 'root' })
export class SearchContextService {
  getStudentContext(section: string): SearchContext {
    switch (section) {
      case 'jobs':
        return { searchType: 'jobs', searchTitle: 'SEARCH.STUDENT_JOB.TITLE', activePanel: 0 };
      case 'internships':
        return { searchType: 'internships', searchTitle: 'SEARCH.INTERNSHIP.TITLE', activePanel: 1 };
      case 'studies':
        return { searchType: 'studies', searchTitle: 'SEARCH.STUDY.TITLE', activePanel: 2 };
      case 'events':
        return { searchType: 'events', searchTitle: 'SEARCH.EVENT.TITLE', activePanel: 3 };
      case 'aid':
        return { searchType: 'aid', searchTitle: 'SEARCH.AID.TITLE', activePanel: 4 };
      case 'kot':
        return { searchType: 'kot', searchTitle: 'SEARCH.KOT.TITLE', activePanel: 5 };
      default:
        return { searchType: 'none', searchTitle: 'LANDINGPAGEA.STRUDENT.TITLE', activePanel: -1 };
    }
  }

  getProfessionalContext(section: string): SearchContext {
    const panelIndex = {
      recruiter: 5,
      institutions: 6,
      organisations: 7,
      associations: 8,
      mouvement: 9
    }[section] ?? 10;

    return {
      searchType: 'unknown',
      searchTitle: 'SEARCH.PRO.TITLE',
      activePanel: panelIndex
    };
  }
}
