import { Component, OnInit } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatTabChangeEvent } from '@angular/material/tabs';
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

    if(!this.isAdm)
    {
      this.router.navigate(['']);
      return;
    }
    
    this.getUsers();
    this.selectedTab = 0;
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

  changeRole(toggleChanged : MatSlideToggleChange, username : string, role : Role) : void {
    if (role == Role.Admin) {
      alert("You cannot change the role of an ADM!");
      toggleChanged.source.checked = !toggleChanged.checked;
      return;
    }

    if (this.isAdm) {
      this.adminService.changeRole(username, 1).subscribe();
      
      this.clearUsers();
      this.getUsers();
    }
  }

  deleteUser(username : string) : void {
    if (this.isAdm) {
      this.adminService.deleteUser(username);
    }
  }

  findUsers() : void {
    let input : HTMLInputElement = document.getElementById('findUserInput') as HTMLInputElement;
    this.clearUsers();
    
    switch(this.selectedTab) {
      case 0:
        this.adminService.searchUser(input.value, Role.User).subscribe(x => {
          this.users = x;
        });
        break;
      case 1:
        this.adminService.searchUser(input.value, Role.Admin).subscribe(x => {
          this.mods = x;
        });
        break;
    }
  }

  public tabChanged(tabChangeEvent: MatTabChangeEvent): void {
    this.clearUsers();
    
    switch (tabChangeEvent.index) {
      case 0:
        this.getUsers();
        break;
      case 1:
        this.getMods();
        break;
    }

    this.selectedTab = tabChangeEvent.index;
  }

  private clearUsers() : void {
    this.users = undefined;
    this.mods = undefined;
  }

  users? : UserModel[];
  mods? : UserModel[];
  isAdm? : boolean;
  selectedTab? : Number;
}
