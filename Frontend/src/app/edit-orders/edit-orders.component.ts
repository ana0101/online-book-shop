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
  public message: string = '';
  public showMessage: boolean = false;

  getAllOrdersGroupedByUser() {
    this.orderService.getAllOrdersGroupedByUser().pipe(take(1)).subscribe((data: Record<string, UserOrder[]>) => {
      this.orders = Object.entries(data).map(([key, value]) => ({ key, value }));
    })
  }

  getStatusOrdersGroupedByUser(event: Event) {
    var status = (event.target as HTMLSelectElement).value;
    this.orderService.getStatusOrdersGroupedByUser(status).pipe(take(1)).subscribe((data: Record<string, UserOrder[]>) => {
      this.orders = Object.entries(data).map(([key, value]) => ({ key, value }));
    })
  }

  updateOrderStatus(form: FormGroup) {
    if (form.invalid) {
      this.message = "Invalid form";
      this.showMessage = true;
    }
    else {
      const orderId = form.get('id')?.value;
      const newStatus = form.get('newStatus')?.value;
      this.orderService.updateOrderStatus(orderId, newStatus).pipe(take(1)).subscribe({
        next: () => {
          this.message = "Order status updated succesfully";
          this.showMessage = true;
          this.getAllOrdersGroupedByUser();
        },
        error: (err: HttpErrorResponse) => {
          this.message = err.error.message;
          this.showMessage = true;
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
