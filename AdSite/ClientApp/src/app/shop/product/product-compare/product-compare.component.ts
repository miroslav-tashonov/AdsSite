import { Component, OnInit } from '@angular/core';
import { Product } from '../../../shared/classes/product';
import { ProductsService } from '../../../shared/services/products.service';
import { CartService } from '../../../shared/services/cart.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-product-compare',
  templateUrl: './product-compare.component.html',
  styleUrls: ['./product-compare.component.scss']
})
export class ProductCompareComponent implements OnInit {

  public product            :   Observable<Product[]> = of([]);
  public products           :   Product[] = [];

  constructor(private productsService: ProductsService,
    private cartService: CartService) { }

  ngOnInit() {
  	this.product = this.productsService.getComapreProducts();
    this.product.subscribe(products => this.products = products);
  }

  // Add to cart
  public addToCart(product: Product, quantity: number = 1) {
     this.cartService.addToCart(product, quantity);
  }
  
  // Remove from compare list
  public removeItem(product: Product) {
    this.productsService.removeFromCompare(product);
  }

}
