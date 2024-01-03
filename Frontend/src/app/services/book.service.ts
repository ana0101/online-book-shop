import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CartDto } from '../_interfaces/cart-dto';
import { AuthenticationService } from './authentication.service';
import { Book } from '../_interfaces/book';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http:HttpClient, private router: Router, private authenticationService: AuthenticationService) {}

  readonly APIUrl = "https://localhost:7202/api/";

  getBooks(): Observable<Book[]> {
    const url = `${this.APIUrl}Books/`;
    return this.http.get<Book[]>(url);
  }
}
