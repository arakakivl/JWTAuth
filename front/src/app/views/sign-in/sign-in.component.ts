import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountBehaviorService } from 'src/app/services/account-behavior.service';
import { AuthService } from 'src/app/services/auth.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  constructor(
    private formBuilder : FormBuilder,
    private tokenService : TokenService,
    private authService : AuthService,
    private router : Router,
    private behavior : AccountBehaviorService) { }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      main: ["", [Validators.required, Validators.minLength(4)]],
      password: ["", [Validators.required, Validators.minLength(6)]]
    });

    if (this.tokenService.isAuthenticated())
    {
      this.router.navigate(['']);
    }
  }

  signIn() : void {
    this.authService.login(this.formGroup?.value).subscribe({
      next: tkns => {
        this.authService.addTokens(tkns.token, tkns.refresh);
        
        alert("Logado com sucesso!");
        if (this.loginError) {
          this.loginError = undefined;
        }

        this.behavior.isAuthenticated.next(this.tokenService.isAuthenticated());
        this.router.navigate(['']);
      },
      error: err => {
        this.loginError = "Credenciais inválidas.";
      }
    });
  }

  formGroup? : FormGroup;
  loginError? : string;
}
