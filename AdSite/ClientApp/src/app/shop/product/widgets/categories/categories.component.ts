import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Menu } from '../../../../shared/header/widgets/left-menu/left-menu-items';
import { CategoriesService } from '../../../../shared/services/categories.service';
import { CountryService } from '../../../../shared/services/country.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {

  public menuItems: Menu[];

  constructor(private countryService: CountryService, private locationStrategy: LocationStrategy, private categoriesService: CategoriesService) {
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.categoriesService.getCategoriesTreeMenu(country.id).subscribe(
          result => {
            this.menuItems = result;
          });
      }
    });
  }
  
  // collapse toggle
  ngOnInit() {
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

  // For mobile view
  public mobileFilterBack() {
     $('.collection-filter').css("left", "-365px");
  }

}
