import { Component, OnInit } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { Router } from '@angular/router';
import { Role } from 'src/app/models/role';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { TokenService } from 'src/app/services/token.service';
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
    private behaviorService : AccountBehaviorService,
    private tokenService : TokenService,
    private router : Router) { }

  ngOnInit(): void {
    this.behaviorService.isAuthenticated.subscribe(x => {
      let role = Role[Role.Admin];
      this.isAdm = x && this.tokenService.getRole()?.toString() == role;
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
      this.users = x;
    });
  }

  getMods() : void {
    this.adminService.getMods().subscribe(x => {
      this.mods = x;
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

  deleteUser(username : string, role : Role) : void {
    if (role == Role.Admin) {
      alert("You cannot ban an Admin!");
      return;
    }

    if (this.isAdm) {
      this.adminService.deleteUser(username).subscribe();
      
      this.clearUsers();
      this.getUsers();
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
