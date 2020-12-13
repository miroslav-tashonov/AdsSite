import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NotificationService } from './notification.service';
import { environment } from 'src/environments/environment';
import { CityModel } from '../classes/city';

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

  get data(): CityModel[] {
    return this.dataChange.value;
  }

  getDialogData() {
    return this.dialogData;
  }

  /** CRUD METHODS */
  getAllCities(): void {
    this.httpClient.get<CityModel[]>(this.myAppUrl + this.myApiUrl).subscribe(data => {
      this.dataChange.next(data);
    },
      (error: HttpErrorResponse) => {
        console.log(error.name + ' ' + error.message);
      });
  }

  // ADD, POST METHOD
  addItem(city: CityModel): void {
    this.httpClient.post(this.myAppUrl + this.myApiUrl, city).subscribe(data => {
      this.dialogData = city;
      this.notificationService.showSuccess('Successfully added', 'Success!');
    },
      (err: HttpErrorResponse) => {
        this.notificationService.showError('Error occurred. Details: ' + err.name + ' ' + err.message, 'Error');
      });
  }

  // UPDATE, PUT METHOD
  updateItem(city: CityModel): void {
    this.httpClient.put(this.myAppUrl + this.myApiUrl + city.id, city).subscribe(data => {
      this.dialogData = city;
      this.notificationService.showSuccess('Successfully edited', 'Success');
    },
      (err: HttpErrorResponse) => {
        this.notificationService.showError('Error occurred. Details: ' + err.name + ' ' + err.message, 'Error');
      }
    );
  }

  // DELETE METHOD
  deleteItem(id: number): void {
    this.httpClient.delete(this.myAppUrl + this.myApiUrl + id).subscribe(data => {
      this.notificationService.showSuccess('Successfully deleted', 'Success');
    },
      (err: HttpErrorResponse) => {
        this.notificationService.showError('Error occurred. Details: ' + err.name + ' ' + err.message, 'Error');
      }
    );
  }
}
