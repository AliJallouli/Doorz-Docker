import { TestBed } from '@angular/core/testing';

import { FloatingWindowService } from './floating-window.service';

describe('FloatingWindowService', () => {
  let service: FloatingWindowService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FloatingWindowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
