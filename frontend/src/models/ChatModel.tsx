export interface MessageModel {
	senderId: string;
	receiverId: string;
	messageText: string;
	messageRead: boolean;
	messageDate?: string;
}

export interface ChatModel {
	messages: MessageModel[];
}
