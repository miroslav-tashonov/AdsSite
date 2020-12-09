import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { NotificationService } from './notification.service';
import { WebSettingsModel } from '../classes/web-settings';
import { CountryService } from './country.service';

@Injectable({
  providedIn: 'root'
})
export class WebSettingsService {
  webSettingsModel$: Observable<WebSettingsModel>;

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private notificationService: NotificationService, private http: HttpClient, private countryService: CountryService) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/WebSettingsApi/';
  }

  getWebSettingsModel(countryId?: string): Observable<WebSettingsModel> {
    return this.http.get<WebSettingsModel>(this.myAppUrl + this.myApiUrl + countryId)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    this.notificationService.showError(errorMessage, "Getting web settings error!");

    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
