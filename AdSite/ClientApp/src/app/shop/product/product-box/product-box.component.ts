import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../../../shared/classes/product';
import { ProductsService } from '../../../shared/services/products.service';
import { WishlistService } from '../../../shared/services/wishlist.service';
import { CartService } from '../../../shared/services/cart.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-product-box',
  templateUrl: './product-box.component.html',
  styleUrls: ['./product-box.component.scss']
})
export class ProductBoxComponent implements OnInit {

  @Input() product : Product;

  public variantImage   :   any = ''; 

  constructor(private router: Router, public productsService: ProductsService,
   private wishlistService: WishlistService, private cartService: CartService) { 
  }

  ngOnInit() { }

  // Add to cart
  public addToCart(product: Product,  quantity: number = 1) {
    this.cartService.addToCart(product,quantity);
  }

  // Add to compare
  public addToCompare(product: Product) {
     this.productsService.addToCompare(product);
  }

  // Add to wishlist
  public addToWishlist(product: Product) {
     this.wishlistService.addToWishlist(product);
  }

  // Change variant images
  public changeVariantImage(image) {
     this.variantImage = image;
  }  
 
}
