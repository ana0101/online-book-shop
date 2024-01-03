import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserOrder } from '../_interfaces/user-orders';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-orders.component.html',
  styleUrl: './user-orders.component.scss'
})
export class UserOrdersComponent implements OnInit {
  readonly APIUrl = "https://localhost:7202/api/Orders/";

  constructor(private http: HttpClient) {}

  orders: UserOrder[] = [];

  getOrders(userId: string) {
    const url = `${this.APIUrl}${userId}`;

    const token = localStorage.getItem("jwt");

    if (token) {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });

      this.http.get<UserOrder[]>(url, {headers}).subscribe(data => {
        this.orders = data;
        console.log(this.orders);
      });
    }
  }

  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") ?? '';
    if (token) {
      this.getOrders(userId);
    }
  }
}
