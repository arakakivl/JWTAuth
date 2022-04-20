import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private accountService : AccountService) { }
  ngOnInit(): void {
    this.isAuthenticated = (this.accountService.getToken() != null)
    this.username = (this.accountService.getUsername());
  }

  isAuthenticated? : boolean;
  username? : string;

}
