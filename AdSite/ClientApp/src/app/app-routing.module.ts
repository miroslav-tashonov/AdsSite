import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { LoginComponent } from './login/login.component';
import { Role } from './models/RolesEnum';
import { AuthGuard } from './utilities/auth-guard.service';

//, canActivate: [AuthGuard], data: { roles: [Role.User] }

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'contact', component: ContactFormComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
