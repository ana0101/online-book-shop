import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Observable, take } from 'rxjs';
import { User } from '../_interfaces/user';
import { FormControl, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-users',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './edit-users.component.html',
  styleUrl: './edit-users.component.scss'
})
export class EditUsersComponent implements OnInit {

  constructor(private userService: UserService) {}

  users$: Observable<User[]> = new Observable<User[]>();
  promoteUserToAdminForm!: FormGroup;
  deleteUserForm!: FormGroup;
  errorMessage: string = '';
  showErrorMessage: boolean = false;
  deleteMessage: string = '';
  showDeleteMessage: boolean = false;
  successMessage: string  = '';
  showSuccessMessage: boolean = false;

  hideSuccessMessage(): void {
    this.showSuccessMessage = false;
  }

  getUsers() {
    this.users$ = this.userService.getUsers();
  }

  getRoleUsers(event: Event) {
    var role = (event.target as HTMLSelectElement).value;
    if (role == "all") {
      this.getUsers();
    }
    else {
      this.users$ = this.userService.getRoleUsers(role);
    }
  }

  promoteUserToAdmin(form: FormGroup): void {
    if (form.invalid) {
      this.errorMessage = "Invalid form";
      this.showErrorMessage = true;
      setTimeout(() => {
        this.showErrorMessage = false;
      }, 3000);
    }
    else {
      const email = form.get('email')?.value;
      this.userService.promoteUserToAdmin(email).pipe(take(1)).subscribe({
        next: () => {
          this.successMessage = "User promoted to admin successfully";
          this.showSuccessMessage = true;
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.error?.message || 'An error occurred';
          this.showErrorMessage = true;
          setTimeout(() => {
            this.showErrorMessage = false;
          }, 3000);
        }
      });
    }
  }

  deleteUser(form: FormGroup): void {
    if (form.invalid) {
      this.deleteMessage = "Invalid form";
      this.showDeleteMessage = true;
      setTimeout(() => {
        this.showDeleteMessage = false;
      }, 3000);
    }
    else {
      const email = form.get('email')?.value;
      this.userService.deleteUser(email).pipe(take(1)).subscribe({
        next: () => {
          this.successMessage = "User deleted successfully";
          this.showSuccessMessage = true;
        },
        error: (err: HttpErrorResponse) => {
          this.deleteMessage = err.error.message || 'An error occurred';
          this.showDeleteMessage = true;
          setTimeout(() => {
            this.showDeleteMessage = false;
          }, 3000);
        }
      });
    }
  }

  ngOnInit(): void {
    this.getUsers();

    this.promoteUserToAdminForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });

    this.deleteUserForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }
}
