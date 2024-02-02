import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CartDto } from '../_interfaces/cart-dto';
import { AuthenticationService } from './authentication.service';
import { Book } from '../_interfaces/book';
import { Observable } from 'rxjs';
import { BookDto } from '../_interfaces/book-dto';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient, private router: Router, private authenticationService: AuthenticationService) {}

  readonly APIUrl = "https://localhost:7202/api/Books/";

  getBooks(): Observable<Book[]> {
    const url = `${this.APIUrl}`;
    return this.http.get<Book[]>(url);
  }

  getBooksSorted(sortType: string): Observable<Book[]> {
    if (sortType == "title-asc") {
      var order = true;
      const url = `${this.APIUrl}sortTitle/${order}`;
      return this.http.get<Book[]>(url);
    }
    else if (sortType == "title-desc") {
      var order = false;
      const url = `${this.APIUrl}sortTitle/${order}`;
      return this.http.get<Book[]>(url);
    }
    else if (sortType == "price-asc") {
      var order = true;
      const url = `${this.APIUrl}sortPrice/${order}`;
      return this.http.get<Book[]>(url);
    }
    else if (sortType == "price-desc") {
      var order = false;
      const url = `${this.APIUrl}sortPrice/${order}`;
      return this.http.get<Book[]>(url);
    }
    return new Observable;
  }

  getBooksSearch(search: string): Observable<Book[]> {
    if (search.trim() == '') {
      return this.getBooks();
    }
    const url = `${this.APIUrl}search/${search}`;
    return this.http.get<Book[]>(url);
  }

  createBook(bookDto: BookDto): Observable<Book> {
    const url = `${this.APIUrl}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<Book>(url, bookDto, {headers});
  }

  updateBookPrice(bookId: number, newPrice: number): Observable<void> {
    const url = `${this.APIUrl}${bookId}/${newPrice}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.put<void>(url, newPrice, {headers});
  }

  deleteBook(bookId: number): Observable<void> {
    const url = `${this.APIUrl}${bookId}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete<void>(url, {headers});
  }
}
