import { MessageModel } from '../message/message.model';
import { UserModel } from '../user/user.model';
import { ChatDetailsModel, ChatDetailsCreateModel } from './chat-details.model';

export class ChatModel {
    id: number;
    type: ChatType;
    details: ChatDetailsModel;
    participants: UserModel[];
    messages: MessageModel[];
    hasActiveUsers: boolean;
    unreadMessages: number = 0;
}

export interface ChatCreateModel {
    type: ChatType;
    details?: ChatDetailsCreateModel;
    participantIds: number[];
}

export enum ChatType {
    OneToOne = 1,
    Group
}