import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.scss']
})
export class AboutUsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  // testimonial carousel
  public testimonial = [
     {
     image: 'assets/images/eddy_avatar.jpg',
     name: 'Ed',
      designation: 'lead developer',
      description: 'When we get back from where we are going, we will return to where we were. I know people there!',
   }, {
     image: 'assets/images/avtar.jpg',
     name: 'Eddy',
      designation: 'content writer',
      description: 'Good luck catching us as well be invisible to the naked eye thanks to this baking powder vapor barrier! A shroud one might say',
  }]

  // teastimonial slick slider config
  public testimonialsliderconfig = {
    infinite: true,
    slidestoshow: 2,
    slidestoscroll: 2,
    responsive: [
        {
            breakpoint: 991,
            settings: {
                slidestoshow: 1,
                slidestoscroll: 1
            }
        }
    ]
  };

  // team 
  public team = [{
     image: 'assets/images/team/Ed.jpg',
     name: 'Ed',
     designation: 'designer'
   }, {
      image: 'assets/images/team/Edd.jpg',
     name: 'Edd',
     designation: 'content writer'
   }, {
      image: 'assets/images/team/Eddy.jpg',
     name: 'Eddy',
     designation: 'lead developer'
   }]

  // team slick slider config
  public teamsliderconfig = {
      infinite: true,
      speed: 300,
      slidestoshow: 3,
      slidestoscroll: 1,
      autoplay: true,
      autoplayspeed: 3000,
      responsive: [
        {
            breakpoint: 1200,
            settings: {
                slidestoshow: 3,
                slidestoscroll: 3
            }
        },
        {
            breakpoint: 991,
            settings: {
                slidestoshow: 2,
                slidestoscroll: 2
            }
        },
        {
            breakpoint: 586,
            settings: {
                slidestoshow: 2,
                slidestoscroll: 1
            }
        }
     ]
  };

}
