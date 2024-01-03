import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private jwtHelper: JwtHelperService = new JwtHelperService();

  public isAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");
    return !this.jwtHelper.isTokenExpired(token);
  }

  public isAdmin(): boolean {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      let payload;
      payload = token.split(".")[1];
      payload = window.atob(payload);
      const parsedPayload = JSON.parse(payload);
      const roles = parsedPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      console.log(roles);
      if (roles.includes("Admin")) {
        return true;
      }
    }
    return false;
  }
}
