import { Component, OnInit } from '@angular/core';
import { Role } from 'src/app/models/role';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(
    private accountService : AccountService,
    private behavior : AccountBehaviorService) { }

  ngOnInit(): void {
    this.behavior.isAuthenticated.subscribe(x => {
      this.isAuthenticated = x;

      let role = Role[Role.Admin]
      this.isAdm = x && (this.accountService.getRole()?.toString() == role);
    });
  }

  logout() : void {
    this.accountService.logout().subscribe();
    this.behavior.isAuthenticated.next(false);
    this.isAdm = false;
  }

  isAuthenticated? : boolean;
  isAdm? : boolean;

}
