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
    let nameOfAdm = Role[Role.Admin];

    this.accountService.isValid().subscribe(x => {
      this.valid = x;
    });

    if (role?.toString().toLowerCase() == nameOfAdm.toLowerCase() && this.valid) {
      return true;
    } else {
      return false;
    }
  }

  valid? : boolean;
  
}
