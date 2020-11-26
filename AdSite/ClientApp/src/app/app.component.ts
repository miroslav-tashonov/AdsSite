import { APP_BASE_HREF, LocationStrategy } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CategoryViewModel } from './models/CategoryViewModel';
import { CountryModel } from './models/CountryModel';
import { Role } from './models/RolesEnum';
import { User } from './models/User';
import { WebSettingsModel } from './models/WebSettingsModel';
import { AuthenticationService } from './services/authentication.service';
import { CategoriesService } from './services/categories.service';
import { CountryService } from './services/country.service';
import { WebSettingsService } from './services/web-settings.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'Ads Site';
  currentUser?: User;

  countryId?: string;
  menuCategories$: Observable<CategoryViewModel[]> | undefined;
  webSettings$: Observable<WebSettingsModel> | undefined;

  constructor(private locationStrategy: LocationStrategy, private countryService: CountryService, private webSettingsService: WebSettingsService, private categoriesService: CategoriesService, private authenticationService: AuthenticationService, private router: Router) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.webSettings$ = this.webSettingsService.getWebSettingsModel(country.id);
        this.menuCategories$ = this.categoriesService.getCategoriesTreeMenu(country.id);
      }
    });

    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === Role.Admin;
  }

  get isUser() {
    return this.currentUser && this.currentUser.role === Role.User;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/']);
  }

}
