import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-users-panel',
  templateUrl: './users-panel.component.html'
})
export class UsersPanelComponent implements OnInit {

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    
  }

  ngOnInit(): void {
    this.router.navigate([{ outlets: { sub: ['myads'] } }], { relativeTo: this.activatedRoute });
  }

}
