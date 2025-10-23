import { Observable } from 'rxjs';

export interface SearchStrategy<T = any> {
  type: string;
  search(query: any): Observable<T[]>;
}

