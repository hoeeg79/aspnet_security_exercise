import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import {ReactiveFormsModule} from "@angular/forms";

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { UsersComponent } from './users.component';
import { RegisterComponent } from './register.component';
import { LoginComponent } from './login.component';
import { ErrorHttpInterceptor } from 'src/interceptors/error-http-interceptors';
import { AuthHttpInterceptor } from "../interceptors/auth-http-interceptor";
import {TokenService} from "../TokenService"

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    RegisterComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot({mode: 'ios'}),
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHttpInterceptor, multi: true },
    TokenService, {provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true}],
  bootstrap: [AppComponent],
})
export class AppModule {}
