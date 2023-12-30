import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
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

      this.http.get(url, {headers}).subscribe(data => {
        this.carts = data;
      });
    }
  }

  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId") ?? '';
    if (token) {
      this.getCarts(userId);
    }
  }
}
