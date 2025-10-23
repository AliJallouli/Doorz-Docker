import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentJobResultsComponent } from './student-job-results.component';

describe('StudentJobResultsComponent', () => {
  let component: StudentJobResultsComponent;
  let fixture: ComponentFixture<StudentJobResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentJobResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentJobResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
