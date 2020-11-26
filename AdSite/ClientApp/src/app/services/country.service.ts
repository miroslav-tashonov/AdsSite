import { LocationStrategy } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CountryModel } from '../models/CountryModel';

@Injectable({
  providedIn: 'root'
})
export class CountryService{
  appUrl: string;
  countryApiUrl: string;
  public countryId?: string;

  constructor(private http: HttpClient, private locationStrategy: LocationStrategy) {
    this.appUrl = environment.appUrl;
    this.countryApiUrl = 'api/CountriesApi/getCountryId';
    this.getCountryId(this.locationStrategy.getBaseHref()).subscribe({
      next: country => {
        this.countryId = country.id;
      }
    });
  }

  getCountryId(Path: string) {
    return this.http.post<CountryModel>(this.appUrl + this.countryApiUrl, { Path });
  };
}
