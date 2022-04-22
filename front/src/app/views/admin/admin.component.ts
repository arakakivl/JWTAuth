import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Role } from 'src/app/models/role';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { AccountService } from 'src/app/services/account.service';
import { AdminService } from 'src/app/services/admin.service';
import { UserModel } from '../../models/userModel';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  constructor(
    private adminService : AdminService,
    private beahviorService : AccountBehaviorService,
    private accountService : AccountService,
    private router : Router) { }

  ngOnInit(): void {
    this.beahviorService.isAuthenticated.subscribe(x => {
      let role = Role[Role.Admin];
      this.isAdm = x && this.accountService.getRole()?.toString() == role;
    });

    if(this.isAdm)
    {
      this.getUsers();
      this.getMods();
    }
    else
    {
      this.router.navigate(['']);
    }
  }

  getUsers() : void {
    this.adminService.getUsers().subscribe(x => {
      console.log("USERS:");
      console.log(x);
      this.users = x;
    }, err => {
      alert("ERROR: " + err.error);
    });
  }

  getMods() : void {
    this.adminService.getMods().subscribe(x => {
      console.log("MODS:");
      console.log(x);
      this.mods = x;
    }, err => {
      alert("ERROR: " + err.error);
    });
  }

  users? : UserModel[];
  mods? : UserModel[];
  isAdm? : boolean;
}
