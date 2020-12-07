import { Component, OnInit } from '@angular/core';
import { MENUITEMS, Menu } from './navbar-items';
import { Router, ActivatedRoute } from "@angular/router";
declare var $: any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  
  public menuItems: Menu[];

  constructor() { }

  ngOnInit() {
  	this.menuItems = MENUITEMS.filter(menuItem => menuItem);
  }

}
