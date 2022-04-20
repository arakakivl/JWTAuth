import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(private accountService : AccountService) { }

  ngOnInit(): void {
    this.isAuthorized = (this.accountService.getToken() != undefined);
  }

  isAuthorized? : boolean;

}
