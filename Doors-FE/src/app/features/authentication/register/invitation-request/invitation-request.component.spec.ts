import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationRequestComponent } from './invitation-request.component';

describe('InvitationRequestComponent', () => {
  let component: InvitationRequestComponent;
  let fixture: ComponentFixture<InvitationRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvitationRequestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvitationRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
