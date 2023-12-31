import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartDto } from '../_interfaces/cart-dto';
import { BookService } from '../services/book.service';
import { Book } from '../_interfaces/book';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  readonly APIUrl = "https://localhost:7202/api/";

  constructor(private http:HttpClient, private router: Router, protected bookService: BookService) {}

  ngOnInit(): void {
    this.bookService.getBooks();
  }
}
