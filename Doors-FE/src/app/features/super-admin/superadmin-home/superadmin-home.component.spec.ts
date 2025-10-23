import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuperadminHomeComponent } from './superadmin-home.component';

describe('SuperadminHomeComponent', () => {
  let component: SuperadminHomeComponent;
  let fixture: ComponentFixture<SuperadminHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuperadminHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuperadminHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
