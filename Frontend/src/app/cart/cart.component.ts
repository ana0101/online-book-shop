import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  readonly APIUrl = "https://localhost:7202/api/Carts/";

  constructor(private http: HttpClient) {}

  carts: any = [];

  getCarts(userId: string) {
    const url = `${this.APIUrl}user/${userId}`;

    const token = localStorage.getItem("jwt");

    if (token) {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });

      this.http.get(url, { headers }).subscribe(data => {
        this.carts = data;
      });
    }
  }

  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    
    if (token) {
      let payload;
      payload = token.split(".")[1];
      payload = window.atob(payload);
      const parsedPayload = JSON.parse(payload);
      const userId = parsedPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      console.log(userId);
      this.getCarts(userId);
    }
  }
}
