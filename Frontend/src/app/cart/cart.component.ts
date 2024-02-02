import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CartService } from '../services/cart.service';
import { AuthenticationService } from '../services/authentication.service';
import { Cart } from '../_interfaces/cart';
import { Observable, take } from 'rxjs';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  constructor(private router: Router, private cartService: CartService, private authService: AuthenticationService) {}

  carts$: Observable<Cart[]> = new Observable<Cart[]>();
  emptyMessage: string = '';
  empty: boolean = false;

  getCarts(userId: string) {
    if (this.authService.isAuthenticated()) {
      this.carts$ = this.cartService.getCarts(userId);
    }
  }

  updateQuantity(bookId: number, newQuantity: number) {
    if (this.authService.isAuthenticated()) {
      const userId = localStorage.getItem("userId") ?? '';
      this.cartService.updateQuantity(userId, bookId, newQuantity).pipe(take(1)).subscribe();
    }
  }

  deleteCart(bookId: number) {
    if (this.authService.isAuthenticated()) {
      const userId = localStorage.getItem("userId") ?? '';
      this.cartService.deleteCart(userId, bookId).pipe(take(1)).subscribe(() => {
        this.getCarts(localStorage.getItem("userId") ?? '');
      })
    }
  }

  goToOrder() {
    // go to order only if at least one item in cart
    this.carts$.pipe(take(1)).subscribe(carts => {
      if (carts && carts.length > 0) {
        this.router.navigate(['order']);
      } 
      else {
        this.emptyMessage = "Your cart is empty"
        this.empty = true;
        setTimeout(() => {
          this.empty = false;
        }, 3000);
      }
    });
  }

  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") ?? '';
    if (token) {
      this.getCarts(userId);
    }
  }
}
