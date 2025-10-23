import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KotSearchComponent } from './kot-search.component';

describe('KotSearchComponent', () => {
  let component: KotSearchComponent;
  let fixture: ComponentFixture<KotSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KotSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KotSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
