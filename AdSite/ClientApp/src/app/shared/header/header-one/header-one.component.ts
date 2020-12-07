import { Component, Inject, HostListener, OnInit, ViewEncapsulation } from '@angular/core';
import { LandingFixService } from '../../services/landing-fix.service';
import { DOCUMENT } from "@angular/common";
import { WINDOW } from '../../services/windows.service';
import { CartItem } from '../../classes/cart-item';
import { CartService } from '../../services/cart.service';
import { Observable, of } from 'rxjs';
declare var $: any;

@Component({
  selector: 'app-header-one',
  templateUrl: './header-one.component.html',
  styleUrls: ['./header-one.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderOneComponent implements OnInit {
  
  public shoppingCartItems  :   CartItem[] = [];
  
  constructor(@Inject(DOCUMENT) private document: Document,
    @Inject(WINDOW) private window,private fix: LandingFixService, private cartService: CartService) { 
    this.cartService.getItems().subscribe(shoppingCartItems => this.shoppingCartItems = shoppingCartItems);
  }

  ngOnInit() { 
    $.getScript('assets/js/menu.js');
  }

  openNav() {
  	this.fix.addNavFix();
  }

  closeNav() {
     this.fix.removeNavFix();
  }

  // @HostListener Decorator
  @HostListener("window:scroll", [])
  onWindowScroll() {
    let number = this.window.pageYOffset || this.document.documentElement.scrollTop || this.document.body.scrollTop || 0;
      if (number >= 300) { 
        this.document.getElementById("sticky").classList.add('fixed');
      } else {
        this.document.getElementById("sticky").classList.remove('fixed');
      }
  }

}
