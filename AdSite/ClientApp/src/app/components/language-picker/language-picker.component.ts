import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { SupportedLanguagesModel } from '../../models/SupportedLanguagesModel';
import { LanguageService } from '../../services/language.service';

@Component({
  selector: 'app-language-picker',
  templateUrl: './language-picker.component.html',
  styleUrls: ['./language-picker.component.css']
})
export class LanguagePickerComponent implements OnInit {

  @Input() countryId?: string;
  supportedLanguages$: Observable<SupportedLanguagesModel[]> | undefined;

  constructor(private languageService: LanguageService) {
    this.supportedLanguages$ = this.languageService.getSupportedCultures();
  }

  ngOnInit(): void {
  }

}
