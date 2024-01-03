import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { LoginUserComponent } from './login-user/login-user.component';
import { CartComponent } from './cart/cart.component';
import { AuthGuard } from './auth.guard';
import { OrderComponent } from './order/order.component';
import { PaymentComponent } from './payment/payment.component';
import { UserOrdersComponent } from './user-orders/user-orders.component';

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
        component: CartComponent, canActivate: [AuthGuard]
    },
    {
        path: "order",
        component: OrderComponent, canActivate: [AuthGuard]
    },
    {
        path: "payment",
        component: PaymentComponent, canActivate: [AuthGuard]
    },
    {
        path: "user-orders",
        component: UserOrdersComponent, canActivate: [AuthGuard]
    }
];
