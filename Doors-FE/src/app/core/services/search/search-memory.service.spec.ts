import { TestBed } from '@angular/core/testing';

import { SearchMemoryService } from './search-memory.service';

describe('SearchMemoryService', () => {
  let service: SearchMemoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SearchMemoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
