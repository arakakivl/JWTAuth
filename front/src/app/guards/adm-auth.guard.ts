import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { Role } from '../models/role';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdmAuthGuard implements CanActivate {
  constructor(private accountService : AccountService) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let role = this.accountService.getRole();

    if (this.accountService.getRole()?.toString() == role && this.accountService.isAuthenticated()) {
      return true;
    } else {
      return false;
    }
  }
}
