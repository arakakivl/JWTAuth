import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  constructor(
    private formBuilder : FormBuilder,
    private usersService : AccountService,
    private router : Router) { }
  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      username: ["", [Validators.required, Validators.minLength(4), Validators.maxLength(15)]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.minLength(6)]]
      }
    );

    if (this.usersService.isAuthenticated())
    {
      this.router.navigate(['']);
    }
  }

  signUp() : void {
    this.usersService.register(this.formGroup?.value).subscribe(x => {
      alert("Registrado com sucesso!");
      this.router.navigate(['signin']);
    });
  }

  formGroup? : FormGroup;
}
