import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { UserModel } from '../../models/userModel';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  constructor(private adminService : AdminService) { }
  ngOnInit(): void {
    this.getUsers();
    this.getMods();
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
}
