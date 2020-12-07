import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
  selector: 'app-newsletter',
  templateUrl: './newsletter.component.html',
  styleUrls: ['./newsletter.component.scss']
})
export class NewsletterComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  	if(localStorage.getItem('entryState') != 'newsletter'){
  		$('.newsletterm').modal('show');
  		localStorage.setItem('entryState','newsletter');
  	}
  }

}
