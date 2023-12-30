import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private jwtHelper: JwtHelperService = new JwtHelperService();

  isAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");
    return !this.jwtHelper.isTokenExpired(token);
  }
  //constructor() { }
}
