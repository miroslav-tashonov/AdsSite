import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';
import { ErrorStateMatcher } from '@angular/material/core';
import { MyErrorStateMatcher } from '../../../models/ErrorStateMatcher';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(7)
  ]);

  matcher = new MyErrorStateMatcher();
  hide = true;
  errors: string[];

  constructor(private notificationService: NotificationService, private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.errors = [];
  }

  onSubmit() {
    const credentials = {
      'Email': this.emailFormControl.value,
      'Password': this.passwordFormControl.value,
      'CountryId': this.countryService.countryId
    }
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
        });
  }

  ngOnInit(): void {
  }

}
