import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logo',
  templateUrl: './logo.component.html',
  styleUrls: ['./logo.component.scss']
})
export class LogoComponent implements OnInit {

  constructor() { }

  ngOnInit() { }
  
  // Slick slider config
  public logoSlideConfig: any = {
    dots: false,
    infinite: true,
    speed: 300,
    slidesToShow: 6,
    slidesToScroll: 6,
    responsive: [{
        breakpoint: 1367,
        settings: {
          slidesToShow: 5,
          slidesToScroll: 5,
          infinite: true
        }
      },
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 4,
          slidesToScroll: 4,
          infinite: true
        }
      },
      {
        breakpoint: 767,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2
        }
      }
    ]
  };

  // Logo
  public logo = [{
      image: 'assets/images/logos/1.png',
    }, {
      image: 'assets/images/logos/2.png',
    }, {
      image: 'assets/images/logos/3.png',
    }, {
      image: 'assets/images/logos/4.png',
    }, {
      image: 'assets/images/logos/5.png',
    }, {
      image: 'assets/images/logos/6.png',
    }, {
      image: 'assets/images/logos/7.png',
    }, {
      image: 'assets/images/logos/8.png',        
  }]

}
