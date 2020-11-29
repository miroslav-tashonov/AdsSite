import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../../services/authentication.service';
import { RegisterUser } from '../../../models/User';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';

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
  errors: string[];

  constructor(private notificationService: NotificationService,private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
      phone: ['', [Validators.required, Validators.pattern("^[0-9]*$"),
        Validators.minLength(7)]],
    });

    this.errors = [];
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
          this.notificationService.showSuccess('Register account is succesful!', 'Register action');
          this.router.navigate(['/']);
        },
        thrownError => {
          this.errors = [];
          if (thrownError.error.errors) {
            for (const [key, value] of Object.entries(thrownError.error.errors)) {
              this.errors.push(thrownError.error.errors[key]);
            }
          }
          else {
            this.errors.push(thrownError.error);
          }

          this.notificationService.showError('Register account failed!', 'Register action');
          this.loading = false;
        });
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
      phone: ['', [Validators.required, Validators.pattern("^[0-9]*$"),
        Validators.minLength(7)]],
    });
  }
}
