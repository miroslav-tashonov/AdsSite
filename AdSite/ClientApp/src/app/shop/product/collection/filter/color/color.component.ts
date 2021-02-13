import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductColor, ColorFilter } from '../../../../../shared/classes/product';

@Component({
  selector: 'app-color',
  templateUrl: './color.component.html',
  styleUrls: ['./color.component.scss']
})
export class ColorComponent implements OnInit {
  
  public activeItem : any = '';

  // Using Input and Output EventEmitter
  @Input()  colorsFilters  :  ColorFilter[] = [];
  @Output() colorFilters   :  EventEmitter<ColorFilter[]> = new EventEmitter<ColorFilter[]>();

  constructor() { }
  
  ngOnInit() {  }

  // Click to call function 
  public changeColor(colors: ColorFilter) {
    this.activeItem = colors.color
    if(colors.color) {
      this.colorFilters.emit([colors]);
    } else {
      this.colorFilters.emit([]);
    }
  }

}
