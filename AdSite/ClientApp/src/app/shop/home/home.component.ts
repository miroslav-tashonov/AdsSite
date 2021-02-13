import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../shared/classes/product';
import { CountryService } from '../../shared/services/country.service';
import { ProductsService } from '../../shared/services/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  
  public products: Product[] = [];
  public countryId: string;

  constructor(private productsService: ProductsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.countryId = country.id;
        this.productsService.getProducts(country.id).subscribe(product => this.products = product);
      }
    });
  }

  ngOnInit() {
  }

}
