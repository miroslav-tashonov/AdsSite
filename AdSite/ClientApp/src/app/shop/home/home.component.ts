import { Component, OnInit } from '@angular/core';
import { Product } from '../../shared/classes/product';
import { ProductsService } from '../../shared/services/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  
  public products: Product[] = [];
  
  constructor(private productsService: ProductsService) {   }

  ngOnInit() {
  	this.productsService.getProducts().subscribe(product => this.products = product);
  }

}
