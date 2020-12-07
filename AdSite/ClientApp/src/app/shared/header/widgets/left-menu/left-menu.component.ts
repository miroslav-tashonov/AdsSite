import { Component, OnInit } from '@angular/core';
import { MENUITEMS, Menu } from './left-menu-items';
declare var $: any;

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.scss']
})
export class LeftMenuComponent implements OnInit {
  
  public menuItems: Menu[];

  constructor() { }

  ngOnInit() { 
     this.menuItems = MENUITEMS.filter(menuItem => menuItem);
  }

}
