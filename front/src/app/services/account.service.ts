import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { Observable } from 'rxjs';
import { Role } from '../models/role';
import { DecodedToken, UserModel } from '../models/userModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private httpClient : HttpClient) { }

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  /* Login, Register and Logout */
  login(user : any) : Observable<any> {
    return this.httpClient.post<any>(this.apiUrl + "login", user, this.httpOptions);
  }

  register(user : any) : Observable<any> {
    return this.httpClient.post(this.apiUrl + "register", user, this.httpOptions);
  }

  logout() : void {    
    let response = this.httpClient.post(this.apiUrl + "logout", {}).subscribe();
    window.localStorage.removeItem('token');
  }

  /* About being valid and authenticated */
  isAuthenticated() : boolean {
    let token = window.localStorage.getItem('token');
    if (token && !this.isExpired())
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

  isValid() : Observable<boolean> {
    return this.httpClient.post<boolean>(this.apiUrl, {});
  }

  private getExpirationDate() : Date | undefined {
    let strToken = window.localStorage.getItem('token');
    if (strToken) {
      let decoded = jwtDecode<DecodedToken>(strToken);
      
      let date = new Date(0);
      date.setUTCSeconds(decoded.exp);

      return date;
    }

    return undefined;
  }

  /* About getting data from the Token */
  private decodeToken() : UserModel | undefined {
    let token = window.localStorage.getItem('token');
    if (token) {
      return jwtDecode<UserModel>(token);
    }

    return undefined;
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

  private readonly apiUrl : string = "http://localhost:5166/";
}

