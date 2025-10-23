import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LegalConsentComponent } from './legal-consent.component';

describe('LegalConsentComponent', () => {
  let component: LegalConsentComponent;
  let fixture: ComponentFixture<LegalConsentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LegalConsentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LegalConsentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
