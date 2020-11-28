import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ResetPasswordModel, User } from '../../../models/User';
import { AuthenticationService } from '../../../services/authentication.service';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent implements OnInit {
  submitted = false;
  loading = false;
  invalidLogin?: boolean;
  loginForm: FormGroup;
  currentUser?: User;
  resetPasswordModel: ResetPasswordModel;
  errors: string[];

  constructor(private notificationService: NotificationService,private router: Router, private http: HttpClient, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {

    this.loginForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required, Validators.minLength(6)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });

    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);

    this.resetPasswordModel = new ResetPasswordModel();
    this.errors = [];
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.resetPasswordModel.email = this.currentUser?.email;
    this.resetPasswordModel.oldPassword = this.f.oldPassword.value;
    this.resetPasswordModel.password = this.f.password.value;
    this.resetPasswordModel.confirmPassword = this.f.confirmPassword.value;
    this.resetPasswordModel.countryId = this.countryService.countryId;

    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.resetPassword(this.resetPasswordModel)
      .pipe(first())
      .subscribe(
        data => {
          this.notificationService.showSuccess('Reset password succesfully!', 'Reset password action');
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

          this.notificationService.showError('Reset password has some errors!', 'Reset password action');
          this.loading = false;
        });
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required, Validators.minLength(6)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }
}
