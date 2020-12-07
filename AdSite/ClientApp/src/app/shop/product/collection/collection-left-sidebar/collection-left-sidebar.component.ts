import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { trigger, transition, style, animate } from "@angular/animations";
import { Product, ColorFilter, TagFilter } from '../../../../shared/classes/product';
import { ProductsService } from '../../../../shared/services/products.service';
import * as _ from 'lodash'
import * as $ from 'jquery';

@Component({
  selector: 'app-collection-left-sidebar',
  templateUrl: './collection-left-sidebar.component.html',
  styleUrls: ['./collection-left-sidebar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
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
export class CollectionLeftSidebarComponent implements OnInit {

  public products     :   Product[] = [];
  public items        :   Product[] = [];
  public allItems     :   Product[] = [];
  public colorFilters :   ColorFilter[] = [];
  public tagsFilters  :   any[] = [];
  public tags         :   any[] = [];
  public colors       :   any[] = [];
  public sortByOrder  :   string = '';   // sorting
  public animation    :   any;   // Animation

  lastKey = ''      // key to offset next query from
  finished = false  // boolean when end of data is reached
  
  constructor(private route: ActivatedRoute, private router: Router,
    private productsService: ProductsService) { 
       this.route.params.subscribe(params => {
          const category = params['category'];
          this.productsService.getProductByCategory(category).subscribe(products => {
             this.allItems = products  // all products
             this.products = products.slice(0,8)
             this.getTags(products)
             this.getColors(products)
          })
       });
  }

  ngOnInit() {  
    
  }
  
  // Get current product tags
  public getTags(products) {
     var uniqueBrands = []
     var itemBrand = Array();
     products.map((product, index) => { 
        if(product.tags) {
           product.tags.map((tag) => {
           const index = uniqueBrands.indexOf(tag);
           if(index === -1)  uniqueBrands.push(tag);
        })
       }
     });
     for (var i = 0; i < uniqueBrands.length; i++) {
          itemBrand.push({brand:uniqueBrands[i]})
     }
     this.tags = itemBrand
  }
  
  // Get current product colors
  public getColors(products) {
     var uniqueColors = []
     var itemColor = Array();
     products.map((product, index) => {
       if(product.colors) {
       product.colors.map((color) => {
           const index = uniqueColors.indexOf(color);
           if(index === -1)  uniqueColors.push(color);
       })
      }
     });
     for (var i = 0; i < uniqueColors.length; i++) {
          itemColor.push({color:uniqueColors[i]})
     }
     this.colors = itemColor
  }

   
  // Animation Effect fadeIn
  public fadeIn() {
      this.animation = 'fadeIn';
  }

  // Animation Effect fadeOut
  public fadeOut() {
      this.animation = 'fadeOut';
  }

 
  // Initialize filetr Items
  public filterItems(): Product[] {
      return this.items.filter((item: Product) => {
          const Colors: boolean = this.colorFilters.reduce((prev, curr) => { // Match Color
            if(item.colors){
              if (item.colors.includes(curr.color)) {
                return prev && true;
              } 
            }
          }, true);
          const Tags: boolean = this.tagsFilters.reduce((prev, curr) => { // Match Tags
            if(item.tags) {
              if (item.tags.includes(curr)) {
                return prev && true;
              } 
            }
          }, true);
          return Colors && Tags; // return true
      });
  }
  
  // Update tags filter
  public updateTagFilters(tags: any[]) {
      this.tagsFilters = tags;
      this.animation == 'fadeOut' ? this.fadeIn() : this.fadeOut(); // animation
  }

  // Update color filter
  public updateColorFilters(colors: ColorFilter[]) {
      this.colorFilters = colors;
      this.animation == 'fadeOut' ? this.fadeIn() : this.fadeOut(); // animation
  }
  
  // Update price filter
  public updatePriceFilters(price: any) {
      let items: any[] = [];
      this.products.filter((item: Product) => {
          if (item.price >= price[0] && item.price <= price[1] )  {            
             items.push(item); // push in array
          } 
      });
      this.items = items;
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

  // For mobile filter view
  public mobileFilter() {
    $('.collection-filter').css("left", "-15px");
  }

  // Infinite scroll
  public onScroll() {
      this.lastKey = _.last(this.allItems)['id'];
      if (this.lastKey != _.last(this.items)['id']) {
         this.finished = false
      }   
      // If data is identical, stop making queries
      if (this.lastKey == _.last(this.items)['id']) {
         this.finished = true
      }
      if(this.products.length < this.allItems.length){  
         let len = this.products.length;
         for(var i = len; i < len+4; i++){
           if(this.allItems[i] == undefined) return true
             this.products.push(this.allItems[i]);
         }
      }
  }
  
  // sorting type ASC / DESC / A-Z / Z-A etc.
  public onChangeSorting(val) {
     this.sortByOrder = val;
     this.animation == 'fadeOut' ? this.fadeIn() : this.fadeOut(); // animation
  }

}
