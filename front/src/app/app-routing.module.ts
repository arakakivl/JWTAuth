import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdmAuthGuard } from './guards/adm-auth.guard';
import { NotAuthenticatedGuard } from './guards/not-authenticated.guard';
import { AdminComponent } from './views/admin/admin.component';
import { HomeComponent } from './views/home/home.component';
import { SignInComponent } from './views/sign-in/sign-in.component';
import { SignUpComponent } from './views/sign-up/sign-up.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'signin', component: SignInComponent, canActivate: [NotAuthenticatedGuard] },
  { path: 'signup', component: SignUpComponent, canActivate: [NotAuthenticatedGuard] },
  { path: 'admin', component: AdminComponent, canActivate: [AdmAuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
