import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

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
import { FaqComponent } from './faq/faq.component';
import { DashboardNewAdComponent } from './dashboard-new-ad/dashboard-new-ad.component';
import { DashboardMyAdsComponent } from './dashboard-my-ads/dashboard-my-ads.component';
import { DashboardWishlistComponent } from './dashboard-wishlist/dashboard-wishlist.component';
import { DashboardWebSettingsComponent } from './dashboard-web-settings/dashboard-web-settings.component';
import { DashboardCitiesComponent } from './dashboard-cities/dashboard-cities.component';
import { DashboardCategoriesComponent } from './dashboard-categories/dashboard-categories.component';
import { DashboardManageUsersComponent } from './dashboard-manage-users/dashboard-manage-users.component';
import { DashboardReportedAdsComponent } from './dashboard-reported-ads/dashboard-reported-ads.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'about-us',
        component: AboutUsComponent
      },
      {
        path: '404',
        component: ErrorPageComponent
      },
      {
        path: 'lookbook',
        component: LookbookComponent
      },
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'register',
        component: RegisterComponent
      },
      {
        path: 'search',
        component: SearchComponent
      },
      {
        path: 'wishlist',
        component: WishlistComponent
      },
      {
        path: 'forgetpassword',
        component: ForgetPasswordComponent
      },
      {
        path: 'contact',
        component: ContactComponent
      },
      {
        path: 'compare',
        component: CompareComponent
      },
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'dashboard-new-ad',
        component: DashboardNewAdComponent
      },
      {
        path: 'dashboard-my-ads',
        component: DashboardMyAdsComponent
      },
      {
        path: 'dashboard-wishlist',
        component: DashboardWishlistComponent
      },
      {
        path: 'dashboard-web-settings',
        component: DashboardWebSettingsComponent
      },
      {
        path: 'dashboard-cities',
        component: DashboardCitiesComponent
      },
      {
        path: 'dashboard-categories',
        component: DashboardCategoriesComponent
      },
      {
        path: 'dashboard-manage-users',
        component: DashboardManageUsersComponent
      },
      {
        path: 'dashboard-reported-ads',
        component: DashboardReportedAdsComponent
      },
      {
        path: 'faq',
        component: FaqComponent
      }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
