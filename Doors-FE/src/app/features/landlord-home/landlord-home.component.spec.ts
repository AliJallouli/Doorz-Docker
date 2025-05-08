import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandlordHomeComponent } from './landlord-home.component';

describe('LandlordHomeComponent', () => {
  let component: LandlordHomeComponent;
  let fixture: ComponentFixture<LandlordHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LandlordHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandlordHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
