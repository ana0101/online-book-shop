import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { LoginUserComponent } from './login-user/login-user.component';

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
    }
];
