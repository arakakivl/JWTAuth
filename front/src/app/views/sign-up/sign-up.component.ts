import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { TokenService } from 'src/app/services/token.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  constructor(
    private formBuilder : FormBuilder,
    private tokenService : TokenService,
    private router : Router,
    private authService : AuthService) { }
  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      username: ["", [Validators.required, Validators.minLength(4), Validators.maxLength(15)]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.minLength(6)]]
      }
    );

    if (this.tokenService.isAuthenticated()) {
      this.router.navigate(['']);
    }
  }

  signUp() : void {
    this.authService.register(this.formGroup?.value).subscribe({
      next: () => {
        alert("Registrado com sucesso!");
        this.router.navigate(['signin']);
      },
      error: () => {
        alert("Usuário já cadastrado! Verifique seus dados e tente novamente.")
      }
    });
  }

  formGroup? : FormGroup;
}
