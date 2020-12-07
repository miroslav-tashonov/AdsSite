import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { Product } from '../../../../shared/classes/product';
import { ProductsService } from '../../../../shared/services/products.service';
import { WishlistService } from '../../../../shared/services/wishlist.service';
import { CartService } from '../../../../shared/services/cart.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-product-left-image',
  templateUrl: './product-left-image.component.html',
  styleUrls: ['./product-left-image.component.scss']
})
export class ProductLeftImageComponent implements OnInit {

  public product            :   Product = {};
  public products           :   Product[] = [];
  public counter            :   number = 1; 
  public selectedSize       :   any = '';
  public screenWidth       
  public slideRightNavConfig
  
  //Get Product By Id
  constructor(private route: ActivatedRoute, private router: Router,
    public productsService: ProductsService, private wishlistService: WishlistService,
    private cartService: CartService) {
      this.route.params.subscribe(params => {
        const id = +params['id'];
        this.productsService.getProduct(id).subscribe(product => this.product = product)
      });
      this.onResize();
  }

  ngOnInit() {
    this.productsService.getProducts().subscribe(product => this.products = product);
  }
  
  public slideRightConfig = {
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    fade: true,
    asNavFor: '.slider-right-nav'
  }

  @HostListener('window:resize', ['$event'])
  onResize(event?) {
      this.screenWidth = window.innerWidth;
      if (this.screenWidth > 576) {
         return this.slideRightNavConfig = { 
            vertical: true,
            verticalSwiping: true,
            slidesToShow: 3,
            slidesToScroll: 1,
            asNavFor: '.product-right-slick',
            arrows: false,
            infinite: true,
            dots: false,
            centerMode: false,
            focusOnSelect: true
        }
     } else {
        return this.slideRightNavConfig = { 
            vertical: false,
            verticalSwiping: false,
            slidesToShow: 3,
            slidesToScroll: 1,
            asNavFor: '.product-right-slick',
            arrows: false,
            infinite: true,
            centerMode: false,
            dots: false,
            focusOnSelect: true,
            responsive: [
                  {
                      breakpoint: 576,
                      settings: {
                          slidesToShow: 3,
                          slidesToScroll: 1
                      }
                  }
            ]
        }
     }
  }

  

  public increment() { 
      this.counter += 1;
  }

  public decrement() {
      if(this.counter >1){
          this.counter -= 1;
      }
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
