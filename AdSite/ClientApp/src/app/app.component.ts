import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { WebSettingsModel } from './models/WebSettingsModel';
import { WebSettingsService } from './services/web-settings.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';

  webSettings$: Observable<WebSettingsModel> | undefined;

  //todo countryId
  constructor(private webSettingsService: WebSettingsService) {
    this.webSettings$ = this.webSettingsService.getWebSettingsModel('99DE8181-09A8-41DB-895E-54E5E0650C3A');
  }
}
