/* eslint-disable @typescript-eslint/no-unsafe-return */
import { AxiosBaseService } from './axios-base.service';
import {
	GroceryRequestDetailModel,
	GroceryRequestModel,
	GroceryRequestResponseModel,
	PatchShopping,
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

	public async updateShoppingState(body: PatchShopping): Promise<void> {
		return this.instance
			.patch<PatchShopping>(`/groceryRequestState`, body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getAllClientShoppings(): Promise<GroceryRequestDetailModel[]> {
		return this.instance
			.get('groceryRequest/clientRequests')
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getAllContractorShoppings(): Promise<
		GroceryRequestDetailModel[]
	> {
		return this.instance
			.get('groceryRequest/contractorRequests')
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async getGroceryListsBySearchParams(
		zipCode?: number
	): Promise<GroceryRequestResponseModel[]> {
		return this.instance
			.get(`Search${zipCode ? `?zipCode=${zipCode}` : ''}`)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}
}
