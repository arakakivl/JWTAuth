import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { Role } from '../models/role';
import { DecodedToken, UserModel } from '../models/userModel';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  constructor() { }

  /* Getting data */
  getToken() : string | undefined {
    let token = localStorage.getItem('token');
    return (token ? token : undefined);
  }

  getRefresh() : string | undefined {
    let refresh = localStorage.getItem('refresh');
    return(refresh ? refresh : undefined)
  }

  getUsername() : string | undefined {
    let user = this.decodeToken();
    if (user) {
      return user.username;
    }

    return undefined;
  }

  getRole() : Role | undefined {
    let user = this.decodeToken();
    if (user) {
      return user.role;
    }

    return undefined;
  }

  private decodeToken() : UserModel | undefined {
    let token = window.localStorage.getItem('token');
    if (token) {
      return jwtDecode<UserModel>(token);
    }

    return undefined;
  }

  private getExpirationDate() : Date | undefined {
    let token = this.getToken();
    if (token) {
      let decoded = jwtDecode<DecodedToken>(token);
      
      let date = new Date(0);
      date.setUTCSeconds(decoded.exp);

      return date;
    }

    return undefined;
  }

  /* About being valid and authenticated */
  isAuthenticated() : boolean {
    if (this.getToken() && this.getRefresh())
      return true;
    
    return false;
  }

  isExpired() : boolean {
    let strToken = window.localStorage.getItem('token');
    if (!strToken) {
      return true;
    } else {
      let date = this.getExpirationDate();
      if (date) {
        return !(date.valueOf() > new Date().valueOf());
      }
    }

    return true;
  }

  isAdm() : boolean {
    return this.getRole() == Role.Admin;
  }

  private readonly apiUrl : string = "http://localhost:5166/";
}

