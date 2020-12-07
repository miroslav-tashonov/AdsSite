import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-collection-banner',
  templateUrl: './collection-banner.component.html',
  styleUrls: ['./collection-banner.component.scss']
})
export class CollectionBannerComponent implements OnInit {

  constructor() { }

  ngOnInit() { }

  // Collection banner
  public category = [{
    image: 'assets/images/sub-banner1.jpg',
    save: 'save 50%',
    title: 'men',
    link: '/home/left-sidebar/collection/men'
  }, {
    image: 'assets/images/sub-banner2.jpg',
    save: 'save 50%',
    title: 'women',
    link: '/home/left-sidebar/collection/women'
  }]

}
