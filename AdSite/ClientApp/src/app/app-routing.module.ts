import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/account/login/login.component';
import { ManageComponent } from './components/account/manage/manage.component';
import { RegisterComponent } from './components/account/register/register.component';
import { ResetPasswordComponent } from './components/account/reset-password/reset-password.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { CitiesAllComponent } from './components/cities-all/cities-all.component';
import { ContactFormComponent } from './components/contact-form/contact-form.component';
import { HomeComponent } from './components/home/home.component';
import { UsersPanelComponent } from './components/users-panel/users-panel.component';
import { Role } from './models/RolesEnum';
import { AuthGuard } from './utilities/auth-guard.service';

//, canActivate: [AuthGuard], data: { roles: [Role.User] }

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contact', component: ContactFormComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'manage', component: ManageComponent, canActivate: [AuthGuard], data: { roles: [Role.User, Role.Admin] } },
  { path: 'resetPassword', component: ResetPasswordComponent, canActivate: [AuthGuard], data: { roles: [Role.User, Role.Admin] } },
  {
    path: 'userspanel', component: UsersPanelComponent, canActivate: [AuthGuard], data: { roles: [Role.User] }, children: [
      { path: 'myads', component: LoginComponent, outlet: 'sub' },
      { path: 'newad', component: LoginComponent, outlet: 'sub' },
      { path: 'mywishlist', component: LoginComponent, outlet: 'sub' },
      { path: 'verifications', component: LoginComponent, outlet: 'sub' },
    ] },
  {
    path: 'adminpanel', component: AdminPanelComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] }, children: [
      { path: 'manageusers', component: LoginComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'categories', component: LoginComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'cities', component: CitiesAllComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'languages', component: LoginComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'localizations', component: LoginComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'webSettings', component: LoginComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'countries', component: LoginComponent, outlet: 'subAdmin', canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
    ] },
  { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
