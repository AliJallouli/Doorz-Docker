import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccordionIconComponent } from './accordion-icon.component';

describe('AccordionIconComponent', () => {
  let component: AccordionIconComponent;
  let fixture: ComponentFixture<AccordionIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccordionIconComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccordionIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
