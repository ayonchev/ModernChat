import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginModel } from 'src/app/models/auth/login.model';
import { ApiPaths } from 'src/app/constants/api.constants';
import { RegisterModel } from 'src/app/models/auth/register.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private username: string;
  private userId: number;

  constructor(
    private http: HttpClient,
    private jwtService: JwtHelperService,
  ) { }

  register(model: RegisterModel) {
    return this.http.post<any>(environment.backEndUrl + ApiPaths.AuthRegister, model);
  }

  login(model: LoginModel): Observable<any> {
    return this.http.post<any>(environment.backEndUrl + ApiPaths.AuthLogin, model);
  }

  loggedIn() {
    let token = this.getToken();

    return token && !this.jwtService.isTokenExpired(token);
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getId(): number {
    if(this.userId)
      return this.userId;

    let token = this.getToken();

    let decoded = this.jwtService.decodeToken(token);
    this.userId = +decoded.id;

    return this.userId;
  }

  getUsername(): string {
    if(this.username)
      return this.username;

    let token = this.getToken();

    let decoded = this.jwtService.decodeToken(token);
    this.username = decoded.username;

    return this.username;
  }
}
