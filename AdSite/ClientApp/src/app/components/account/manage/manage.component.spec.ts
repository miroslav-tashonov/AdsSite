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

import { ManageComponent } from './manage.component';

describe('ManageComponent', () => {
  let component: ManageComponent;
  let fixture: ComponentFixture<ManageComponent>;
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
        ManageComponent,
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
    fixture = TestBed.createComponent(ManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('email field validity', () => {
    let errors: ValidationErrors | null;
    let username = component.loginForm.controls['email'];
    expect(username.valid).toBeFalsy();

    // Email field is required
    errors = username.errors || {};
    expect(errors['required']).toBeTruthy();

    // Set email to something
    username.setValue("test");
    errors = username.errors || {};
    expect(errors['required']).toBeFalsy();

    // Set email to something correct
    username.setValue("test@example.com");
    errors = username.errors || {};
    expect(errors['required']).toBeFalsy();
  });

  it('password field validity', () => {
    let errors: ValidationErrors | null;
    let password = component.loginForm.controls['phone'];

    // Email field is required
    errors = password.errors || {};
    expect(errors['required']).toBeTruthy();

    // Set email to something
    password.setValue("1234");
    errors = password.errors || {};
    expect(errors['required']).toBeFalsy();

    // Set email to something correct
    password.setValue("123456789");
    errors = password.errors || {};
    expect(errors['required']).toBeFalsy();
  });
});
