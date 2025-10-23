import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestResetPasswordComponent } from './request-reset-password.component';

describe('ResetPasswordComponent', () => {
  let component: RequestResetPasswordComponent;
  let fixture: ComponentFixture<RequestResetPasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequestResetPasswordComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
