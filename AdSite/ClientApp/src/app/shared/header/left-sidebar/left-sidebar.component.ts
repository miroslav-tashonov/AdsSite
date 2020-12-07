import { Component, OnInit } from '@angular/core';
import { LandingFixService } from '../../services/landing-fix.service';
declare var $: any;

@Component({
  selector: 'app-left-sidebar',
  templateUrl: './left-sidebar.component.html',
  styleUrls: ['./left-sidebar.component.scss']
})
export class LeftSidebarComponent implements OnInit {

  constructor(private fix: LandingFixService) { }

  ngOnInit() {
  	$.getScript('assets/js/menu.js');
  }

  closeNav() {
     this.fix.removeNavFix();
  }

}
