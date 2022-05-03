import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from '../services/token.service';

@Injectable({
  providedIn: 'root'
})
export class NotAuthenticatedGuard implements CanActivate {
  constructor(private tokenService : TokenService) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : boolean {
    if (!this.tokenService.isAuthenticated()) {
      return true;
    }
    
    return false;
  }
  
}
