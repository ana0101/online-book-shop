import { JwtModule, JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { NgModule } from '@angular/core';
//import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ErrorHandlerService } from './services/error-handler.service';
import { AuthGuard } from './auth.guard';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    //BrowserModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:4200'],
        disallowedRoutes: [],
      },
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true,
    },
    JwtHelperService,
    {
      provide: JWT_OPTIONS,
      useValue: JWT_OPTIONS,
    },
    AuthGuard,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
