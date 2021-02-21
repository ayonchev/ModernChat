import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterModel } from 'src/app/models/auth/register.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  model: RegisterModel = {
    email: '',
    password: '',
    confirmPassword: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }

  register() {
    this.authService
      .register(this.model)
      .subscribe(res => {
        this.authService.saveToken(res.token);
        this.router.navigate(['']);
      })
  }
}
