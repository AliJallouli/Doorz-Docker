import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { tap } from 'rxjs/operators';
import { SearchStrategy } from './strategies/search-strategy.interface';
import { JobSearchStrategy } from './strategies/job-search.strategy';
import { StudySearchStrategy } from './strategies/study-search-strategy';
import { EventSearchStrategy } from './strategies/event-search.strategy';
import { InternshipSearchStrategy } from './strategies/internship-search.strategy';
import { AidSearchStrategy } from './strategies/aid-search.strategy';
import {KotSearchStrategy} from './strategies/kot-search.strategy'; // Add this import

export interface SearchState<T = any> {
  type: string;
  query: any;
  results: T[];
}

@Injectable({ providedIn: 'root' })
export class SearchService {
  private strategyMap = new Map<string, SearchStrategy>();
  private stateSubject = new BehaviorSubject<SearchState<any> | null>(null);
  public readonly state$ = this.stateSubject.asObservable();

  constructor(
    job: JobSearchStrategy,
    study: StudySearchStrategy,
    event: EventSearchStrategy,
    internship: InternshipSearchStrategy,
    aid: AidSearchStrategy,
    kot: KotSearchStrategy,
  ) {
    [job, study, event, internship, aid,kot].forEach(strategy =>
      this.strategyMap.set(strategy.type, strategy)
    );
  }

  search(type: string, query: any): Observable<any[]> {
    const strategy = this.strategyMap.get(type);
    if (!strategy) return throwError(() => new Error(`No strategy for type ${type}`));

    return strategy.search(query).pipe(
      tap(results => {
        this.stateSubject.next({ type, query, results });
      })
    );
  }

  get currentState(): SearchState | null {
    return this.stateSubject.value;
  }

  clearState() {
    this.stateSubject.next(null);
  }
}
