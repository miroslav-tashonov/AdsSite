import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  submitted = false;
  loading = false;
  invalidLogin?: boolean;
  loginForm: FormGroup;

  errors: string[];

  constructor(private notificationService: NotificationService, private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.errors = [];
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    const credentials = {
      'Email': this.f.username.value,
      'Password': this.f.password.value,
      'CountryId': this.countryService.countryId
    }
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }
    
    this.loading = true;
    this.authenticationService.login(credentials.Email, credentials.Password, credentials.CountryId)
      .pipe(first())
      .subscribe(
        data => {
          this.notificationService.showSuccess('Login succesful!','Login action');
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

          this.notificationService.showError('Login failed!', 'Login action');
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
