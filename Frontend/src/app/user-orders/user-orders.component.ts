import { Component, OnInit } from '@angular/core';
import { UserOrder } from '../_interfaces/user-orders';
import { CommonModule } from '@angular/common';
import { OrderService } from '../services/order.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-orders',
  standalone: true,
  imports: [CommonModule],
  providers: [DatePipe],
  templateUrl: './user-orders.component.html',
  styleUrl: './user-orders.component.scss'
})
export class UserOrdersComponent implements OnInit {

  constructor(private orderService: OrderService) {}

  orders: UserOrder[] = [];

  getUserOrders(userId: string) {
    this.orderService.getUserOrders(userId).subscribe((data: UserOrder[]) => {
      this.orders = data;
    });
  }

  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") || '';
    if (token) {
      this.getUserOrders(userId);
    }
  }
}
