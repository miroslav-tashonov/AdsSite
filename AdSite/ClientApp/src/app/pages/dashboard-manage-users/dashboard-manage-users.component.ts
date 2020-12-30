import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { CityModel } from '../../shared/classes/city';
import { Product } from '../../shared/classes/product';
import { User } from '../../shared/classes/User';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { CityService } from '../../shared/services/city.service';
import { CountryService } from '../../shared/services/country.service';
import { ProductsService } from '../../shared/services/products.service';
import { WishlistService } from '../../shared/services/wishlist.service';

@Component({
  selector: 'app-dashboard-manage-users',
  templateUrl: './dashboard-manage-users.component.html',
  styleUrls: ['./dashboard-manage-users.component.scss']
})
export class DashboardManageUsersComponent implements OnInit {
  public city: Observable<User[]> = of([]);
  public userItems: Observable<User[]> = of([]);
  public countryId: string;

  constructor(private router: Router, private authenticationService: AuthenticationService,
    private productsService: ProductsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.getAllItems(country.id);
        this.countryId = country.id;
      }
    });
  }

  ngOnInit() { }

  getAllItems(countryId: string) {
    this.authenticationService.getAllUsers(countryId);

    this.userItems = this.authenticationService.data;
  }

  // Remove from list
  public removeItem(item: User) {
    if (confirm("Are you sure to delete " + item?.id)) {
      this.authenticationService.deleteItem(item?.id, this.countryId);
    }
  }
}
