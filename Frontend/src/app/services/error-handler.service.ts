import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      return next.handle(req).pipe(catchError((error: HttpErrorResponse) => {
        let errorMessage = this.handleError(error);
        return throwError(() => new Error(errorMessage));
      }))
  }

  private handleError = (error: HttpErrorResponse): string => {
    console.log(this.router.url);
    if (error.status == 400) {
      return this.handleBadRequest(error);
    }
    else if (error.status == 401) {
      return this.handleUnauthorized(error);
    }
    else if (error.status == 404) {
      return this.handleUnauthorized(error);
    }
    else if (error.status == 409) {
      return this.handleConflict(error);
    }
    else {
      return '';
    }
  }

  private handleBadRequest = (error: HttpErrorResponse): string => {
    if (this.router.url == '/Authentication/register') {
      let message = '';
      const values: string[] = Object.values(error.error.errors);
      values.map((m: string) => {
        message += m + '<br>';
      });
      return message.slice(0, -4);
    } 
    else {
      return error.message;
    }
  }  

  private handleUnauthorized = (error: HttpErrorResponse): string => {
    if (this.router.url == '/Authentication/login') {
      let message = '';
      const values: string[] = Object.values(error.error.errors);
      values.map((m: string) => {
        message += m + '<br>';
      });
      return message.slice(0, -4);
    }
    else {
      return error.message;
    }
  }

  private handleNotFound = (error: HttpErrorResponse): string => {
    if (this.router.url == '/Authentication/login') {
      let message = '';
      const values: string[] = Object.values(error.error.errors);
      values.map((m: string) => {
        message += m + '<br>';
      });
      return message.slice(0, -4);
    }
    else {
      return error.message;
    }
  }

  private handleConflict = (error: HttpErrorResponse): string => {
    if (this.router.url == '/Authentication/register') {
      let message = '';
      const values: string[] = Object.values(error.error.errors);
      values.map((m: string) => {
        message += m + '<br>';
      });
      return message.slice(0, -4);
    }
    else {
      return error.message;
    }
  }
}
