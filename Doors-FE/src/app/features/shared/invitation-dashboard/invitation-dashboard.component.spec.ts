import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationDashboardComponent } from './invitation-dashboard.component';

describe('InvitationDashboardComponent', () => {
  let component: InvitationDashboardComponent;
  let fixture: ComponentFixture<InvitationDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvitationDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvitationDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
