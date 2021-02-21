import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ChatModel } from 'src/app/models/chat/chat.model';
import { MessageCreateModel, MessageModel } from 'src/app/models/message/message.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { NotificationService } from 'src/app/services/real-time/notification.service';
import { HubEvents } from 'src/app/constants/hub.constants';
import { BaseRestService } from 'src/app/services/rest/base-rest.service';
import { ApiPaths } from 'src/app/constants/api.constants';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  @Input() model: ChatModel;

  messageContent: string = '';

  constructor(
    private notificationService: NotificationService,
    private restService: BaseRestService,
    private authService: AuthService) { }

  ngOnInit(): void { }

  send() {
    if(!this.messageContent) {
      return;
    }

    let currentUserId = this.authService.getId();

    let message: MessageCreateModel = {
      content: this.messageContent,
      authorId: currentUserId,
      chatId: this.model.id
    };

    this.notificationService
      .sendMessage(message)
      .then(() => this.messageContent = '')
      .catch(err => console.log(err));
  }
}