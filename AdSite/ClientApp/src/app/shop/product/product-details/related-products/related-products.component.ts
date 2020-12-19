import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../../shared/classes/product';
import { CountryService } from '../../../../shared/services/country.service';
import { ProductsService } from '../../../../shared/services/products.service';

@Component({
  selector: 'app-related-products',
  templateUrl: './related-products.component.html',
  styleUrls: ['./related-products.component.scss']
})
export class RelatedProductsComponent implements OnInit {

  public products: Product[] = [];

  constructor(private productsService: ProductsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.productsService.getRelatedProducts(country.id).subscribe(product => this.products = product);
      }
    })
  }

  ngOnInit() {}

}
