import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Menu } from '../header/widgets/left-menu/left-menu-items';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})

export class CategoriesService {
  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private notificationService: NotificationService,private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/CategoriesApi/';
  }

  getCategoriesTreeMenu(countryId?: string): Observable<Menu[]> {
    return this.http.get<Menu[]>(this.myAppUrl + this.myApiUrl + countryId)
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
    this.notificationService.showError(errorMessage, "Getting categories error!");
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
