import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LocalizationComponent } from './localization/localization.component';
import { LocalizationService } from './services/localization.service';
import { WebSettingsService } from './services/web-settings.service';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { WebSettingsComponent } from './web-settings/web-settings.component';
import { CategoriesMenuComponent } from './categories-menu/categories-menu.component';
import { CategoriesService } from './services/categories.service';

@NgModule({
  declarations: [
    AppComponent,
    LocalizationComponent,
    ContactFormComponent,
    WebSettingsComponent,
    CategoriesMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    LocalizationService,
    WebSettingsService,
    CategoriesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
