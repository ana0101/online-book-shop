import { Injectable } from '@angular/core';
import { CreateUser } from '../_interfaces/create-user';
import { Response } from 'express';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }
}
