import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionnelLandingpageComponent } from './professionnel-landingpage.component';

describe('ProfessionnelLandingpageComponent', () => {
  let component: ProfessionnelLandingpageComponent;
  let fixture: ComponentFixture<ProfessionnelLandingpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfessionnelLandingpageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfessionnelLandingpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
