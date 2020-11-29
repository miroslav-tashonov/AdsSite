import { LocationStrategy } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ToastrModule } from 'ngx-toastr';
import { of } from 'rxjs';
import { CountryModel } from '../../models/CountryModel';
import { WebSettingsModel } from '../../models/WebSettingsModel';
import { CountryService } from '../../services/country.service';
import { WebSettingsService } from '../../services/web-settings.service';

import { ContactFormComponent } from './contact-form.component';

describe('ContactFormComponent', () => {
  let component: ContactFormComponent;
  let fixture: ComponentFixture<ContactFormComponent>;
  let webSettingsService: jasmine.SpyObj<WebSettingsService>;
  let countryService: jasmine.SpyObj<CountryService>;
  let locationStrategy: jasmine.SpyObj<LocationStrategy>;
  let httpClientSpy: { get: jasmine.Spy };
  let webSettingsModel: WebSettingsModel;
  let countryModel: CountryModel;
  

  beforeEach(async () => {
    locationStrategy = jasmine.createSpyObj('LocationStrategy', ['getBaseHref']);
    countryService = jasmine.createSpyObj('CountryService', ['getCountryId']);
    webSettingsService = jasmine.createSpyObj('WebSettingsService', ['getWebSettingsModel']);
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['post']);

    await TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot()
      ],
      declarations: [ContactFormComponent],
      providers: [
        { provide: WebSettingsService, useValue: webSettingsService },
        { provide: HttpClient, useValue: httpClientSpy },
        { provide: CountryService, useValue: countryService },
        { provide: LocationStrategy, useValue: locationStrategy }
      ]
    }).compileComponents();

    locationStrategy.getBaseHref.and.returnValue('/belgija');
    locationStrategy = TestBed.inject(LocationStrategy) as jasmine.SpyObj<LocationStrategy>;

    countryService.getCountryId.and.returnValue(of(countryModel));
    countryService = TestBed.inject(CountryService) as jasmine.SpyObj<CountryService>;

    webSettingsService.getWebSettingsModel.and.returnValue(of(webSettingsModel));
    webSettingsService = TestBed.inject(WebSettingsService) as jasmine.SpyObj<WebSettingsService>;
    
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should return web settings model', () => {
    const expectedCountry: CountryModel = { id: '1', name: 'A' };
    const countryId = '1';

    countryService.getCountryId.and.returnValue(of(expectedCountry));

    countryService.getCountryId(countryId).subscribe(
      categories => webSettingsService.getWebSettingsModel(countryId),
      error => fail('expected an error, not data'),
    );

    expect(webSettingsService.getWebSettingsModel.calls.count()).toBe(1, 'one call');
  });
});
