import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../services/authentication.service';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { RegisterUser, User } from '../../../models/User';
import { CountryService } from '../../../services/country.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html'
})
export class ManageComponent implements OnInit {
  submitted = false;
  loading = false;
  invalidLogin?: boolean;
  loginForm: FormGroup;
  registerUser: RegisterUser;
  currentUser?: User;

  error = '';

  constructor(private router: Router, private http: HttpClient, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      phone: ['', Validators.required],
    });

    this.registerUser = new RegisterUser();
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.registerUser.email = this.f.email.value;
    this.registerUser.password = this.f.password.value;
    this.registerUser.confirmPassword = this.f.confirmPassword.value;
    this.registerUser.phone = this.f.phone.value;
    this.registerUser.countryId = this.countryService.countryId;


    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.update(this.registerUser)
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
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      phone: ['', Validators.required],
    });

  }
}
