import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Product } from '../../../../shared/classes/product';
import { CartItem } from '../../../../shared/classes/cart-item';
import { ProductsService } from '../../../../shared/services/products.service';
import { CartService } from '../../../../shared/services/cart.service';
declare var $: any;

@Component({
  selector: 'app-quick-view',
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements OnInit, OnDestroy {

  @Input() products: Product;


  //public products           :   Product[] = [];
  public counter            :   number = 1;
  public variantImage       :   any = '';
  public selectedColor      :   any = '';
  public selectedSize       :   any = '';
  
  constructor(private productsService: ProductsService,
  	private cartService: CartService) { }

  ngOnInit() {
  }

  ngOnDestroy() {
    $('.quickviewm').modal('hide');
  }

  public increment() { 
      this.counter += 1;
  }

  public decrement() {
      if(this.counter >1){
          this.counter -= 1;
      }
  }
  
   // Change variant images
  public changeVariantImage(image) {
     this.variantImage = image;
     this.selectedColor = image;
  }

  // Change variant
  public changeVariantSize(variant) {
     this.selectedSize = variant;
  }

  public addToCart(product: Product, quantity) {
    if (quantity == 0) return false;
    this.cartService.addToCart(product, parseInt(quantity));
  }

}
