import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternshipResultsComponent } from './internship-results.component';

describe('InternshipResultsComponent', () => {
  let component: InternshipResultsComponent;
  let fixture: ComponentFixture<InternshipResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternshipResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InternshipResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
