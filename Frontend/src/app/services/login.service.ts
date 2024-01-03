import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginUser } from '../_interfaces/login-user';
import { Observable } from 'rxjs';
import { AuthResponse } from '../_interfaces/auth-response';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) {}

  readonly APIUrl="https://localhost:7202/api/Authentication/login/";

  login(loginUser: LoginUser): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.APIUrl, loginUser);
  }
}
