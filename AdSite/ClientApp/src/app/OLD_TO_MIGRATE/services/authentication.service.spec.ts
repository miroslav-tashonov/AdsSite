import { LocationStrategy } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { RegisterUser, ResetPasswordModel } from '../models/User';

import { AuthenticationService } from './authentication.service';

describe('AuthenticationService', () => {
  let service: AuthenticationService;
  let httpClientSpy: { post: jasmine.Spy };
  let locationStrategy: jasmine.SpyObj<LocationStrategy>;

  beforeEach(() => {
    locationStrategy = jasmine.createSpyObj('LocationStrategy', ['getBaseHref']);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['post']);
    TestBed.configureTestingModule({
      providers: [
        AuthenticationService,
        { provide: LocationStrategy, useValue: locationStrategy },
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    locationStrategy = TestBed.inject(LocationStrategy) as jasmine.SpyObj<LocationStrategy>;
    service = TestBed.inject(AuthenticationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('auth service is calling methods properly', () => {
    httpClientSpy.post.and.returnValue(of());

    service.login('', '', '');
    service.register(new RegisterUser());
    service.update(new RegisterUser());
    service.resetPassword(new ResetPasswordModel());

    expect(httpClientSpy.post.calls.count()).toBe(4, 'four calls');
  });
});
