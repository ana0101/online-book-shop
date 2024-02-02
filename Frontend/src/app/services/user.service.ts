import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {}

  readonly APIUrl = "https://localhost:7202/api/Authentication/";

  getUsers(role: string): Observable<User[]> {
    const url = `${this.APIUrl}${role}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<User[]>(url, {headers});
  }

  promoteUserToAdmin(email: string): Observable<void> {
    const url = `${this.APIUrl}promote-to-admin/${email}`;
    const token = localStorage.getItem("jwt");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.put<void>(url, email, {headers});
  }
}
