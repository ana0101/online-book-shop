import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { take } from 'rxjs';
import { UserOrder } from '../_interfaces/user-orders';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-orders',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './edit-orders.component.html',
  styleUrl: './edit-orders.component.scss'
})
export class EditOrdersComponent implements OnInit {

  constructor(private orderService: OrderService) {}

  orders: { key: string, value: UserOrder[] }[] = [];
  updateStatusForm!: FormGroup;
  errorMessage: string = '';
  showErrorMessage: boolean = false;
  successMessage: string = '';
  showSuccessMessage: boolean = false;

  hideSuccessMessage(): void {
    this.showSuccessMessage = false;
  }

  getAllOrdersGroupedByUser() {
    this.orderService.getAllOrdersGroupedByUser().pipe(take(1)).subscribe((data: Record<string, UserOrder[]>) => {
      this.orders = Object.entries(data).map(([key, value]) => ({ key, value }));
    })
  }

  getStatusOrdersGroupedByUser(event: Event) {
    var status = (event.target as HTMLSelectElement).value;
    if (status == "3") {
      this.getAllOrdersGroupedByUser();
    }
    else {
      this.orderService.getStatusOrdersGroupedByUser(status).pipe(take(1)).subscribe((data: Record<string, UserOrder[]>) => {
        this.orders = Object.entries(data).map(([key, value]) => ({ key, value }));
      })
    }
  }

  updateOrderStatus(form: FormGroup) {
    if (form.invalid) {
      this.errorMessage = "Invalid form";
      this.showErrorMessage = true;
      setTimeout(() => {
        this.showErrorMessage = false;
      }, 3000);
    }
    else {
      const orderId = form.get('id')?.value;
      const newStatus = form.get('newStatus')?.value;
      this.orderService.updateOrderStatus(orderId, newStatus).pipe(take(1)).subscribe({
        next: () => {
          this.successMessage = "Order status updated succesfully";
          this.showSuccessMessage = true;
          this.getAllOrdersGroupedByUser();
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.error.message;
          this.showErrorMessage = true;
          setTimeout(() => {
            this.showErrorMessage = false;
          }, 3000);
        }
      });
    }
  }

  ngOnInit(): void {
    this.getAllOrdersGroupedByUser();

    this.updateStatusForm = new FormGroup({
      id: new FormControl('', Validators.required),
      newStatus: new FormControl('', Validators.required)
    })
  }
}
