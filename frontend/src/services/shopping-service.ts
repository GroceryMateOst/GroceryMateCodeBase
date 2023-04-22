import { AxiosBaseService } from './axios-base.service';
import {
	GroceryRequestModel,
	GroceryRequestResponseModel,
} from '../models/GroceryRequestModel';

export default class ShoppingService extends AxiosBaseService {
	constructor() {
		super('/Shopping');
	}

	public async createShopping(body: GroceryRequestModel): Promise<void> {
		return this.instance
			.post<GroceryRequestModel>('/groceryRequest', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getAllShoppings(): Promise<GroceryRequestResponseModel[]> {
		return this.instance
			.get('groceryRequest')
			.then(this.responseBody)
			.catch(this.errorHandling);
	}
}
