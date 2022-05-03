import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { Role } from '../models/role';
import { TokenService } from '../services/token.service';

@Injectable({
  providedIn: 'root'
})
export class AdmAuthGuard implements CanActivate {
  constructor(private tokenService : TokenService) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let role = this.tokenService.getRole();

    if (this.tokenService.getRole()?.toString() == role && this.tokenService.isAuthenticated()) {
      return true;
    } else {
      return false;
    }
  }
}
