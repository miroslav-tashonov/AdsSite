import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  submitted = false;
  loading = false;
  invalidLogin?: boolean;
  loginForm: FormGroup;

  error = '';
  myAppUrl: string;
  myApiUrl: string;

  constructor(private router: Router, private http: HttpClient, private formBuilder: FormBuilder, private authenticationService: AuthenticationService) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/AuthenticationApi/login';

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get f() { return this.loginForm.controls; }

  //todo countryId
  onSubmit() {
    const credentials = {
      'Email': this.f.username.value,
      'Password': this.f.password.value,
      'CountryId': "6248DE50-32E7-4C04-82A4-A7EF1D03CD05"
    }
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }
    
    this.loading = true;
    this.authenticationService.login(this.myAppUrl + this.myApiUrl, credentials.Email, credentials.Password, credentials.CountryId)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/']);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

}
