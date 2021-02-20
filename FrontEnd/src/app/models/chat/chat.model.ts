import { MessageModel } from '../message/message.model';
import { UserModel } from '../user/user.model';
import { ChatDetailsModel, ChatDetailsCreateModel } from './chat-details.model';

export interface ChatModel {
    id: number;
    type: ChatType;
    details: ChatDetailsModel;
    participants: UserModel[];
    messages: MessageModel[];
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