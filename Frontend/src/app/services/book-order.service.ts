import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookOrderService {

  constructor(private http: HttpClient) {}

  readonly APIUrl = "https://localhost:7202/api/BookOrders/";

  createBookOrders(userId: string, orderId: number): Observable<void> {
    const url = `${this.APIUrl}${userId}/${orderId}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<void>(url, orderId, {headers});
  } 
}
