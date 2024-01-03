import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartDto } from '../_interfaces/cart-dto';
import { BookService } from '../services/book.service';
import { Book } from '../_interfaces/book';
import { AuthenticationService } from '../services/authentication.service';
import { CartService } from '../services/cart.service';
import { Observable, take } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  constructor(private router: Router, private bookService: BookService, private cartService: CartService, private authService: AuthenticationService) {}

  books$: Observable<Book[]> = new Observable<Book[]>();

  getBooks() {
    this.books$ = this.bookService.getBooks();
  }

  addToCart(bookId: number) {
    if (this.authService.isAuthenticated()) {

      // check if the user already has a cart with this book
      const userId = localStorage.getItem("userId") ?? '';
      var quantity;
      this.cartService.getQuantity(userId, bookId).pipe(take(1)).subscribe((data) => {
        quantity = data;

        if (quantity != 0) {
          // increase the quantity
          const newQuantity = quantity + 1;
          this.cartService.updateQuantity(userId, bookId, newQuantity).pipe(take(1)).subscribe();
        }
        else {
          // add the new cart
          const cartDto: CartDto = {
            applicationUserId: userId,
            bookId: bookId,
            quantity: 1 
          };
          this.cartService.createCart(cartDto).pipe(take(1)).subscribe();
        }
      });
    } 
    else {
      this.router.navigate(['login']);
    }
  }

  ngOnInit(): void {
    this.getBooks();
  }
}
