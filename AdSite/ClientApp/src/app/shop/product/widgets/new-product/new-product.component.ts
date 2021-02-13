import { LocationStrategy } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { Product } from '../../../../shared/classes/product';
import { CountryService } from '../../../../shared/services/country.service';
import { ProductsService } from '../../../../shared/services/products.service';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.scss']
})
export class NewProductComponent implements OnInit {
  public start: number;
  public end: number;
  public products: Product[] = [];

  constructor(private productsService: ProductsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.productsService.getLatestProducts(country.id).subscribe(product => this.products = product);
      }
    })
  }

  ngOnInit() {}

}
