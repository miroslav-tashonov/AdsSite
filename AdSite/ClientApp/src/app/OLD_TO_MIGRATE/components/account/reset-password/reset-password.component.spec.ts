import { HttpClient } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, ValidationErrors } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule } from 'ngx-toastr';
import { of } from 'rxjs';
import { CountryModel } from '../../../models/CountryModel';
import { AuthenticationService } from '../../../services/authentication.service';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';

import { ResetPasswordComponent } from './reset-password.component';

describe('ResetPasswordComponent', () => {
  let component: ResetPasswordComponent;
  let fixture: ComponentFixture<ResetPasswordComponent>;
  let httpClientSpy: { post: jasmine.Spy };
  let countryService: jasmine.SpyObj<CountryService>;
  let countryModel: CountryModel;

  beforeEach(async () => {
    countryService = jasmine.createSpyObj('CountryService', ['getCountryId']);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['post']);

    await TestBed.configureTestingModule({

      imports: [
        RouterTestingModule,
        ToastrModule.forRoot()
      ],
      declarations: [
        ResetPasswordComponent,
      ],
      providers: [
        { provide: HttpClient, useValue: httpClientSpy },
        { provide: CountryService, useValue: countryService },
        AuthenticationService,
        NotificationService,
        FormBuilder,
      ]
    })
      .compileComponents();

    countryService.getCountryId.and.returnValue(of(countryModel));
    countryService = TestBed.inject(CountryService) as jasmine.SpyObj<CountryService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //it('old password field validity', () => {
  //  let errors: ValidationErrors | null;
  //  let password = component.loginForm.controls['oldPassword'];

  //  // Email field is required
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeTruthy();

  //  // Set email to something
  //  password.setValue("1234");
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeFalsy();

  //  // Set email to something correct
  //  password.setValue("123456789");
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeFalsy();
  //});

  //it('password field validity', () => {
  //  let errors: ValidationErrors | null;
  //  let password = component.loginForm.controls['password'];

  //  // Email field is required
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeTruthy();

  //  // Set email to something
  //  password.setValue("1234");
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeFalsy();

  //  // Set email to something correct
  //  password.setValue("123456789");
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeFalsy();
  //});

  //it('confirmPassword field validity', () => {
  //  let errors: ValidationErrors | null;
  //  let password = component.loginForm.controls['confirmPassword'];

  //  // Email field is required
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeTruthy();

  //  // Set email to something
  //  password.setValue("1234");
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeFalsy();

  //  // Set email to something correct
  //  password.setValue("123456789");
  //  errors = password.errors || {};
  //  expect(errors['required']).toBeFalsy();
  //});
});
