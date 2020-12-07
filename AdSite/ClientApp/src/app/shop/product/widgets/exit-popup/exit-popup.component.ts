import { Component, OnInit } from '@angular/core';
import exitIntent from 'exit-intent'
declare var $: any;

if(localStorage.getItem('exitState') != 'exitshown'){
  const removeExitIntent = exitIntent({
    threshold: 50,
    maxDisplays: 1,
    eventThrottle: 100,
    onExitIntent: () => {
      $('#exit_popup').modal('show');
      localStorage.setItem('exitState','exitshown')
    }    
  })
}


@Component({
  selector: 'app-exit-popup',
  templateUrl: './exit-popup.component.html',
  styleUrls: ['./exit-popup.component.scss']
})
export class ExitPopupComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
