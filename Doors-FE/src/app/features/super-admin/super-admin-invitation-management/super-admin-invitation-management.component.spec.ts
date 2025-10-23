import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuperAdminInvitationManagementComponent } from './super-admin-invitation-management.component';

describe('SuperAdminInvitationManagementComponent', () => {
  let component: SuperAdminInvitationManagementComponent;
  let fixture: ComponentFixture<SuperAdminInvitationManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuperAdminInvitationManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuperAdminInvitationManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
