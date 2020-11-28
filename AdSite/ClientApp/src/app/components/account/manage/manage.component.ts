import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../services/authentication.service';
import { environment } from 'src/environments/environment';
import { first } from 'rxjs/operators';
import { ManageUser, RegisterUser, User } from '../../../models/User';
import { CountryService } from '../../../services/country.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html'
})
export class ManageComponent implements OnInit {
  submitted = false;
  loading = false;
  invalidLogin?: boolean;
  loginForm: FormGroup;
  model: ManageUser;
  currentUser?: User;
  errors: string[];

  constructor(private notificationService: NotificationService, private router: Router, private http: HttpClient, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern("^[0-9]*$"),
      Validators.minLength(7)]],
    });

    this.errors = [];
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    this.model = new ManageUser();
    this.model.email = this.currentUser?.email;
    this.model.phone = this.currentUser?.phoneNumber;
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.model.email = this.f.email.value;
    this.model.phone = this.f.phone.value;
    this.model.countryId = this.countryService.countryId;


    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.update(this.model)
      .pipe(first())
      .subscribe(
        data => {
          this.notificationService.showSuccess('Update account is succesful!', 'Save action');
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

          this.notificationService.showError('Update account failed!', 'Save action');
          this.loading = false;
        });
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: [this.model.email, [Validators.required, Validators.email]],
      phone: [this.model.phone, [Validators.required, Validators.pattern("^[0-9]*$"),
      Validators.minLength(7)]],
    });

  }
}
