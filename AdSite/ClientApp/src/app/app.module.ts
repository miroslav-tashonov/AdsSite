import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LocalizationService } from './services/localization.service';
import { WebSettingsService } from './services/web-settings.service';
import { CategoriesService } from './services/categories.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthenticationService } from './services/authentication.service';
import { JwtInterceptor } from './interceptors/jwt-interceptor.service';
import { LocalizationComponent } from './components/localization/localization.component';
import { ContactFormComponent } from './components/contact-form/contact-form.component';
import { WebSettingsComponent } from './components/web-settings/web-settings.component';
import { CategoriesMenuComponent } from './components/categories-menu/categories-menu.component';
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { ManageComponent } from './components/account/manage/manage.component';
import { CountryService } from './services/country.service';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LocalizationComponent,
    ContactFormComponent,
    WebSettingsComponent,
    CategoriesMenuComponent,
    LoginComponent,
    RegisterComponent,
    ManageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: LocationStrategy, useClass: PathLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    LocalizationService,
    WebSettingsService,
    CategoriesService,
    AuthenticationService,
    CountryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
