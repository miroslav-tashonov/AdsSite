import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ToastrModule } from 'ngx-toastr';
import { of, throwError } from 'rxjs';
import { SupportedLanguagesModel } from '../models/SupportedLanguagesModel';

import { LanguageService } from './language.service';
import { NotificationService } from './notification.service';

describe('LanguageService', () => {
  let languageService: LanguageService;
  let httpClientSpy: { get: jasmine.Spy };
  let notificationService: jasmine.SpyObj<NotificationService>;

  beforeEach(() => {
    notificationService = jasmine.createSpyObj('NotificationService', ['showError']);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);

    TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot()
      ],
      providers: [
        LanguageService,
        { provide: NotificationService, useValue: notificationService },
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    notificationService = TestBed.inject(NotificationService) as jasmine.SpyObj<NotificationService>;
    languageService = TestBed.inject(LanguageService);

  });

  it('should be created', () => {
    expect(languageService).toBeTruthy();
  });


  it('should return stub value in supportedLanguages', () => {
    const expectedLanguages: SupportedLanguagesModel[] =
      [{ cultureId: 1, languageName: 'AL' }, { cultureId: 2, languageName: 'MKD' }];

    httpClientSpy.get.and.returnValue(of(expectedLanguages));

    languageService.getSupportedCultures().subscribe(
      languages => expect(languages).toEqual(expectedLanguages, 'expected languages'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should return an error when the server returns a 404', () => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404, statusText: 'Not Found'
    });

    notificationService.showError.and.returnValue();
    httpClientSpy.get.and.returnValue(throwError(errorResponse));

    languageService.getSupportedCultures().subscribe(
      languages => fail('expected an error, not data'),
      error => languageService.errorHandler(error),
    );

    expect(notificationService.showError.calls.count()).toBe(1, 'one call');
  });

});
