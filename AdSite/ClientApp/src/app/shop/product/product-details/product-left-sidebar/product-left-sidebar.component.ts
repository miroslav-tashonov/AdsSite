import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { Product } from '../../../../shared/classes/product';
import { ProductsService } from '../../../../shared/services/products.service';
import { WishlistService } from '../../../../shared/services/wishlist.service';
import { CartService } from '../../../../shared/services/cart.service';
import { Observable, of } from 'rxjs';
import * as $ from 'jquery';

@Component({
  selector: 'app-product-left-sidebar',
  templateUrl: './product-left-sidebar.component.html',
  styleUrls: ['./product-left-sidebar.component.scss']
})
export class ProductLeftSidebarComponent implements OnInit {

  public product            :   Product = {};
  public products           :   Product[] = [];
  public counter            :   number = 1; 
  public selectedSize       :   any = '';
public iStyle               = "width:100%; height:100%;"
  //Get Product By Id
  constructor(private route: ActivatedRoute, private router: Router,
    public productsService: ProductsService, private wishlistService: WishlistService,
    private cartService: CartService) {
      this.route.params.subscribe(params => {
        const id = +params['id'];
        this.productsService.getProduct(id).subscribe(product => this.product = product)
      });
  }

  ngOnInit() {
    this.productsService.getProducts().subscribe(product => this.products = product);
  }
  
  // product zoom 
  onMouseOver(): void {
    document.getElementById("p-zoom").classList.add('product-zoom');
  }

  onMouseOut(): void {
    document.getElementById("p-zoom").classList.remove('product-zoom');
  }

  public slideConfig = {
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    fade: true,
  };

  public slideNavConfig = {
    vertical: false,
    slidesToShow: 3,
    slidesToScroll: 1,
    asNavFor: '.product-slick',
    arrows: false,
    dots: false,
    focusOnSelect: true
  }

  public increment() { 
      this.counter += 1;
  }

  public decrement() {
      if(this.counter >1){
         this.counter -= 1;
      }
  }

  // For mobile filter view
  public mobileSidebar() {
    $('.collection-filter').css("left", "-15px");
  }

  // Add to cart
  public addToCart(product: Product, quantity) {
    if (quantity == 0) return false;
    this.cartService.addToCart(product, parseInt(quantity));
  }

  // Add to cart
  public buyNow(product: Product, quantity) {
     if (quantity > 0) 
       this.cartService.addToCart(product,parseInt(quantity));
       this.router.navigate(['/home/checkout']);
  }

  // Add to wishlist
  public addToWishlist(product: Product) {
     this.wishlistService.addToWishlist(product);
  }

  
  // Change size variant
  public changeSizeVariant(variant) {
     this.selectedSize = variant;
  }

}
