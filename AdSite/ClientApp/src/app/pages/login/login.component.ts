import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { NotificationService } from '../../shared/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
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

  loginForm: any;

  //matcher = new MyErrorStateMatcher();
  hide = true;
  errors: string[];

  constructor(private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router,
    private formBuilder: FormBuilder) {
    this.loginForm = this.formBuilder.group({
      name: '',
      address: ''
    });
  }

  ngOnInit() {
  }

  onSubmit() {
    const credentials = {
      'Email': this.emailFormControl.value,
      'Password': this.passwordFormControl.value,
      'CountryId': '99DE8181-09A8-41DB-895E-54E5E0650C3A'
    }
    this.authenticationService.login(credentials.Email, credentials.Password, credentials.CountryId)
      .pipe(first())
      .subscribe(
        data => {
          this.notificationService.showSuccess('Login succesful!', 'Login action');
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

}
