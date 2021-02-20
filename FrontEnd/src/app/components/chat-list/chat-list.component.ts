import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ApiPaths } from 'src/app/constants/api.constants';
import { ChatModel } from 'src/app/models/chat/chat.model';
import { BaseRestService } from 'src/app/services/rest/base-rest.service';

@Component({
  selector: 'chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss']
})
export class ChatListComponent implements OnInit {
  @Output() chatSelectionChanged: EventEmitter<ChatModel> = new EventEmitter<ChatModel>();

  chats: ChatModel[];
  selectedChat: ChatModel;

  constructor(private restService: BaseRestService) { }

  ngOnInit(): void {
    this.restService
      .get<ChatModel>(ApiPaths.Chats, undefined, { userChatsOnly: true })
      .subscribe(res => this.chats = res);
  }

  selectChat() {
    this.chatSelectionChanged.emit(this.selectedChat);
  }
}
