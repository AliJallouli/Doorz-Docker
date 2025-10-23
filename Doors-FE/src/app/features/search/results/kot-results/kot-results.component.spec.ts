import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KotResultsComponent } from './kot-results.component';

describe('KotResultsComponent', () => {
  let component: KotResultsComponent;
  let fixture: ComponentFixture<KotResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KotResultsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KotResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
