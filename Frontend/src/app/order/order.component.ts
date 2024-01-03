import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit {
  constructor(private router: Router) {}

  orderForm!: FormGroup;
  public errorMessage: string = '';
  public showError: boolean = false;

  ngOnInit(): void {
    this.orderForm = new FormGroup({
      city: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required)
    });
  }

  public createOrder(form: FormGroup): void {
    if (form.invalid) {
      this.errorMessage = "Invalid form";
      this.showError = true;
    }
    else {
      localStorage.setItem("city", form.value.city);
      localStorage.setItem("address", form.value.address);
      this.router.navigate(["payment"]);
    }
  }
}
