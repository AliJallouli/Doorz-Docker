import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentLandingpageComponent } from './student-landingpage.component';

describe('StudentLandingpageComponent', () => {
  let component: StudentLandingpageComponent;
  let fixture: ComponentFixture<StudentLandingpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentLandingpageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentLandingpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
