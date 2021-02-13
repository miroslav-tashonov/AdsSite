import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../classes/product';
import { BehaviorSubject, Observable, Subscriber } from 'rxjs';
import { map, filter } from 'rxjs/operators';

// Get product from Localstorage
let products = JSON.parse(localStorage.getItem("wishlistItem")) || [];

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  
  // wishlist array
  public wishlistProducts: BehaviorSubject<Product[]> = new BehaviorSubject([]);
  public observer   :  Subscriber<{}>;

  // Initialize 
  constructor(private toastrService: ToastrService) { 
    this.wishlistProducts.subscribe(products => products = products);
  }

  // Get  wishlist Products
  public getProducts(): Observable<Product[]> {
    const itemsStream = new Observable(observer => {
      observer.next(products);
      observer.complete();
    });
    return <Observable<Product[]>>itemsStream;
  }

  // If item is aleready added In wishlist
  public hasProduct(product: Product): boolean {
    const item = products.find(item => item.id === product.id);
    return item !== undefined;
  }

  // Add to wishlist
  public addToWishlist(product: Product): Product | boolean {
    var item: Product | boolean = false;
    if (this.hasProduct(product)) {
      item = products.filter(item => item.id === product.id)[0];
      const index = products.indexOf(item);
    } else {
      products.push(product);
    }
      this.toastrService.success('This product added to Wishlist.'); // toasr services
      localStorage.setItem("wishlistItem", JSON.stringify(products));
      return item;
  }

  // Removed Product
  public removeFromWishlist(product: Product) {
    if (product === undefined) { return; }
    const index = products.indexOf(product);
    products.splice(index, 1);
    localStorage.setItem("wishlistItem", JSON.stringify(products));
  }
  

}
