import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Cart } from '../_interfaces/cart';
import { Order } from '../_interfaces/order';
import { CommonModule } from '@angular/common';
import { Payment } from '../_interfaces/payment';
import { CartService } from '../services/cart.service';
import { AuthenticationService } from '../services/authentication.service';
import { Observable, of, take } from 'rxjs';
import { OrderService } from '../services/order.service';
import { BookOrderService } from '../services/book-order.service';
import { PaymentService } from '../services/payment.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit {

  constructor(private cartService: CartService, private orderService: OrderService, private bookOrderService: BookOrderService, private paymentService: PaymentService, private authService: AuthenticationService, private router: Router) {}

  total: number = 0;
  paymentForm!: FormGroup;
  public errorMessage: string = '';
  public showError: boolean = false;

  ngOnInit(): void {
    const userId = localStorage.getItem("userId") || '';
    this.getCarts(userId);

    this.paymentForm = new FormGroup({
      type: new FormControl('')
    });
  }

  private getCarts(userId: string): void {
    if (this.authService.isAuthenticated()) {
      this.cartService.getCarts(userId).pipe(take(1)).subscribe((carts: Cart[]) => {
        this.total = this.calculateTotal(carts); 
      });
    }
  }  

  private calculateTotal(carts: Cart[]): number {
    let total = 0;
    carts.forEach((cart: Cart) => {
      if (cart && cart.book) {
        total += cart.book.price * cart.quantity;
      }
    });
    return total;
  }

  public placeOrder(form: FormGroup): void {
    if (form.invalid) {
      this.errorMessage = "Invalid form";
      this.showError = true;
    } 
    else {
      const userId = localStorage.getItem("userId") ?? '';

      // call create order
      const order: Order = {
        applicationUserId: userId || '',
        date: new Date(),
        city: localStorage.getItem("city") || '',
        address: localStorage.getItem("address") || '',
        status: 'Received'
      };

      var orderId: number;
      this.orderService.createOrder(order).pipe(take(1)).subscribe(
        (data) => {
          orderId = data;
          console.log(orderId);

          // call create book orders
          this.bookOrderService.createBookOrders(userId, orderId).pipe(take(1)).subscribe(() => {
              // call delete carts
              this.cartService.deleteCarts(userId).pipe(take(1)).subscribe();

              // call create payment
              const payment: Payment = {
                orderId: orderId,
                total: this.total,
                type: form.get("type")?.value
              };
              this.paymentService.createPayment(payment).pipe(take(1)).subscribe();

              // delete city and address from localStorage
              localStorage.removeItem("city");
              localStorage.removeItem("address");
            })
        })

      this.router.navigate(['/']);
    }
  }
}
