import { Component, OnInit } from '@angular/core';
import { CreateUser } from '../_interfaces/create-user';
import { AuthenticationService } from '../services/authentication.service';
import { Form, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-user',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register-user.component.html',
  styleUrl: './register-user.component.scss'
})
export class RegisterUserComponent implements OnInit {
  readonly APIUrl="https://localhost:7202/api/Authentication/register/";

  constructor(private http: HttpClient, private router: Router) {}

  registerForm!: FormGroup;
  public errorMessage: string = '';
  public showError: boolean = false;

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      userName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    });
  }

  public registerUser(form: FormGroup): void {
    if (form.invalid) {
      this.errorMessage = "Invalid form";
      this.showError = true;
    }
    else {
      this.http.post<FormGroup>(this.APIUrl, form.value).pipe(take(1)).subscribe({
        next: (_) => {
          console.log("Successful registration");
          this.router.navigate(['/login']);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.error.message;
          this.showError = true;
        }
      });
    }
  }
}
