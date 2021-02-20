import { Component, OnDestroy, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ApiPaths } from 'src/app/constants/api.constants';
import { HubEvents } from 'src/app/constants/hub.constants';
import { ChatModel } from 'src/app/models/chat/chat.model';
import { UserModel } from 'src/app/models/user/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { NotificationService } from 'src/app/services/real-time/notification.service';
import { BaseRestService } from 'src/app/services/rest/base-rest.service';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
  users: UserModel[] = [];
  selectedUser: UserModel;
  selectedChat: ChatModel;

  username: string;

  constructor(
    private restService: BaseRestService,
    private authService: AuthService,
    private notificationService: NotificationService,
    private messageService: MessageService) { }

  ngOnDestroy(): void {
    // this.messageNotificationsService.connection.stop();
  }

  ngOnInit(): void {
    this.restService.get<UserModel>(ApiPaths.Users).subscribe(res => this.users = res);
    this.username = this.authService.getUsername();
    
    this.notificationService.startConnection();
    this.notificationService
      .connection
      .on(HubEvents.Connected, (username: string) => {
        this.messageService.add({ severity: 'success', summary: `${username} connected!` });
      });

    this.notificationService
      .connection
      .on(HubEvents.Disconnected, (username: string) => {
        this.messageService.add({ severity: 'error', summary: `${username} disconnected!` });
      });
  }

  openNewChatDialog() {

  }

  createChat() {
    // this.restService.create();
  }

  chatSelectedHandler(chat: ChatModel) {
    this.selectedChat = chat;
  }
}