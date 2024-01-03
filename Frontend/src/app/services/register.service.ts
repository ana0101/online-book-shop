import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUser } from '../_interfaces/create-user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) {}

  readonly APIUrl = "https://localhost:7202/api/Authentication/register/";

  register(createUser: CreateUser): Observable<void> {
    const url = `${this.APIUrl}`;
    return this.http.post<void>(url, createUser);
  }
}
