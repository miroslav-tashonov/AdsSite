import { LocationStrategy } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SupportedLanguagesModel } from '../models/SupportedLanguagesModel';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  appUrl: string;
  languageApiUrl: string;

  constructor(private notificationService: NotificationService, private http: HttpClient) {
    this.appUrl = environment.appUrl;
    this.languageApiUrl = 'api/LanguageApi/getSupportedCultures';
  }

  getSupportedCultures() {
    return this.http.get<SupportedLanguagesModel[]>(this.appUrl + this.languageApiUrl)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  };


  errorHandler(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    this.notificationService.showError(errorMessage, "Supported Languages error!");

    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
