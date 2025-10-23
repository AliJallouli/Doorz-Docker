import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AidSearchComponent } from './aid-search.component';

describe('AidSearchComponent', () => {
  let component: AidSearchComponent;
  let fixture: ComponentFixture<AidSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AidSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AidSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
