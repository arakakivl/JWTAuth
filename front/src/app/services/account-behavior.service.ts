import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
    providedIn: 'root'
})
export class AccountBehaviorService {
    constructor(private accountService : AccountService) {}
    public isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.accountService.isAuthenticated());
}