import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'; 
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  constructor(private authenticationService: AuthenticationService, private router: Router) {}

  ngOnInit(): void {}

  isUserAuthenticated = (): boolean => {
    return this.authenticationService.isAuthenticated();
  }

  logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(['/']); 
  }
}
