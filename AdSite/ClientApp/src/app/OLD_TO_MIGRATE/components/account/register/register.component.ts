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
import { MyErrorStateMatcher } from '../../../models/ErrorStateMatcher';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  addressForm = this.fb.group({
    userEmail: [null, Validators.compose([
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


  hasUnitNumber = false;


  states = [
    { name: 'Alabama', abbreviation: 'AL' },
    { name: 'Alaska', abbreviation: 'AK' },
    { name: 'American Samoa', abbreviation: 'AS' },

  ];

  registerUser: RegisterUser;
  errors: string[];
  hide = true;
  hideOld = true;
  matcher = new MyErrorStateMatcher();

  constructor(private fb: FormBuilder, private notificationService: NotificationService,private router: Router, private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.errors = [];
    this.registerUser = new RegisterUser();
  }
  onSubmit() {
    //this.registerUser.email = this.fb.firstName.value;
    //this.registerUser.password = this.f.password.value;
    //this.registerUser.confirmPassword = this.f.confirmPassword.value;
    //this.registerUser.phone = this.f.phone.value;
    //this.registerUser.countryId = this.countryService.countryId;


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
        });
  }

  //todo 
  onFileComplete(data: any) {
    
  }

  ngOnInit(): void {
  }
}
