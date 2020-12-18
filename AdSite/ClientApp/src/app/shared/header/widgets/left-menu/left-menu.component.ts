import { LocationStrategy } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriesService } from '../../../services/categories.service';
import { CountryService } from '../../../services/country.service';
import { MENUITEMS, Menu } from './left-menu-items';
declare var $: any;

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.scss']
})
export class LeftMenuComponent implements OnInit {

  public menuItems: Observable<Menu[]>;

  constructor(private categoriesService: CategoriesService, private countryService: CountryService, private locationStrategy: LocationStrategy) { }

  ngOnInit() {
    //todo
    //this.menuItems = MENUITEMS.filter(menuItem => menuItem);
    this.countryService.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.menuItems = this.categoriesService.getCategoriesTreeMenu(country.id);
      }
    });
    
  }

}
