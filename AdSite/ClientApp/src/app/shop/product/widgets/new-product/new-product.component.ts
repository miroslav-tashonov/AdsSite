import { Component, OnInit, Input } from '@angular/core';
import { Product } from '../../../../shared/classes/product';
import { ProductsService } from '../../../../shared/services/products.service';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.scss']
})
export class NewProductComponent implements OnInit {
  public start: number;
  public end: number;
  public products : Product[] = [];	

  constructor(private productsService: ProductsService) { }

  ngOnInit() {
    this.productsService.getLatestProducts().subscribe(product => this.products = product);
  }

}
