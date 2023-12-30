import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartDto } from '../_interfaces/cart-dto';
import { take } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  readonly APIUrl = "https://localhost:7202/api/";

  constructor(private http:HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.getBooks();
  }

  books: any = [];
  getBooks() {
    const url = `${this.APIUrl}Books/`;
    this.http.get(url).subscribe(data => {
      this.books = data;
    });
  }

  addToCart(bookId: number) {
    // check if the user is authenticated
    const token = localStorage.getItem("jwt");
    if (token) {
      // check if the user already has a cart with this book
      const userId = localStorage.getItem("userId") ?? '';
      var url = `${this.APIUrl}Carts/${userId}/${bookId}`;
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });
  
      this.http.get<number>(url, {headers}).subscribe(quantity => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`
        });

        if (quantity != 0) {
          // increase the quantity
          console.log(quantity);
          const newQuantity = quantity + 1;
          url = `${this.APIUrl}Carts/${userId}/${bookId}/${newQuantity}`;
          console.log(newQuantity);
          this.http.put<number>(url, newQuantity, {headers}).subscribe();
        }
        else {
          // add the new cart
          url = `${this.APIUrl}Carts/`;
          const cartDto: CartDto = {
            applicationUserId: userId,
            bookId: bookId,
            quantity: 1 
          };

          this.http.post<CartDto>(url, cartDto, {headers}).subscribe();
        }
      });
    } 
    else {
      this.router.navigate(['login']);
    }
  }
}
