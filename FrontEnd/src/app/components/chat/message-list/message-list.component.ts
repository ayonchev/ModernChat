import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MessageModel } from 'src/app/models/message/message.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.scss']
})
export class MessageListComponent implements OnInit {
  @Input() messages: MessageModel[];
  @Output() messageDeleted: EventEmitter<number> = new EventEmitter<number>();

  currentUserId: number;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.getId();
  }

  deleteMessage(messageId: number) {
    this.messageDeleted.emit(messageId);
  }
}
