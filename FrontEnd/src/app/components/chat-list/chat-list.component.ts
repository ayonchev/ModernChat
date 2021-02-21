import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ApiPaths } from 'src/app/constants/api.constants';
import { ChatModel, ChatType } from 'src/app/models/chat/chat.model';
import { UserModel } from 'src/app/models/user/user.model';
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
  activeUserStyle

  constructor(private restService: BaseRestService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.assignActiveChats();
  }

  ngOnInit(): void {
    this.restService
      .get<ChatModel>(ApiPaths.Chats, undefined, { userChatsOnly: true })
      .subscribe(res =>  { this.chats = res; this.assignActiveChats(); });
  }

  selectChat() {
    this.chatSelectionChanged.emit(this.selectedChat);
  }

  assignActiveChats() {
    this.chats
      .forEach(c => {
        c.isActive = c.participants
          .some(p => this.activeUsers.map(au => au.username).includes(p.username));
      });
  }
}
