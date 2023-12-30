import { CommonModule } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { AuthResponse } from '../_interfaces/auth-response';

@Component({
  selector: 'app-login-user',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login-user.component.html',
  styleUrl: './login-user.component.scss'
})
export class LoginUserComponent implements OnInit {
  readonly APIUrl="https://localhost:7202/api/Authentication/login/";

  constructor(private http: HttpClient, private router: Router) {}

  loginForm!: FormGroup;
  public errorMessage: string = '';
  public showError: boolean = false;
  public invalidLogin: boolean = true;

  ngOnInit(): void {
      this.loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', Validators.required)
      });
  }

  public loginUser(form: FormGroup): void {
    if (form.invalid) {
      this.errorMessage = "Invalid form";
      this.showError = true;
    }
    else {
      this.http.post<AuthResponse>(this.APIUrl, form.value).pipe(take(1)).subscribe({
        next: (respone: AuthResponse) => {
          const token = respone.token;
          localStorage.setItem("jwt", token);
          this.invalidLogin = false;
          console.log("Successful login");
          console.log(token);
          this.router.navigate(['/']);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.error.message;
          this.showError = true;
          this.invalidLogin = true;
        }
      })
    }
  }
}

// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
// eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjNAZ21haWwuY29tIiwianRpIjoiYmYzNTkzOTMtOTM0Zi00NDgyLWIxNmEtZjA4MTk1MDQwYjcwIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTcwMzkzNTg0NSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.
// w3HDW_tMc30Wd9HLRBHV8syfLs89bu6uPMOUmQ5i5Eo

// 0773c156-73ac-483e-97f7-10485c13eafb