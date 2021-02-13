import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CitiesAllComponent } from './cities-all.component';

describe('CitiesAllComponent', () => {
  let component: CitiesAllComponent;
  let fixture: ComponentFixture<CitiesAllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CitiesAllComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CitiesAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
