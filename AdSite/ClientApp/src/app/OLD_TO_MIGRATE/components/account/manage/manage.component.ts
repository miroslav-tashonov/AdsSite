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
import { MyErrorStateMatcher } from '../../../models/ErrorStateMatcher';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent implements OnInit {

  addressForm!: any;
  hasUnitNumber = false;

  states = [
    { name: 'Alabama', abbreviation: 'AL' },
    { name: 'Alaska', abbreviation: 'AK' },
    { name: 'American Samoa', abbreviation: 'AS' },
  ];

  registerUser!: RegisterUser;
  errors: string[];
  hide = true;
  hideOld = true;
  model: ManageUser;
  currentUser?: User;
  matcher = new MyErrorStateMatcher();

  constructor(private fb: FormBuilder, private notificationService: NotificationService, private router: Router, private http: HttpClient, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.errors = [];
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    this.model = new ManageUser();
    this.model.email = this.currentUser?.email;
    this.model.phone = this.currentUser?.phoneNumber;
  }

  onSubmit() {
    //this.model.email = this.f.email.value;
    //this.model.phone = this.f.phone.value;
    this.model.countryId = this.countryService.countryId;


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
        });
  }

  ngOnInit(): void {
    this.addressForm = this.fb.group({
      userEmail: [this.model.email, Validators.compose([
        Validators.required, Validators.email])
      ],
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      address: [null, Validators.required],
      address2: null,
      city: [null, Validators.required],
      state: [null, Validators.required],
      telephone: [null, Validators.compose([
        Validators.required, Validators.minLength(8)])
      ],
      password: [null, Validators.compose([
        Validators.required, Validators.minLength(7)])
      ],
      repeatPassword: [null, Validators.compose([
        Validators.required, Validators.minLength(7)])
      ],
      gender: ['male', Validators.required]
    });
  }

  //todo 
  onFileComplete(data: any) {

  }
}
