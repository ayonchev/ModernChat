import { Injectable } from '@angular/core';
import * as SignalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { HubMethods, HubPaths } from 'src/app/constants/hub.constants';
import { MessageCreateModel } from 'src/app/models/message/message.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private url: string = environment.backEndUrl + HubPaths.Notifications;
  connection: SignalR.HubConnection;

  constructor() { }

  startConnection() {
    this.connection = new SignalR.HubConnectionBuilder()
      .withAutomaticReconnect([1, 5, 10, 20])
      .withUrl(this.url, {
        accessTokenFactory: () => {
          return localStorage.getItem("token");
        }
      })
      .build();

    this.connection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Connection failed! ' + err));
  }

  sendMessage(message: MessageCreateModel): Promise<any> {
    return this.connection
      .invoke(HubMethods.SendMessage, message);
  }

  deleteMessage(messageId: number, chatId: number): Promise<any> {
    return this.connection
      .invoke(HubMethods.DeleteMessage, messageId, chatId);
  }
}