import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
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
  
  constructor(private bookService: BookService) {}

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
  public successMessage: string = '';
  public showSuccessMessage: boolean = false;

  hideSuccessMessage(): void {
    this.showSuccessMessage = false;
  }

  getBooks() {
    this.books$ = this.bookService.getBooks();
  }

  createBook(form: FormGroup): void {
    if (form.invalid) {
      this.createMessage = "Invalid form";
      this.showCreateMessage = true;
      setTimeout(() => {
        this.showCreateMessage = false;
      }, 3000);
    }
    else {
      this.bookService.createBook(form.value).pipe(take(1)).subscribe(() => {
        this.successMessage = "Book added successfully";
        this.showSuccessMessage = true;
        this.getBooks();
      })
    }
  }

  updateBookPrice(form: FormGroup): void {
    if (form.invalid) {
      this.updateMessage = "Invalid form";
      this.showUpdateMessage = true;
      setTimeout(() => {
        this.showUpdateMessage = false;
      }, 3000);
    }
    else {
      const bookId = form.get('id')?.value;
      const newPrice = form.get('price')?.value;
      this.bookService.updateBookPrice(bookId, newPrice).pipe(take(1)).subscribe({
        next: () => {
          this.successMessage = "Book price updated successfully";
          this.showSuccessMessage = true;
          this.getBooks();
        },
        error: (err: HttpErrorResponse) => {
          this.updateMessage = err.error.message;
          this.showUpdateMessage = true;
          setTimeout(() => {
            this.showUpdateMessage = false;
          }, 3000);
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
  }
}
