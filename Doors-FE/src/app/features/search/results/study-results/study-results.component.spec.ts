import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudyResultsComponent } from './study-results.component';

describe('StudyResultsComponent', () => {
  let component: StudyResultsComponent;
  let fixture: ComponentFixture<StudyResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudyResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudyResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
