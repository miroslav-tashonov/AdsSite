import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { Product } from '../../../shared/classes/product';
import { ProductsService } from '../../../shared/services/products.service';
import { WishlistService } from '../../../shared/services/wishlist.service';
import { CartService } from '../../../shared/services/cart.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {

  public product        :   Observable<Product[]> = of([]);
  public wishlistItems  :   Product[] = [];

  constructor(private router: Router, private wishlistService: WishlistService,
  private productsService: ProductsService, private cartService: CartService) { 
    this.product = this.wishlistService.getProducts();
    this.product.subscribe(products => this.wishlistItems = products);
  }

  ngOnInit() { }

  // Add to cart
  public addToCart(product: Product,  quantity: number = 1) {
     if (quantity > 0)
      this.cartService.addToCart(product,quantity);
      this.wishlistService.removeFromWishlist(product);
  }
  
  // Remove from wishlist
  public removeItem(product: Product) {
    this.wishlistService.removeFromWishlist(product);
  }

}
