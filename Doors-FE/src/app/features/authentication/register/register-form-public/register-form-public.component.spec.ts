import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterFormPublicComponent } from './register-form-public.component';

describe('RegisterFormPublicComponent', () => {
  let component: RegisterFormPublicComponent;
  let fixture: ComponentFixture<RegisterFormPublicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterFormPublicComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterFormPublicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
