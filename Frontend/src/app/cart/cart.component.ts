import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  readonly APIUrl = "https://localhost:7202/api/Carts/";

  constructor(private http: HttpClient, private router: Router) {}

  carts: any = [];

  getCarts(userId: string) {
    const url = `${this.APIUrl}user/${userId}`;

    const token = localStorage.getItem("jwt");

    if (token) {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });

      this.http.get(url, {headers}).subscribe(data => {
        this.carts = data;
      });
    }
  }

  updateQuantity(bookId: number, newQuantity: number) {
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") ?? '';
    const url = `${this.APIUrl}${userId}/${bookId}/${newQuantity}`;

    if (token) {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });

      this.http.put<number>(url, newQuantity, {headers}).subscribe();
    }
  }

  deleteCart(bookId: number) {
    console.log("delete cart");
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") ?? '';
    const url = `${this.APIUrl}${userId}/${bookId}`;

    if (token) {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });

      this.http.delete(url, {headers}).subscribe(() => {
        this.getCarts(userId);
      });
    }
  }

  goToOrder() {
    this.router.navigate(['order']);
  }

  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") ?? '';
    if (token) {
      this.getCarts(userId);
    }
  }
}
