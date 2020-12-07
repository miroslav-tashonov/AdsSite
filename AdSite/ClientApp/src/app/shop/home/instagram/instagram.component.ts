import { Component, OnInit } from '@angular/core';
import { InstagramService } from '../../../shared/services/instagram.service';

@Component({
  selector: 'app-instagram',
  templateUrl: './instagram.component.html',
  styleUrls: ['./instagram.component.scss']
})

export class InstagramComponent implements OnInit {
   
  public instagram;

  constructor(private instaService: InstagramService) { }

  ngOnInit() {
      this.instaService.getInstagramData().subscribe(res => { this.instagram = res.json().data });
  }

  // Slick slider config
  public instaSlideConfig: any = {
    dots: false,
    infinite: true,
    speed: 300,
    slidesToShow: 7,
    slidesToScroll: 7,
    responsive: [{
          breakpoint: 1367,
          settings: {
            slidesToShow: 6,
            slidesToScroll: 6
          }
        },
        {
          breakpoint: 1024,
          settings: {
            slidesToShow: 5,
            slidesToScroll: 5,
            infinite: true
          }
        },
        {
          breakpoint: 600,
          settings: {
            slidesToShow: 4,
            slidesToScroll: 4
          }
        },
        {
          breakpoint: 480,
          settings: {
            slidesToShow: 3,
            slidesToScroll: 3
          }
        }
      ]
    };

}
