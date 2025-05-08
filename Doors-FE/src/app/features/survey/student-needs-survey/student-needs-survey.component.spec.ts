import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentNeedsSurveyComponent } from './student-needs-survey.component';

describe('StudentNeedsSurveyComponent', () => {
  let component: StudentNeedsSurveyComponent;
  let fixture: ComponentFixture<StudentNeedsSurveyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentNeedsSurveyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentNeedsSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
