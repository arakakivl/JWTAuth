import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
    private router : Router) { }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      main: ["", [Validators.required, Validators.minLength(4)]],
      password: ["", [Validators.required, Validators.minLength(6)]]
    });
  }

  signIn() : void {
    // calls authService.signIn();
    // token on localStorage
    // redirect
    this.userService.login(this.formGroup?.value).subscribe(x => {
      localStorage.setItem('token', x.token);
      console.log(x.token);
      alert("Logado com sucesso!");
      this.router.navigate(['']);
    }, err => {
      alert(err.error);
    });

  }

  formGroup? : FormGroup;
}
