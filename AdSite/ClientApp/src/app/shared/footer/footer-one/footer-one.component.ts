import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WebSettingsModel } from '../../classes/web-settings';
import { CountryService } from '../../services/country.service';
import { WebSettingsService } from '../../services/web-settings.service';

@Component({
  selector: 'app-footer-one',
  templateUrl: './footer-one.component.html',
  styleUrls: ['./footer-one.component.scss']
})
export class FooterOneComponent implements OnInit {

  webSettings: WebSettingsModel;

  constructor(private countryService: CountryService, private locationStrategy: LocationStrategy, private webSettingsService: WebSettingsService) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.webSettingsService.getWebSettingsModel(country.id).subscribe(x => this.webSettings = x);
      }
    });
  }

  ngOnInit() {
  }

}
