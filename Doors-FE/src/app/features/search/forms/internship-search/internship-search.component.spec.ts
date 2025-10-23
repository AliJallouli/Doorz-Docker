import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternshipSearchComponent } from './internship-search.component';

describe('InternshipSearchComponent', () => {
  let component: InternshipSearchComponent;
  let fixture: ComponentFixture<InternshipSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternshipSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InternshipSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
