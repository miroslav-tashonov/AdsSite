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
import { LanguageService } from './services/language.service';
import { LanguagePickerComponent } from './components/language-picker/language-picker.component';
import { HomeComponent } from './components/home/home.component';
import { ResetPasswordComponent } from './components/account/reset-password/reset-password.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NotificationService } from './services/notification.service';
import { CitiesAllComponent } from './components/cities-all/cities-all.component';
import { CityComponent } from './components/city/city.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AddDialogComponent } from './components/cities-all/dialogs/add/add.dialog.component';
import { EditDialogComponent } from './components/cities-all/dialogs/edit/edit.dialog.component';
import { DeleteDialogComponent } from './components/cities-all/dialogs/delete/delete.dialog.component';
import { CityService } from './services/city.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { NgMatSearchBarModule } from 'ng-mat-search-bar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FileUploadComponent } from './components/file-upload/file-upload.component';

@NgModule({
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
    ResetPasswordComponent,
    CitiesAllComponent,
    CityComponent,
    AddDialogComponent,
    EditDialogComponent,
    DeleteDialogComponent,
    FileUploadComponent
  ],
  imports: [
    BrowserAnimationsModule,
    FlexLayoutModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    NgMatSearchBarModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatSidenavModule,
    MatListModule,
    MatMenuModule,
    MatFormFieldModule,
    MatCardModule,
    MatNativeDateModule,
    MatSelectModule,
    MatRadioModule,
    MatGridListModule,
    MatProgressBarModule
  ],
  entryComponents: [
    AddDialogComponent,
    EditDialogComponent,
    DeleteDialogComponent
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
    NotificationService,
    CityService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
