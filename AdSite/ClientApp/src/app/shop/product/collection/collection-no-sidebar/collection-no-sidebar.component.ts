import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { Product } from '../../../../shared/classes/product';
import { ProductsService } from '../../../../shared/services/products.service';
import { PaginationService } from '../../../../shared/classes/paginate'
import { trigger, transition, style, animate } from "@angular/animations";
import * as _ from 'lodash'
import * as $ from 'jquery';

@Component({
  selector: 'app-collection-no-sidebar',
  templateUrl: './collection-no-sidebar.component.html',
  styleUrls: ['./collection-no-sidebar.component.scss'],
  animations: [  // angular animation
    trigger('Animation', [
      transition('* => fadeOut', [
        style({ opacity: 0.1 }),
        animate(1000, style({ opacity: 0.1 }))
      ]),
      transition('* => fadeIn', [
         style({ opacity: 0.1 }),
         animate(1000, style({ opacity: 0.1 }))
      ])
    ])
  ]
})
export class CollectionNoSidebarComponent implements OnInit {

  private allProduct   :   Product[] = [];
  public  products     :   Product[] = [];
  public  sortByOrder  :   string = '';   // sorting
  public  animation    :   any;   // Animation
  paginate: any = {};

  constructor(private route: ActivatedRoute, private router: Router,
    private productsService: ProductsService, private paginateService: PaginationService) { 
       this.route.params.subscribe(params => {
          const category = params['category'];
          this.productsService.getProductByCategory(category).subscribe(products => {
             this.allProduct = products
             this.setPage(1);
          })
       });
  }

  ngOnInit() {  }
 
  // Animation Effect fadeIn
  public fadeIn() {
      this.animation = 'fadeIn';
  }

  // Animation Effect fadeOut
  public fadeOut() {
      this.animation = 'fadeOut';
  }

  public twoCol() {
    if ($('.product-wrapper-grid').hasClass("list-view")) {} else {
      $(".product-wrapper-grid").children().children().children().removeClass();
      $(".product-wrapper-grid").children().children().children().addClass("col-lg-6");
    }
  }

  public threeCol() {
    if ($('.product-wrapper-grid').hasClass("list-view")) {} else {
      $(".product-wrapper-grid").children().children().children().removeClass();
      $(".product-wrapper-grid").children().children().children().addClass("col-lg-4");
    }
  }

  public fourCol() {
    if ($('.product-wrapper-grid').hasClass("list-view")) {} else {
      $(".product-wrapper-grid").children().children().children().removeClass();
      $(".product-wrapper-grid").children().children().children().addClass("col-lg-3");
    }
  }

  public sixCol() {
    if ($('.product-wrapper-grid').hasClass("list-view")) {} else {
      $(".product-wrapper-grid").children().children().children().removeClass();
      $(".product-wrapper-grid").children().children().children().addClass("col-lg-2");
    }
  }

  // sorting type ASC / DESC / A-Z / Z-A etc.
  public onChangeSorting(val) {
     this.sortByOrder = val;
     this.animation == 'fadeOut' ? this.fadeIn() : this.fadeOut(); // animation
  }

  public setPage(page: number) {
        // get paginate object from service
        this.paginate = this.paginateService.getPager(this.allProduct.length, page);
        // get current page of items
        this.products = this.allProduct.slice(this.paginate.startIndex, this.paginate.endIndex + 1);      
    }

}
