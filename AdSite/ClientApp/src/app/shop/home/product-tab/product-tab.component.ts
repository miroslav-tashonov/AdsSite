import { Component, OnInit, Input } from '@angular/core';
import { Product } from '../../../shared/classes/product';
declare var $: any;

@Component({
  selector: 'app-product-tab',
  templateUrl: './product-tab.component.html',
  styleUrls: ['./product-tab.component.scss']
})
export class ProductTabComponent implements OnInit {

  @Input() products: Product;

  constructor() { }

  ngOnInit() {
    // tab js
  	  $("#tab-1").css("display", "Block");
	  $(".default").css("display", "Block");
	  $(".tabs li a").on('click', function() {
	    event.preventDefault();
	    $(this).parent().parent().find("li").removeClass("current");
	    $(this).parent().addClass("current");
	    var currunt_href = $(this).attr("href");
	    $('#' + currunt_href).show();
	    $(this).parent().parent().parent().find(".tab-content").not('#' + currunt_href).css("display", "none");
	  });
  }


}
