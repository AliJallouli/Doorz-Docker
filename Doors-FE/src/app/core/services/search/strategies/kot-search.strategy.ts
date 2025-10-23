import { Injectable } from '@angular/core';
import { SearchStrategy } from './search-strategy.interface';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import {KotResult} from '../../../models/search.models';

@Injectable({ providedIn: 'root' })
export class KotSearchStrategy  implements SearchStrategy<KotResult> {
  type = 'kot';

  constructor() {}

  search(query: any): Observable<KotResult[]> {
    const fakeResults: KotResult[] = [


    ];

    const filtered = fakeResults.filter(kot =>
      (!query.keywords || kot.title.toLowerCase().includes(query.keywords.toLowerCase())) &&
      (!query.location || kot.location.toLowerCase().includes(query.location.toLowerCase()))
    );

    return of(filtered).pipe(delay(300));
  }
}
