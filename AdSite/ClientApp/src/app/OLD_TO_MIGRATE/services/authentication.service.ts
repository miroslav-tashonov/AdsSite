import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { RegisterUser, ResetPasswordModel, User } from '../models/User';
import { environment } from 'src/environments/environment';
import { LocationStrategy } from '@angular/common';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  appUrl: string;
  updateApiUrl: string;
  registerApiUrl: string;
  loginApiUrl: string;
  resetPasswordUrl: string;

  constructor(private http: HttpClient, private locationStrategy: LocationStrategy) {
    var userJson = localStorage.getItem('currentUser-' + this.locationStrategy.getBaseHref());
    this.currentUserSubject = userJson !== null ? new BehaviorSubject<User>(JSON.parse(userJson)) : new BehaviorSubject<User>(new User());
    this.currentUser = this.currentUserSubject.asObservable();

    this.appUrl = environment.appUrl;
    this.updateApiUrl = 'api/AuthenticationApi/update';
    this.registerApiUrl = 'api/AuthenticationApi/register';
    this.loginApiUrl = 'api/AuthenticationApi/login';
    this.resetPasswordUrl = 'api/AuthenticationApi/resetPassword';
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string, countryId?: string) {
    return this.http.post<any>(this.appUrl + this.loginApiUrl, { email, password, countryId })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser-' + this.locationStrategy.getBaseHref(), JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      }));
  }

  register(user: RegisterUser) {
    return this.http.post<any>(this.appUrl + this.registerApiUrl, user)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser-' + this.locationStrategy.getBaseHref(), JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      }));
  }

  update(user: RegisterUser) {
    return this.http.post<any>(this.appUrl + this.updateApiUrl, user)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser-' + this.locationStrategy.getBaseHref(), JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      }));
  }

  resetPassword(user: ResetPasswordModel) {
    return this.http.post<any>(this.appUrl + this.resetPasswordUrl, user)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser-' + this.locationStrategy.getBaseHref(), JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser-' + this.locationStrategy.getBaseHref());
    this.currentUserSubject.next(new User());
  }
}
