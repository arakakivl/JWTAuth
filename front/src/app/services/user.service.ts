import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
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

  private readonly apiUrl : string = "https://localhost:7166/";
}
