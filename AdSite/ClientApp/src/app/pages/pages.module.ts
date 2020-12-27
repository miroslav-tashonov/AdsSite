import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages-routing.module';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { IsotopeModule } from 'ngx-isotope';

import { AboutUsComponent } from './about-us/about-us.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { LookbookComponent } from './lookbook/lookbook.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SearchComponent } from './search/search.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ContactComponent } from './contact/contact.component';
import { CompareComponent } from './compare/compare.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardNewAdComponent } from './dashboard-new-ad/dashboard-new-ad.component';
import { DashboardMyAdsComponent } from './dashboard-my-ads/dashboard-my-ads.component';
import { DashboardWishlistComponent } from './dashboard-wishlist/dashboard-wishlist.component';
import { DashboardWebSettingsComponent } from './dashboard-web-settings/dashboard-web-settings.component';
import { DashboardCitiesComponent } from './dashboard-cities/dashboard-cities.component';
import { DashboardCategoriesComponent } from './dashboard-categories/dashboard-categories.component';
import { DashboardReportedAdsComponent } from './dashboard-reported-ads/dashboard-reported-ads.component';
import { DashboardManageUsersComponent } from './dashboard-manage-users/dashboard-manage-users.component';
import { DashboardCitiesDialogComponent } from './dashboard-cities/dashboard-cities-dialog/dashboard-cities-dialog.component';
import { DashboardCitiesAddDialogComponent } from './dashboard-cities/dashboard-cities-add-dialog/dashboard-cities-add-dialog.component';
import { FaqComponent } from './faq/faq.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    PagesRoutingModule,
    SlickCarouselModule,
    IsotopeModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    AboutUsComponent,
    ErrorPageComponent,
    LookbookComponent,
    LoginComponent,
    RegisterComponent,
    SearchComponent,
    WishlistComponent,
    ForgetPasswordComponent,
    ContactComponent,
    CompareComponent,
    DashboardComponent,
    DashboardNewAdComponent,
    DashboardMyAdsComponent,
    DashboardWishlistComponent,
    DashboardWebSettingsComponent,
    DashboardCitiesComponent,
    DashboardCategoriesComponent,
    DashboardManageUsersComponent,
    DashboardReportedAdsComponent,
    DashboardCitiesDialogComponent,
    DashboardCitiesAddDialogComponent,
    FaqComponent,
  ]
})
export class PagesModule { }
