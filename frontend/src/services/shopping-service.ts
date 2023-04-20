import { AxiosBaseService } from './axios-base.service';
import { ShoppingModel } from '../models/ShoppingModel';

export default class ShoppingService extends AxiosBaseService {
	constructor() {
		super('/Shopping');
	}

	public async createShopping(body: ShoppingModel): Promise<void> {
		return this.instance
			.post<ShoppingModel>('', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getAllShoppings(): Promise<ShoppingModel[]> {
		return this.instance
			.get('')
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getShoppingByUser(userId: string): Promise<ShoppingModel[]> {
		return this.instance
			.get(`?userid=${userId}`)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}
}
