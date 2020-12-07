import { Component, OnInit, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import 'rxjs/add/observable/interval';

@Component({
  selector: 'app-price',
  templateUrl: './price.component.html',
  styleUrls: ['./price.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class PriceComponent implements OnInit {
  
  // Using Output EventEmitter
  @Output() priceFilters = new EventEmitter();
	
  // define min, max and range
  public min : number = 100;
  public max : number = 1000;
  public range = [100,1000];
  
  constructor() { }
  
  ngOnInit() {  }

  // rangeChanged
  priceChanged(event:any) {
    setInterval(() => {
      this.priceFilters.emit(event);
    }, 1000);
  }

}
