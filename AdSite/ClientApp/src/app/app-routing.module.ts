import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/account/login/login.component';
import { ManageComponent } from './components/account/manage/manage.component';
import { RegisterComponent } from './components/account/register/register.component';
import { ContactFormComponent } from './components/contact-form/contact-form.component';
import { Role } from './models/RolesEnum';
import { AuthGuard } from './utilities/auth-guard.service';

//, canActivate: [AuthGuard], data: { roles: [Role.User] }

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'contact', component: ContactFormComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'manage', component: ManageComponent, canActivate: [AuthGuard], data: { roles: [Role.User, Role.Admin] } },
  { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
