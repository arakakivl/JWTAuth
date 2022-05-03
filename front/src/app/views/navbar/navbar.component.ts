import { Component, OnInit } from '@angular/core';
import { Role } from 'src/app/models/role';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { AuthService } from 'src/app/services/auth.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(
    private tokenService : TokenService,
    private behavior : AccountBehaviorService,
    private authService : AuthService) { }

  ngOnInit(): void {
    this.behavior.isAuthenticated.subscribe(x => {
      this.isAuthenticated = x;

      let role = Role[Role.Admin]
      this.isAdm = x && (this.tokenService.getRole()?.toString() == role);
    });
  }

  logout() : void {
    this.authService.logout().subscribe({
      next: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('refresh');
      },

      error: () => {
        // do something on error!
      }
    });
    
    this.behavior.isAuthenticated.next(false);
    this.isAdm = false;
  }

  isAuthenticated? : boolean;
  isAdm? : boolean;
}
