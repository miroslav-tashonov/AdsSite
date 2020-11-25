import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LocalizationComponent } from './localization/localization.component';
import { LocalizationService } from './services/localization.service';
import { WebSettingsService } from './services/web-settings.service';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { WebSettingsComponent } from './web-settings/web-settings.component';
import { CategoriesMenuComponent } from './categories-menu/categories-menu.component';
import { CategoriesService } from './services/categories.service';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthenticationService } from './services/authentication.service';
import { JwtInterceptor } from './interceptors/jwt-interceptor.service';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    LocalizationComponent,
    ContactFormComponent,
    WebSettingsComponent,
    CategoriesMenuComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    LocalizationService,
    WebSettingsService,
    CategoriesService,
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
