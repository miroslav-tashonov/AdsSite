import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalizationService } from '../../services/localization.service';

@Component({
  selector: 'app-localization',
  templateUrl: './localization.component.html'
})

export class LocalizationComponent implements OnInit {
  @Input() localizationKey: string;

  localizationValue$: Observable<string> | undefined;

  constructor(private localizationService: LocalizationService) {
    this.localizationKey = '';
    this.loadLocalization();
  }

  ngOnInit() {
    this.loadLocalization();
  }

  loadLocalization() {
    this.localizationValue$ = this.localizationService.getByLocalizationKey(this.localizationKey);
  }

}
