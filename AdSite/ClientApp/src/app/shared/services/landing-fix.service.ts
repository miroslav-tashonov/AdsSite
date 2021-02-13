import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from "@angular/common";
import * as $ from 'jquery';

@Injectable({
  providedIn: 'root'
})

export class LandingFixService {
   
  constructor(@Inject(DOCUMENT) private document: Document) { }
  
  // Add or Remove class
  public addNavFix() {
     this.document.getElementById("mySidenav").classList.add('open-side');
  }

  public removeNavFix() {
     this.document.getElementById("mySidenav").classList.remove('open-side');
  }
  
}
