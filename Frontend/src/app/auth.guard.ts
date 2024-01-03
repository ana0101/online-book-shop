import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthenticationService } from "./services/authentication.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private authenticationService: AuthenticationService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const requiredRole = route.data["role"] as string; 
    if (this.authenticationService.isAuthenticated()) {
      if (requiredRole === "User" || (requiredRole === "Admin" && this.authenticationService.isAdmin())) {
        return true;
      }
    }
    return this.router.createUrlTree(['/login']);
  }
}
