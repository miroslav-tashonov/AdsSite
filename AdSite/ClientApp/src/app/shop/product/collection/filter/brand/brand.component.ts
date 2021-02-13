import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductTags, TagFilter } from '../../../../../shared/classes/product';
declare var $: any;

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {
  
  // Using Input nad Output EventEmitter
  @Input()  tagsFilters : ProductTags[] = [];
  @Output() tagFilters  : EventEmitter<ProductTags[]> = new EventEmitter<ProductTags[]>();
  
  // Array
  public checkedTagsArray: any[] = [];
  
  constructor() { }

  ngOnInit() {  
  	  this.tagFilters.emit(this.checkedTagsArray);   // Pass value Using emit 
      $('.collapse-block-title').on('click', function(e) {
        e.preventDefault;
        var speed = 300;
        var thisItem = $(this).parent(),
          nextLevel = $(this).next('.collection-collapse-block-content');
        if (thisItem.hasClass('open')) {
          thisItem.removeClass('open');
          nextLevel.slideUp(speed);
        } else {
          thisItem.addClass('open');
          nextLevel.slideDown(speed);
        }
    });
  }

  // value checked call this function
  checkedFilter(event){
      let index = this.checkedTagsArray.indexOf(event.target.value);  // checked and unchecked value
       if (event.target.checked)   
           this.checkedTagsArray.push(event.target.value); // push in array cheked value
        else 
           this.checkedTagsArray.splice(index,1);  // removed in array unchecked value           
  }

  

}
