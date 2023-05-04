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
			.put<PatchShopping>(
				`/groceryRequestState?requestId=${body.groceryRequestId}&state=${body.requestState}`
			)
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
}
