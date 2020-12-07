import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ToastrModule } from 'ngx-toastr';
import { of, throwError } from 'rxjs';
import { WebSettingsModel } from '../models/WebSettingsModel';
import { NotificationService } from './notification.service';

import { WebSettingsService } from './web-settings.service';

describe('WebSettingsService', () => {
  let webSettingsService: WebSettingsService;
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
        WebSettingsService,
        { provide: NotificationService, useValue: notificationService },
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    notificationService = TestBed.inject(NotificationService) as jasmine.SpyObj<NotificationService>;
    webSettingsService = TestBed.inject(WebSettingsService);

  });

  it('should be created', () => {
    expect(webSettingsService).toBeTruthy();
  });


  it('should return stub value in getWebSettingsModel', () => {
    const expectedWebSetting: WebSettingsModel = { countryId: '1', email: 'A' };
    const countryId = '1';

    httpClientSpy.get.and.returnValue(of(expectedWebSetting));

    webSettingsService.getWebSettingsModel(countryId).subscribe(
      webSetting => expect(webSetting).toEqual(expectedWebSetting, 'expected heroes'),
      fail
    );
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should return an error when the server returns a 404', () => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404, statusText: 'Not Found'
    });
    const countryId = '1';

    notificationService.showError.and.returnValue();
    httpClientSpy.get.and.returnValue(throwError(errorResponse));

    webSettingsService.getWebSettingsModel(countryId).subscribe(
      webSetting => fail('expected an error, not data'),
      error => webSettingsService.errorHandler(error),
    );

    expect(notificationService.showError.calls.count()).toBe(1, 'one call');
  });
});
