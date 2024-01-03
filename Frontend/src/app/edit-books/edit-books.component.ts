import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from '../services/book.service';
import { AuthenticationService } from '../services/authentication.service';
import { Observable, take } from 'rxjs';
import { Book } from '../_interfaces/book';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-books',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './edit-books.component.html',
  styleUrl: './edit-books.component.scss'
})
export class EditBooksComponent implements OnInit {
  
  constructor(private router: Router, private bookService: BookService, private authService: AuthenticationService) {}

  books$: Observable<Book[]> = new Observable<Book[]>();
  createBookForm!: FormGroup;
  updateBookPriceForm!: FormGroup;
  deleteBookForm!: FormGroup;
  public createMessage: string = '';
  public showCreateMessage: boolean = false;
  public updateMessage: string = '';
  public showUpdateMessage: boolean = false;
  public deleteMessage: string = '';
  public showDeleteMessage: boolean = false;

  getBooks() {
    this.books$ = this.bookService.getBooks();
  }

  createBook(form: FormGroup): void {
    if (form.invalid) {
      this.createMessage = "Invalid form";
      this.showCreateMessage = true;
    }
    else {
      this.bookService.createBook(form.value).pipe(take(1)).subscribe(() => {
        this.createMessage = "Book added succesfully";
        this.showCreateMessage = true;
        this.getBooks();
      })
    }
  }

  updateBookPrice(form: FormGroup): void {
    if (form.invalid) {
      this.updateMessage = "Invalid form";
      this.showUpdateMessage = true;
    }
    else {
      const bookId = form.get('id')?.value;
      const newPrice = form.get('price')?.value;
      this.bookService.updateBookPrice(bookId, newPrice).pipe(take(1)).subscribe({
        next: () => {
          this.updateMessage = "Price updated succesfully";
          this.showUpdateMessage = true;
          this.getBooks();
        },
        error: (err: HttpErrorResponse) => {
          this.updateMessage = err.error.message;
          this.showUpdateMessage = true;
        }
      });
    }
  }

  deleteBook(form: FormGroup): void {
    if (form.invalid) {
      this.deleteMessage = "Invalid form";
      this.showDeleteMessage = true;
    }
    else {
      const bookId = form.get('id')?.value;
      this.bookService.deleteBook(bookId).pipe(take(1)).subscribe({
        next: () => {
          this.deleteMessage = "Book deleted succesfully";
          this.showDeleteMessage = true;
          this.getBooks();
        },
        error: (err: HttpErrorResponse) => {
          this.deleteMessage = err.error.message;
          this.showDeleteMessage = true;
        }
      });
    }
  }

  ngOnInit(): void {
    this.getBooks();

    this.createBookForm = new FormGroup({
      title: new FormControl('', Validators.required),
      author: new FormControl('', Validators.required),
      price: new FormControl('', Validators.required)
    });

    this.updateBookPriceForm = new FormGroup({
      id: new FormControl('', Validators.required),
      price: new FormControl('', Validators.required)
    });

    this.deleteBookForm = new FormGroup({
      id: new FormControl('', Validators.required)
    });
  }
}
