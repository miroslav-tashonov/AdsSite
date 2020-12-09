import { Component, OnInit } from '@angular/core';
import { Product } from '../../../../classes/product';
import { WishlistService } from '../../../../services/wishlist.service';
import { ProductsService } from '../../../../../shared/services/products.service';
import { Observable, of } from 'rxjs';
import { WebSettingsModel } from '../../../../classes/web-settings';
import { Role } from '../../../../classes/roles';
import { CountryService } from '../../../../services/country.service';
import { LocationStrategy } from '@angular/common';
import { WebSettingsService } from '../../../../services/web-settings.service';
import { AuthenticationService } from '../../../../services/authentication.service';
import { Router } from '@angular/router';
import { User } from '../../../../classes/User';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar-one.component.html',
  styleUrls: ['./topbar-one.component.scss']
})
export class TopbarOneComponent implements OnInit {
  currentUser?: User;
  countryId?: string;
  webSettings: WebSettingsModel;

  constructor(public productsService: ProductsService, private locationStrategy: LocationStrategy,
    private countryService: CountryService, private webSettingsService: WebSettingsService,
    private authenticationService: AuthenticationService, private router: Router) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.webSettingsService.getWebSettingsModel(country.id).subscribe(x => this.webSettings = x);
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

  ngOnInit() { }

}
