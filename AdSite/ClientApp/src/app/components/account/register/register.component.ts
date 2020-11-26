import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../../services/authentication.service';
import { RegisterUser } from '../../../models/User';
import { CountryService } from '../../../services/country.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  submitted = false;
  loading = false;
  invalidLogin?: boolean;
  loginForm: FormGroup;
  registerUser: RegisterUser;

  error = '';

  constructor(private router: Router, private http: HttpClient, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      phone: ['', Validators.required],
    });

    this.registerUser = new RegisterUser();
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
    this.authenticationService.register(this.registerUser)
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
