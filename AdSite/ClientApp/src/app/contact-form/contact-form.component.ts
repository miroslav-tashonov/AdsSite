import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WebSettingsModel } from '../models/WebSettingsModel';
import { WebSettingsService } from '../services/web-settings.service';

@Component({
  selector: 'contact',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {
  webSettings$: Observable<WebSettingsModel> | undefined;

  constructor(private webSettingsService: WebSettingsService) {
    this.webSettings$ = this.webSettingsService.getWebSettingsModel('99DE8181-09A8-41DB-895E-54E5E0650C3A');
  }

  ngOnInit(): void {
  }
}
