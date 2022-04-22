import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { Observable } from 'rxjs';
import { Role } from '../models/role';
import { UserModel } from '../models/userModel';

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

  isAuthenticated() : boolean {
    let token = window.localStorage.getItem('token');
    if (token) // E DATA DE EXPIRAÇÃO OK
      return true;
    
    return false;
  }

  decodeToken() : UserModel | undefined {
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

  private readonly apiUrl : string = "https://localhost:7166/";
}

