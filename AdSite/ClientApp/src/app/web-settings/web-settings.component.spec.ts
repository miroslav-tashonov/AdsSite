import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WebSettingsComponent } from './web-settings.component';

describe('WebSettingsComponent', () => {
  let component: WebSettingsComponent;
  let fixture: ComponentFixture<WebSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WebSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WebSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
