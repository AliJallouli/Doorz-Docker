import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectorFormSearchComponent } from './selector-form-search.component';

describe('SelectorFormSearchComponent', () => {
  let component: SelectorFormSearchComponent;
  let fixture: ComponentFixture<SelectorFormSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectorFormSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectorFormSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
