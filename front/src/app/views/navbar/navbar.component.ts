import { Component, OnInit } from '@angular/core';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(private tokenService : TokenService) { }

  ngOnInit(): void {
    this.isAuthorized = (this.tokenService.getToken() != undefined);
  }

  isAuthorized? : boolean;

}
