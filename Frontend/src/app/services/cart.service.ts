import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from '../_interfaces/cart';
import { CartDto } from '../_interfaces/cart-dto';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private http: HttpClient) {}

  readonly APIUrl = "https://localhost:7202/api/Carts/";

  getCarts(userId: string): Observable<Cart[]> {
    const url = `${this.APIUrl}user/${userId}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Cart[]>(url, {headers});
  }

  getQuantity(userId: string, bookId: number): Observable<number> {
    const url = `${this.APIUrl}${userId}/${bookId}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<number>(url, {headers});
  }

  createCart(cartDto: CartDto): Observable<CartDto> {
    const url = `${this.APIUrl}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<CartDto>(url, cartDto, {headers});
  }

  updateQuantity(userId: string, bookId: number, newQuantity: number): Observable<void> {
    const token = localStorage.getItem("jwt");
    const url = `${this.APIUrl}${userId}/${bookId}/${newQuantity}`;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.put<void>(url, newQuantity, {headers});
  }

  deleteCart(userId: string, bookId: number): Observable<void> {
    const token = localStorage.getItem("jwt");
    const url = `${this.APIUrl}${userId}/${bookId}`;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete<void>(url, {headers});
  }

  deleteCarts(userId: string): Observable<void> {
    const token = localStorage.getItem("jwt");
    const url = `${this.APIUrl}${userId}`;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete<void>(url, {headers});
  }
}
