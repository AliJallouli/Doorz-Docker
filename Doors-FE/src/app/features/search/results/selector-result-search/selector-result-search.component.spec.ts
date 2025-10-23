import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectorResultSearchComponent } from './selector-result-search.component';

describe('SelectorResultSearchComponent', () => {
  let component: SelectorResultSearchComponent;
  let fixture: ComponentFixture<SelectorResultSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectorResultSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectorResultSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
