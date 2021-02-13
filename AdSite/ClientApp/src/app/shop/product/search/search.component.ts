import { Component, OnInit } from '@angular/core';
import { Product } from '../../../shared/classes/product';
import { ProductsService } from '../../../shared/services/products.service';
import { trigger, transition, style, animate } from "@angular/animations";
import { CountryService } from '../../../shared/services/country.service';
import { LocationStrategy } from '@angular/common';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  animations: [  // angular animation
    trigger('Animation', [
      transition('* => fadeOut', [
        style({ opacity: 0.1 }),
        animate(1000, style({ opacity: 0.1 }))
      ]),
      transition('* => fadeIn', [
         style({ opacity: 0.1 }),
         animate(1000, style({ opacity: 0.1 }))
      ])
    ])
  ]
})
export class SearchComponent implements OnInit {
  
  public products          :   Product[] = [];  
  public searchProducts    :   Product[] = [];	
  public animation         :   any;
  public searchTerms       :   any = '';

  constructor(private productsService: ProductsService, private countryService: CountryService, private locationStrategy: LocationStrategy) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.productsService.getProducts(country.id).subscribe(product => this.products = product);
      }
    });
  }

  ngOnInit() {}

  public searchTerm(term: string, keys: string = 'name') {
      let res = (this.products || []).filter((item) => keys.split(',').some(key => item.hasOwnProperty(key)  &&  new RegExp(term, 'gi').test(item[key])));
         this.searchProducts = res
 }
 
}
