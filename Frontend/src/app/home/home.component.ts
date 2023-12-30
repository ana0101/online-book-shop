import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  readonly APIUrl = "https://localhost:7202/api/Books/";

  constructor(private http:HttpClient) {}

  books: any = [];
  getBooks() {
    this.http.get(this.APIUrl).subscribe(data => {
      this.books = data;
    })
  }

  ngOnInit(): void {
    this.getBooks();
  }
}
