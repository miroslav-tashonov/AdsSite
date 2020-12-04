import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/account/login/login.component';
import { ManageComponent } from './components/account/manage/manage.component';
import { RegisterComponent } from './components/account/register/register.component';
import { ResetPasswordComponent } from './components/account/reset-password/reset-password.component';
import { CategoriesMenuComponent } from './components/categories-menu/categories-menu.component';
import { ContactFormComponent } from './components/contact-form/contact-form.component';
import { HomeComponent } from './components/home/home.component';
import { LanguagePickerComponent } from './components/language-picker/language-picker.component';
import { LocalizationComponent } from './components/localization/localization.component';
import { WebSettingsComponent } from './components/web-settings/web-settings.component';
import { JwtInterceptor } from './interceptors/jwt-interceptor.service';
import { Role } from './models/RolesEnum';
import { AuthenticationService } from './services/authentication.service';
import { CategoriesService } from './services/categories.service';
import { CountryService } from './services/country.service';
import { LanguageService } from './services/language.service';
import { LocalizationService } from './services/localization.service';
import { NotificationService } from './services/notification.service';
import { WebSettingsService } from './services/web-settings.service';
import { AuthGuard } from './utilities/auth-guard.service';

describe('AppComponent', () => {
  const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'contact', component: ContactFormComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'manage', component: ManageComponent, canActivate: [AuthGuard], data: { roles: [Role.User, Role.Admin] } },
    { path: 'resetPassword', component: ResetPasswordComponent, canActivate: [AuthGuard], data: { roles: [Role.User, Role.Admin] } },
    //{
    //  path: 'userspanel', component: UsersPanelComponent, canActivate: [AuthGuard], data: { roles: [Role.User] }, children: [
    //    { path: 'myads', component: LoginComponent, outlet: 'sub' },
    //    { path: 'newad', component: LoginComponent, outlet: 'sub' },
    //    { path: 'mywishlist', component: LoginComponent, outlet: 'sub' },
    //    { path: 'verifications', component: LoginComponent, outlet: 'sub' },
    //  ]
    //},
    //{
    //  path: 'adminpanel', component: AdminPanelComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] }, children: [
    //    { path: 'manageusers', component: LoginComponent, outlet: 'subAdmin' },
    //    { path: 'categories', component: LoginComponent, outlet: 'subAdmin' },
    //    { path: 'cities', component: LoginComponent, outlet: 'subAdmin' },
    //    { path: 'languages', component: LoginComponent, outlet: 'subAdmin' },
    //    { path: 'localizations', component: LoginComponent, outlet: 'subAdmin' },
    //    { path: 'webSettings', component: LoginComponent, outlet: 'subAdmin' },
    //    { path: 'countries', component: LoginComponent, outlet: 'subAdmin' },
    //  ]
    //},
    { path: '**', redirectTo: '/' }];
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterModule.forRoot(routes),
        RouterTestingModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot()
      ],
      declarations: [
        AppComponent,
        LocalizationComponent,
        ContactFormComponent,
        WebSettingsComponent,
        CategoriesMenuComponent,
        LoginComponent,
        RegisterComponent,
        ManageComponent,
        LanguagePickerComponent,
        HomeComponent,
        ResetPasswordComponent
      ],
      providers: [
        { provide: LocationStrategy, useClass: PathLocationStrategy },
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        LocalizationService,
        WebSettingsService,
        CategoriesService,
        AuthenticationService,
        CountryService,
        LanguageService,
        NotificationService
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Ads Site'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Ads Site');
  });

});
