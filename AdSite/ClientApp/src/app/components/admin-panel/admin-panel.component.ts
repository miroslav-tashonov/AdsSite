import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html'
})
export class AdminPanelComponent implements OnInit {

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    
  }

  ngOnInit(): void {
    this.router.navigate([{ outlets: { subAdmin: ['manageusers'] } }], { relativeTo: this.activatedRoute });
  }

}
