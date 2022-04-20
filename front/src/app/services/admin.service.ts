import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Role } from '../models/role';
import { UserModel } from '../models/userModel';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  constructor(private httpClient : HttpClient) { }

  getUsers() : Observable<UserModel[]> {
    return this.httpClient.get<UserModel[]>(this.apiUrl + "?role=" + Role.User);
  }

  getMods() : Observable<UserModel[]> {
    return this.httpClient.get<UserModel[]>(this.apiUrl + "?role=" + Role.Admin);
  }

  private readonly apiUrl : string = "https://localhost:7166/admin"
}