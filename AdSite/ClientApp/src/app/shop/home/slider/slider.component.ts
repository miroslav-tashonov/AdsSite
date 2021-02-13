import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements OnInit {

  constructor() { }

  ngOnInit() {  }
  
  // Slick slider config
  public sliderConfig: any = {
    autoplay: true,
    autoplaySpeed: 3000
  };

}
