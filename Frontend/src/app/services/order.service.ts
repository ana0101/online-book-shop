import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderDto } from '../_interfaces/order-dto';
import { Observable } from 'rxjs';
import { UserOrder } from '../_interfaces/user-orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) {}

  readonly APIUrl = "https://localhost:7202/api/Orders/";

  getAllOrdersGroupedByUser(): Observable<any> {
    const url = `${this.APIUrl}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Observable<any>>(url, {headers});
  }

  getStatusOrdersGroupedByUser(status: string): Observable<any> {
    const url = `${this.APIUrl}status/${status}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Observable<any>>(url, {headers});
  }

  getUserOrders(userId: string): Observable<UserOrder[]> {
    const url = `${this.APIUrl}${userId}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<UserOrder[]>(url, {headers});
  }

  createOrder(order: OrderDto): Observable<number> {
    const url = `${this.APIUrl}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<number>(url, order, {headers});
  }

  updateOrderStatus(id: number, newStatus: string): Observable<void> {
    const url = `${this.APIUrl}${id}/${newStatus}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.put<void>(url, newStatus, {headers});
  }
}
