import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { MyErrorStateMatcher } from '../../../models/ErrorStateMatcher';
import { ResetPasswordModel, User } from '../../../models/User';
import { AuthenticationService } from '../../../services/authentication.service';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  oldPasswordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(7),
  ]);
  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(7)
  ]);
  newPasswordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(7)
  ]);

  matcher = new MyErrorStateMatcher();
  hideNew = true;
  hideOld = true;
  hide = true;

  currentUser?: User;
  resetPasswordModel: ResetPasswordModel;
  errors: string[];

  constructor(private notificationService: NotificationService,private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {

    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);

    this.resetPasswordModel = new ResetPasswordModel();
    this.errors = [];
  }
  onSubmit() {
    this.resetPasswordModel.email = this.currentUser?.email;
    this.resetPasswordModel.oldPassword = this.oldPasswordFormControl.value;
    this.resetPasswordModel.password = this.passwordFormControl.value;
    this.resetPasswordModel.confirmPassword = this.newPasswordFormControl.value;
    this.resetPasswordModel.countryId = this.countryService.countryId;

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
        });
  }

  ngOnInit(): void {
  }
}
