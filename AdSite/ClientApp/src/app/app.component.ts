import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CategoryViewModel } from './models/CategoryViewModel';
import { Role } from './models/RolesEnum';
import { User } from './models/User';
import { WebSettingsModel } from './models/WebSettingsModel';
import { AuthenticationService } from './services/authentication.service';
import { CategoriesService } from './services/categories.service';
import { WebSettingsService } from './services/web-settings.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Ads Site';
  currentUser?: User;

  menuCategories$: Observable<CategoryViewModel[]> | undefined;
  webSettings$: Observable<WebSettingsModel> | undefined;

  //todo: countryId
  constructor(private webSettingsService: WebSettingsService, private categoriesService: CategoriesService, private authenticationService: AuthenticationService, private router: Router) {
    this.webSettings$ = this.webSettingsService.getWebSettingsModel('99DE8181-09A8-41DB-895E-54E5E0650C3A');
    this.menuCategories$ = this.categoriesService.getCategoriesTreeMenu('6248DE50-32E7-4C04-82A4-A7EF1D03CD05');
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
