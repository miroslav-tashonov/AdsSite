import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from "@angular/router";
import { TranslateModule } from '@ngx-translate/core';
// Services
import { WINDOW_PROVIDERS } from "./services/windows.service";
import { LandingFixService } from '../shared/services/landing-fix.service';
import { InstagramService } from "./services/instagram.service";
import { ProductsService } from "./services/products.service";
import { WishlistService } from "./services/wishlist.service";
import { CartService } from "./services/cart.service";
import { OrderService } from "./services/order.service";
import { AuthenticationService } from './services/authentication.service';
import { CountryService } from './services/country.service';
import { NotificationService } from './services/notification.service';
import { PaginationService } from "./classes/paginate";
import { CategoriesService } from './services/categories.service';
import { CityService } from './services/city.service';
// Pipes
import { OrderByPipe } from './pipes/order-by.pipe';
// components
import { HeaderOneComponent } from './header/header-one/header-one.component';
import { LeftSidebarComponent } from './header/left-sidebar/left-sidebar.component';
import { TopbarOneComponent } from './header/widgets/topbar/topbar-one/topbar-one.component';
import { TopbarTwoComponent } from './header/widgets/topbar/topbar-two/topbar-two.component';
import { NavbarComponent } from './header/widgets/navbar/navbar.component';
import { SettingsComponent } from './header/widgets/settings/settings.component';
import { LeftMenuComponent } from './header/widgets/left-menu/left-menu.component';
import { FooterOneComponent } from './footer/footer-one/footer-one.component';
import { InformationComponent } from './footer/widgets/information/information.component';
import { CategoriesComponent } from './footer/widgets/categories/categories.component';
import { WhyWeChooseComponent } from './footer/widgets/why-we-choose/why-we-choose.component';
import { CopyrightComponent } from './footer/widgets/copyright/copyright.component';
import { SocialComponent } from './footer/widgets/social/social.component';


@NgModule({
  exports: [
    CommonModule,
    TranslateModule,
    HeaderOneComponent,
    LeftSidebarComponent,
    FooterOneComponent,
    OrderByPipe
  ],
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule
  ],
  declarations: [
    HeaderOneComponent,
    LeftSidebarComponent,
    FooterOneComponent,
    OrderByPipe,
    NavbarComponent,
    SettingsComponent,
    LeftMenuComponent,
    TopbarOneComponent,
    TopbarTwoComponent,
    InformationComponent,
    CategoriesComponent,
    WhyWeChooseComponent,
    CopyrightComponent,
    SocialComponent
  ],
  providers: [
    WINDOW_PROVIDERS,
    LandingFixService,
    InstagramService,
    ProductsService,
    WishlistService,
    CartService,
    OrderService,
    PaginationService,
    AuthenticationService,
    CountryService,
    NotificationService,
    CategoriesService,
    CityService
  ]
})
export class SharedModule { }
