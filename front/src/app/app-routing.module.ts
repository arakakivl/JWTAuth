import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthAuthenticatedGuard } from './auth.guard';
import { AuthNotAuthenticatedGuard } from './auth.guard copy';
import { AdminComponent } from './views/admin/admin.component';
import { HomeComponent } from './views/home/home.component';
import { SignInComponent } from './views/sign-in/sign-in.component';
import { SignUpComponent } from './views/sign-up/sign-up.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'signin', component: SignInComponent, canActivate: [AuthNotAuthenticatedGuard] },
  { path: 'signup', component: SignUpComponent, canActivate: [AuthNotAuthenticatedGuard] },
  { path: 'admin', component: AdminComponent, canActivate: [AuthAuthenticatedGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
