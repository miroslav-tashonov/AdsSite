import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ToastrModule } from 'ngx-toastr';
import { of, throwError } from 'rxjs';

import { LocalizationService } from './localization.service';
import { NotificationService } from './notification.service';

describe('LocalizationService', () => {
  let localizationService: LocalizationService;
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
        LocalizationService,
        { provide: NotificationService, useValue: notificationService },
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    notificationService = TestBed.inject(NotificationService) as jasmine.SpyObj<NotificationService>;
    localizationService = TestBed.inject(LocalizationService);

  });

  it('should be created', () => {
    expect(localizationService).toBeTruthy();
  });


  it('should return stub value in getByLocalizationKey', () => {
    const expectedLocalization: string = 'localization';
    const localizationKey = 'key';

    httpClientSpy.get.and.returnValue(of(expectedLocalization));

    localizationService.getByLocalizationKey(localizationKey).subscribe(
      localization => expect(localization).toEqual(expectedLocalization, 'expected heroes'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should return an error when the server returns a 404', () => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404, statusText: 'Not Found'
    });
    const localizationKey = 'key';

    notificationService.showError.and.returnValue();
    httpClientSpy.get.and.returnValue(throwError(errorResponse));

    localizationService.getByLocalizationKey(localizationKey).subscribe(
      localization => fail('expected an error, not data'),
      error => localizationService.errorHandler(error),
    );

    expect(notificationService.showError.calls.count()).toBe(1, 'one call');
  });
});
