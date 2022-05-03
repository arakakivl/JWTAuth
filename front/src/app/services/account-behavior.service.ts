import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TokenService } from './token.service';

@Injectable({
    providedIn: 'root'
})
export class AccountBehaviorService {
    constructor(private tokenService : TokenService) {}
    public isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.tokenService.isAuthenticated());
}