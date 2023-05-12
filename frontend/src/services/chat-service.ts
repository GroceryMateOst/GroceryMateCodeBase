/* eslint-disable @typescript-eslint/no-unsafe-return */
import { ChatModel } from '../models/ChatModel';
import { AxiosBaseService } from './axios-base.service';

export default class Chatservice extends AxiosBaseService {
	constructor() {
		super('/Chat');
	}

	public async getMessages(groceryId: string): Promise<ChatModel> {
		return this.instance
			.get(`?groceryId=${groceryId}`)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async readMessages(groceryId: string): Promise<void> {
		return this.instance
			.put<string>(`/read?groceryId=${groceryId}`)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}
}
