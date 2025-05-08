import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterFromInviteComponent } from './register-from-invite.component';

describe('RegisterFromInviteComponent', () => {
  let component: RegisterFromInviteComponent;
  let fixture: ComponentFixture<RegisterFromInviteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterFromInviteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterFromInviteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
