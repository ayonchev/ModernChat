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
export class ChatComponent implements OnInit, OnChanges {
  @Input() model: ChatModel;

  messageContent: string = '';

  constructor(
    private notificationService: NotificationService,
    private restService: BaseRestService,
    private authService: AuthService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.restService
      .get<MessageModel>(ApiPaths.Messages, this.model.id.toString())
      .subscribe(res => this.model.messages = res);
  }

  ngOnInit(): void {
    this.notificationService
      .connection
      .on(HubEvents.MessageReceived, (message: MessageModel) => {
        this.model.messages.push(message);
      });

    this.notificationService
      .connection
      .on(HubEvents.MessageDeleted, (messageId: number) => {
        this.model.messages = this.model.messages.filter(m => m.id !== messageId);
      });
  }

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

  messageDeletedHandler(messageId: number) {
    this.notificationService
      .deleteMessage(messageId, this.model.id)
      .then(() => console.log('Message deleted successfully!'))
      .catch(err => console.log(err));
  }
}