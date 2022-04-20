import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
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
    
    if (role?.toString().toLowerCase() == nameOfAdm.toLowerCase()) {
      return true;
    }
    
    return false;
  }
  
}
