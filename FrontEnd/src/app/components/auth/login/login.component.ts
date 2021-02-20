import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/auth/login.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  model: LoginModel = {
    email: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    if(this.authService.loggedIn) {
      this.router.navigate(['']);
    }
  }

  login() {
    this.authService
      .login(this.model)
      .subscribe(res => {
        this.authService.saveToken(res.token);
        this.router.navigate([''])
      });
  }
}
