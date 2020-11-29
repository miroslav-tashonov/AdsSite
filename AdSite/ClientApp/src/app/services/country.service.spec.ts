import { LocationStrategy } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ToastrModule } from 'ngx-toastr';
import { of, throwError } from 'rxjs';
import { CountryModel } from '../models/CountryModel';

import { CountryService } from './country.service';
import { NotificationService } from './notification.service';

describe('CountryService', () => {
  let countryService: CountryService;
  let httpClientSpy: { post: jasmine.Spy };
  let notificationService: jasmine.SpyObj<NotificationService>;
  let locationStrategy: jasmine.SpyObj<LocationStrategy>;

  beforeEach(() => {
    notificationService = jasmine.createSpyObj('NotificationService', ['showError']);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['post']);
    locationStrategy = jasmine.createSpyObj('LocationStrategy', ['getBaseHref']);

    TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot()
      ],
      providers: [
        CountryService,
        { provide: NotificationService, useValue: notificationService },
        { provide: HttpClient, useValue: httpClientSpy },
        { provide: LocationStrategy, useValue: locationStrategy }
      ]
    });
    const expectedCountry: CountryModel = { id: '1', name: 'A' };
    const countryPath = '/belgija';

    locationStrategy.getBaseHref.and.returnValue('/belgija');
    httpClientSpy.post.and.returnValue(of(expectedCountry));

    notificationService = TestBed.inject(NotificationService) as jasmine.SpyObj<NotificationService>;
    locationStrategy = TestBed.inject(LocationStrategy) as jasmine.SpyObj<LocationStrategy>;
    countryService = TestBed.inject(CountryService);

  });

  it('should be created', () => {
    expect(countryService).toBeTruthy();
  });


  it('should return stub value in getCountryId', () => {
    const expectedCountry: CountryModel = { id: '1', name: 'A' };
    const countryPath = '/belgija';

    locationStrategy.getBaseHref.and.returnValue('/belgija');
    httpClientSpy.post.and.returnValue(of(expectedCountry));

    countryService.getCountryId(countryPath).subscribe(
      countries => expect(countries).toEqual(expectedCountry, 'expected heroes'),
      fail
    );
    expect(httpClientSpy.post.calls.count()).toBe(2, 'one call');
  });

  it('should return an error when the server returns a 404', () => {
    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404, statusText: 'Not Found'
    });
    const countryId = '1';
    locationStrategy.getBaseHref.and.returnValue('/belgija');

    notificationService.showError.and.returnValue();
    httpClientSpy.post.and.returnValue(throwError(errorResponse));

    countryService.getCountryId(countryId).subscribe(
      country => fail('expected an error, not data'),
      error => countryService.errorHandler(error),
    );

    expect(notificationService.showError.calls.count()).toBe(1, 'one call');
  });
});
