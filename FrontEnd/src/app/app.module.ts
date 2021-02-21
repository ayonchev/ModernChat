import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { LoginComponent } from './components/auth/login/login.component';
import { ChatListComponent } from './components/chat-list/chat-list.component';
import { ChatComponent } from './components/chat/chat.component';
import { MessageListComponent } from './components/chat/message-list/message-list.component';
import { HomeComponent } from './components/home/home.component';
import { AuthService } from './services/auth/auth.service';
import { AuthGuard } from './services/auth/auth-guard.service';
import { AuthInterceptor } from './services/auth/auth-interceptor.service';
import { NotificationService } from './services/real-time/notification.service';
import { BaseRestService } from 'src/app/services/rest/base-rest.service';

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { ChipModule } from 'primeng/chip';
import { CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { VirtualScrollerModule } from 'primeng/virtualscroller';
import { JwtModule } from '@auth0/angular-jwt';
import { MessageService } from 'primeng/api';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    ChatListComponent,
    HomeComponent,
    ChatComponent,
    MessageListComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    PasswordModule,
    InputTextModule,
    ButtonModule,
    TableModule,
    DropdownModule,
    InputTextareaModule,
    AvatarModule,
    AvatarGroupModule,
    ChipModule,
    CardModule,
    ToastModule,
    VirtualScrollerModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token')
        // allowedDomains: ["localhost:5000", "http://api.test.tms-eu.com"]
      }
    })
  ],
  providers: [
    AuthService,
    AuthGuard,
    NotificationService,
    BaseRestService,
    MessageService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
