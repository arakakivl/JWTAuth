import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { TokenService } from './token.service';
import { handleError } from '../handleError/handleError';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private httpClient : HttpClient,
    private tokenService : TokenService) { }

  register(user : any) : Observable<any> {
    return this.httpClient.post(this.apiUrl + "register", user, this.httpOptions).pipe(catchError(handleError));
  }

  login(user : any) : Observable<any> {
    return this.httpClient.post<any>(this.apiUrl + "login", user, this.httpOptions).pipe(catchError(handleError));
  }

  refresh() : Observable<any> {
    return this.httpClient.post<any>(this.apiUrl + "refresh", { oldAccessToken: this.tokenService.getToken(), oldRefreshToken: this.tokenService.getRefresh() }, this.httpOptions).pipe(catchError(handleError));
  }

  logout() : Observable<any> {    
    return this.httpClient.post<any>(this.apiUrl + "logout", {  });
  }

  addTokens(token : string, refresh : string) {
    localStorage.clear();
    
    localStorage.setItem('token', token);
    localStorage.setItem('refresh', refresh);
  }

  private readonly apiUrl : string = "http://localhost:5166/";

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
}
