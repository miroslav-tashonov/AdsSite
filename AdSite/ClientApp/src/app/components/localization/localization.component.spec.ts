import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { LocalizationService } from '../../services/localization.service';

import { LocalizationComponent } from './localization.component';

describe('LocalizationComponent', () => {
  let component: LocalizationComponent;
  let fixture: ComponentFixture<LocalizationComponent>;
  let localizationService: jasmine.SpyObj<LocalizationService>;

  beforeEach(async () => {
    localizationService = jasmine.createSpyObj('LocalizationService', ['getByLocalizationKey']);

    await TestBed.configureTestingModule({
      declarations: [LocalizationComponent],
      providers: [
        { provide: LocalizationService, useValue: localizationService },
      ]
    }).compileComponents();

    localizationService.getByLocalizationKey.and.returnValue(of('string'));
    localizationService = TestBed.inject(LocalizationService) as jasmine.SpyObj<LocalizationService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LocalizationComponent);
    component = fixture.componentInstance;
    fixture.componentInstance.localizationValue$ = new Observable<string>();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    expect(localizationService.getByLocalizationKey.calls.count()).toBe(1, 'one call');
  });
});
