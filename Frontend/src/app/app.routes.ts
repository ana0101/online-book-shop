import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { LoginUserComponent } from './login-user/login-user.component';
import { CartComponent } from './cart/cart.component';
import { AuthGuard } from './auth.guard';
import { OrderComponent } from './order/order.component';
import { PaymentComponent } from './payment/payment.component';
import { UserOrdersComponent } from './user-orders/user-orders.component';
import { EditBooksComponent } from './edit-books/edit-books.component';
import { EditOrdersComponent } from './edit-orders/edit-orders.component';
import { EditUsersComponent } from './edit-users/edit-users.component';

export const routes: Routes = [
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "register",
        component: RegisterUserComponent
    },
    {
        path: "login",
        component: LoginUserComponent	
    },
    {
        path: "cart",
        component: CartComponent,
        canActivate: [AuthGuard],
        data: { role: "User" }
    },
    {
        path: "order",
        component: OrderComponent,
        canActivate: [AuthGuard],
        data: { role: "User" } 
    },
    {
        path: "payment",
        component: PaymentComponent,
        canActivate: [AuthGuard],
        data: { role: "User" } 
    },
    {
        path: "user-orders",
        component: UserOrdersComponent,
        canActivate: [AuthGuard],
        data: { role: "User" } 
    },
    {
        path: "edit-books",
        component: EditBooksComponent,
        canActivate: [AuthGuard],
        data: { role: "Admin" } 
    },
    {
        path: "edit-orders",
        component: EditOrdersComponent,
        canActivate: [AuthGuard],
        data: { role: "Admin" } 
    },
    {
        path: "edit-users",
        component: EditUsersComponent,
        canActivate: [AuthGuard],
        data: { role: "Admin" }
    }
];
