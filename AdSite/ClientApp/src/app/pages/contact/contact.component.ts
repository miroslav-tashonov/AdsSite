import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WebSettingsModel } from '../../shared/classes/web-settings';
import { CountryService } from '../../shared/services/country.service';
import { WebSettingsService } from '../../shared/services/web-settings.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  webSettings: WebSettingsModel;

  constructor(private countryService: CountryService, private webSettingsService: WebSettingsService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.webSettingsService.getWebSettingsModel(country.id).subscribe(x => this.webSettings = x);
      }
    });
  }

  ngOnInit() {
  }

}
