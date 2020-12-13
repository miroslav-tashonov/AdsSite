import { Component, OnInit, Input } from '@angular/core';
import { Product } from '../../../../shared/classes/product';
import { ProductsService } from '../../../../shared/services/products.service';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.scss']
})
export class NewProductComponent implements OnInit {
  
  public products : Product[] = [];	

  slideConfig = {
    "slidesToShow": 3
  };

  constructor(private productsService: ProductsService) { }

  ngOnInit() {
    this.productsService.getLatestProducts().subscribe(product => this.products = product);
  }

}
