import { Component, OnInit } from '@angular/core';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(
    private accountService : AccountService,
    private behavior : AccountBehaviorService) { }
  ngOnInit(): void {
    this.behavior.isAuthenticated.subscribe(x => {
      this.isAuthenticated = x;
    });

    this.username = (this.accountService.getUsername());
  }

  isAuthenticated? : boolean;
  username? : string;

}
