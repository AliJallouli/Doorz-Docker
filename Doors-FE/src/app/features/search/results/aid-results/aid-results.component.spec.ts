import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AidResultsComponent } from './aid-results.component';

describe('AidResultsComponent', () => {
  let component: AidResultsComponent;
  let fixture: ComponentFixture<AidResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AidResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AidResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
