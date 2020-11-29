import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { SupportedLanguagesModel } from '../../models/SupportedLanguagesModel';
import { LanguageService } from '../../services/language.service';

import { LanguagePickerComponent } from './language-picker.component';

describe('LanguagePickerComponent', () => {
  let component: LanguagePickerComponent;
  let fixture: ComponentFixture<LanguagePickerComponent>;
  let languageService: jasmine.SpyObj<LanguageService>;
  let supportedLanguages: SupportedLanguagesModel[];

  beforeEach(async () => {
    languageService = jasmine.createSpyObj('LocalizationService', ['getSupportedCultures']);

    await TestBed.configureTestingModule({
      declarations: [LanguagePickerComponent],
      providers: [
        { provide: LanguageService, useValue: languageService },
      ]
    }).compileComponents();

    supportedLanguages = [];
    languageService.getSupportedCultures.and.returnValue(of(supportedLanguages));
    languageService = TestBed.inject(LanguageService) as jasmine.SpyObj<LanguageService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguagePickerComponent);
    component = fixture.componentInstance;
    fixture.componentInstance.supportedLanguages$ = new Observable<SupportedLanguagesModel[]>();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
    expect(languageService.getSupportedCultures.calls.count()).toBe(1, 'one call');
  });
});
