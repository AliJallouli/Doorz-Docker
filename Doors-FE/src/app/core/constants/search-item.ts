// core/constants/search-items.ts
export interface SearchItem {
  index: number;
  titleKey: string;
  iconClass: string;
  component: 'job' | 'internship' | 'study' | 'event' | 'aid' | 'kot';
}

export const SEARCH_ITEMS: SearchItem[] = [
  { index: 0, titleKey: 'SEARCH.STUDENT_JOB.TITLE', iconClass: 'fas fa-briefcase', component: 'job' },
  { index: 1, titleKey: 'SEARCH.INTERNSHIP.TITLE', iconClass: 'fas fa-user-graduate', component: 'internship' },
  { index: 2, titleKey: 'SEARCH.STUDY.TITLE', iconClass: 'fas fa-book-open', component: 'study' },
  { index: 3, titleKey: 'SEARCH.EVENT.TITLE', iconClass: 'fas fa-calendar-alt', component: 'event' },
  { index: 4, titleKey: 'SEARCH.AID.TITLE', iconClass: 'fas fa-hand-holding-heart', component: 'aid' },
  { index: 5, titleKey: 'SEARCH.KOT.TITLE', iconClass: 'fas fa-hand-holding-heart', component: 'kot' },
];
