import { AfterContentInit, AfterViewInit, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { VirtualScroller } from 'primeng/virtualscroller';
import { ApiPaths } from 'src/app/constants/api.constants';
import { HubEvents } from 'src/app/constants/hub.constants';
import { MessageModel } from 'src/app/models/message/message.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { NotificationService } from 'src/app/services/real-time/notification.service';
import { BaseRestService } from 'src/app/services/rest/base-rest.service';

@Component({
  selector: 'message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.scss']
})
export class MessageListComponent implements OnInit, OnChanges {
  @Input() chatId: number;
  @ViewChild('messageList') vs: VirtualScroller;
  
  @Input() messages: MessageModel[] = [];

  currentUserId: number;

  constructor(
    private authService: AuthService,
    private restService: BaseRestService,
    private notificationService: NotificationService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.restService
      .get<MessageModel>(ApiPaths.Messages, this.chatId.toString())
      .subscribe(res => {
        this.messages = res;

        this.scrollToBottom();
      });
  }

  ngOnInit(): void {
    this.currentUserId = this.authService.getId();

    this.notificationService
      .connection
      .on(HubEvents.MessageReceived, (message: MessageModel) => {
        if(message.chatId === this.chatId) {
          this.messages = [...this.messages, message];// The scroller doesn't reflect when a new item is pushed to the array. :(
          this.scrollToBottom();
        }
      });

    this.notificationService
      .connection
      .on(HubEvents.MessageDeleted, (messageId: number) => {
        this.messages = this.messages.filter(m => m.id !== messageId);
      });
  }

  deleteMessage(messageId: number) {
    this.notificationService
      .deleteMessage(messageId, this.chatId)
      .then(() => console.log('Message deleted successfully!'))
      .catch(err => console.log(err));
  }
  
  scrollToBottom() {
    setTimeout(() => {
      this.vs?.scrollToIndex(this.messages.length, 'smooth');
    }, 200);
  }
}
