import { LocationStrategy } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError, first, map, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CountryModel } from '../classes/country';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService{
  appUrl: string;
  countryApiUrl: string;
  public countryId?: string;

  constructor(private notificationService: NotificationService, private http: HttpClient, private locationStrategy: LocationStrategy) {
    this.appUrl = environment.appUrl;
    this.countryApiUrl = 'api/CountriesApi/getCountryId';
    this.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.countryId = country.id;
      }
    });
  }

  getCountryId(Path: string) {
    return this.http.post<CountryModel>(this.appUrl + this.countryApiUrl, { Path }).pipe(
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

    this.notificationService.showError(errorMessage, "Getting countryId error!");

    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
