import { AxiosBaseService } from './axios-base.service';
import { GroceryRequestModel } from '../models/GroceryRequestModel';

export default class ShoppingService extends AxiosBaseService {
	constructor() {
		super('/Shopping');
	}

	public async createShopping(body: GroceryRequestModel): Promise<void> {
		console.log(body);
		return this.instance
			.post<GroceryRequestModel>('/groceryRequest', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getAllShoppings(): Promise<GroceryRequestModel[]> {
		return this.instance
			.get('')
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getShoppingByUser(
		userId: string
	): Promise<GroceryRequestModel[]> {
		return this.instance
			.get(`?userid=${userId}`)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}
}
