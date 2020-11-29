import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WebSettingsModel } from '../../models/WebSettingsModel';
import { CountryService } from '../../services/country.service';
import { WebSettingsService } from '../../services/web-settings.service';

@Component({
  selector: 'contact',
  templateUrl: './contact-form.component.html'
})
export class ContactFormComponent {
  webSettings$: Observable<WebSettingsModel> | undefined;

  constructor(private webSettingsService: WebSettingsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.webSettings$ = this.webSettingsService.getWebSettingsModel(country.id);
      }
    });
  }
}
