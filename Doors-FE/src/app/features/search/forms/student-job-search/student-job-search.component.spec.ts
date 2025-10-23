import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentJobSearchComponent } from './student-job-search.component';

describe('StudentJobSearchComponent', () => {
  let component: StudentJobSearchComponent;
  let fixture: ComponentFixture<StudentJobSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentJobSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentJobSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
