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
