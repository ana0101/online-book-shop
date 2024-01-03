import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Payment } from '../_interfaces/payment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) {}

  readonly APIUrl = "https://localhost:7202/api/Payments/";

  createPayment(payment: Payment): Observable<void> {
    const url = `${this.APIUrl}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<void>(url, payment, {headers});
  }
}
