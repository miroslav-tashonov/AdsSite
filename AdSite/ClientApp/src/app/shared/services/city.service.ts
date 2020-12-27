import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, throwError } from 'rxjs';
import { NotificationService } from './notification.service';
import { environment } from 'src/environments/environment';
import { CityCreateModel, CityEditModel, CityModel } from '../classes/city';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  myAppUrl: string;
  myApiUrl: string;

  dataChange: BehaviorSubject<CityModel[]> = new BehaviorSubject<CityModel[]>([]);
  // Temporarily stores data from dialogs
  dialogData: any;

  constructor(private httpClient: HttpClient, private notificationService: NotificationService) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/CitiesApi/';
  }

  get data(): Observable<CityModel[]> {
    return this.dataChange;
  }

  getDialogData() {
    return this.dialogData;
  }

  /** CRUD METHODS */
  getAllCities(countryId: string): void {
    this.httpClient.get<CityModel[]>(this.myAppUrl + this.myApiUrl + countryId).subscribe(data => {
      this.dataChange.next(data);
    },
      (error: HttpErrorResponse) => {
        console.log(error.name + ' ' + error.message);
      });
  }

  // ADD, POST METHOD
  addItem(city: CityCreateModel): void {
    this.httpClient.post(this.myAppUrl + this.myApiUrl, city).subscribe(data => {
      this.getAllCities(city.countryId);
      this.dialogData = city;
      this.notificationService.showSuccess('Successfully added', 'Success!');
    },
      (err: HttpErrorResponse) => {
        this.notificationService.showError('Error occurred. Details: ' + err.name + ' ' + err.message, 'Error');
      });
  }

  // UPDATE, PUT METHOD
  updateItem(city: CityEditModel): void {
    this.httpClient.put(this.myAppUrl + this.myApiUrl + city.id, city).subscribe(data => {
      this.getAllCities(city.countryId);
      this.dialogData = city;
      this.notificationService.showSuccess('Successfully edited', 'Success');
    },
      (err: HttpErrorResponse) => {
        this.notificationService.showError('Error occurred. Details: ' + err.name + ' ' + err.message, 'Error');
      }
    );
  }

  // DELETE METHOD
  deleteItem(id: string, countryId: string): void {
    this.httpClient.delete(this.myAppUrl + this.myApiUrl + id).subscribe(data => {
      this.getAllCities(countryId);
      this.notificationService.showSuccess('Successfully deleted', 'Success');
    },
      (err: HttpErrorResponse) => {
        this.notificationService.showError('Error occurred. Details: ' + err.name + ' ' + err.message, 'Error');
      }
    );
  }
}
