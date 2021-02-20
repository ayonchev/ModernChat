export interface MessageCreateModel {
    authorId: number;
    content: string;
    chatId: number;
}

export interface MessageModel {
    id: number;
    authorId: number;
    authorUsername: string;
    content: string;
    chatId: number;
    createdOn: Date;
}