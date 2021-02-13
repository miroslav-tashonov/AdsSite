import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Product } from '../../shared/classes/product';
import { ProductsService } from '../../shared/services/products.service';
import { WishlistService } from '../../shared/services/wishlist.service';

@Component({
  selector: 'app-dashboard-my-ads',
  templateUrl: './dashboard-my-ads.component.html',
  styleUrls: ['./dashboard-my-ads.component.scss']
})
export class DashboardMyAdsComponent implements OnInit {
  public product: Observable<Product[]> = of([]);
  public wishlistItems: Product[] = [];

  constructor(private router: Router, private wishlistService: WishlistService,
    private productsService: ProductsService) {
    this.product = this.wishlistService.getProducts();
    this.product.subscribe(products => this.wishlistItems = products);
  }

  ngOnInit() { }

  // Remove from list
  public removeItem(product: Product) {
    this.wishlistService.removeFromWishlist(product);
  }
}
