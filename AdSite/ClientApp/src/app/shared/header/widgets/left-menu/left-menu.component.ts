import { Component, OnInit } from '@angular/core';
import { CategoriesService } from '../../../services/categories.service';
import { MENUITEMS, Menu } from './left-menu-items';
declare var $: any;

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.scss']
})
export class LeftMenuComponent implements OnInit {
  
  public menuItems: Menu[];

  constructor(private categoriesService: CategoriesService) { }

  ngOnInit() {
    //todo
    //this.menuItems = MENUITEMS.filter(menuItem => menuItem);
    this.categoriesService.getCategoriesTreeMenu('99DE8181-09A8-41DB-895E-54E5E0650C3A').subscribe(
      result => {
        this.menuItems = result;
      }
    );
  }

}
