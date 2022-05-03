import { Component, OnInit } from '@angular/core';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(
    private tokenService : TokenService,
    private behavior : AccountBehaviorService) { }
  ngOnInit(): void {
    this.behavior.isAuthenticated.subscribe(x => {
      this.isAuthenticated = x;
    });

    this.username = (this.tokenService.getUsername());
  }

  isAuthenticated? : boolean;
  username? : string;

}
