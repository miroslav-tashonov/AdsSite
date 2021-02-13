import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { CityModel } from '../../shared/classes/city';
import { Product } from '../../shared/classes/product';
import { CityService } from '../../shared/services/city.service';
import { CountryService } from '../../shared/services/country.service';
import { ProductsService } from '../../shared/services/products.service';
import { WishlistService } from '../../shared/services/wishlist.service';

@Component({
  selector: 'app-dashboard-cities',
  templateUrl: './dashboard-cities.component.html',
  styleUrls: ['./dashboard-cities.component.scss']
})
export class DashboardCitiesComponent implements OnInit {
  public city: Observable<CityModel[]> = of([]);
  public cityItems: Observable<CityModel[]> = of([]);
  public countryId: string;

  constructor(private router: Router, private cityService: CityService,
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
    this.cityService.getAllCities(countryId);

    this.cityItems = this.cityService.data;
  }

  // Remove from list
  public removeItem(item: CityModel) {
    if (confirm("Are you sure to delete " + item?.name)) {
      this.cityService.deleteItem(item?.id, this.countryId);
    }
  }
}
