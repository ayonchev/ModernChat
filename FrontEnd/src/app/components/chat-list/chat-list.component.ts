import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ApiPaths } from 'src/app/constants/api.constants';
import { HubEvents } from 'src/app/constants/hub.constants';
import { ChatModel, ChatType } from 'src/app/models/chat/chat.model';
import { MessageModel } from 'src/app/models/message/message.model';
import { UserModel } from 'src/app/models/user/user.model';
import { NotificationService } from 'src/app/services/real-time/notification.service';
import { BaseRestService } from 'src/app/services/rest/base-rest.service';

@Component({
  selector: 'chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss']
})
export class ChatListComponent implements OnInit, OnChanges {
  @Input() activeUsers: UserModel[] = [];
  @Output() chatSelectionChanged: EventEmitter<ChatModel> = new EventEmitter<ChatModel>();

  ChatType = ChatType;

  chats: ChatModel[] = [];
  selectedChat: ChatModel;

  constructor(
    private restService: BaseRestService,
    private notificationService: NotificationService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.assignActiveChats();
  }

  ngOnInit(): void {
    this.restService
      .get<ChatModel>(ApiPaths.Chats, undefined, { userChatsOnly: true })
      .subscribe(res =>  { 
        this.chats = res; 
        this.chats.forEach(c => c.unreadMessages = 0);
        this.assignActiveChats(); 
      });

    this.notificationService
      .connection
      .on(HubEvents.MessageReceived, (message: MessageModel) => {
        if(message.chatId != this.selectedChat.id) {
          let chat = this.chats.find(c => c.id === message.chatId);
          chat.unreadMessages++;
        }
      });
  }

  selectChat() {
    this.chatSelectionChanged.emit(this.selectedChat);
  }

  assignActiveChats() {
    this.chats
      .forEach(c => {
        c.hasActiveUsers = c.participants
          .some(p => this.activeUsers.map(au => au.username).includes(p.username));
      });
  }
}
