import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  constructor(
    private formBuilder : FormBuilder,
    private userService : AccountService,
    private router : Router,
    private behavior : AccountBehaviorService) { }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      main: ["", [Validators.required, Validators.minLength(4)]],
      password: ["", [Validators.required, Validators.minLength(6)]]
    });
  }

  signIn() : void {
    this.userService.login(this.formGroup?.value).subscribe(x => {
      localStorage.setItem('token', x.token);
      this.behavior.isAuthenticated.next(this.userService.isAuthenticated());
      alert("Logado com sucesso!");
      this.router.navigate(['']);
    });
  }

  formGroup? : FormGroup;
}
