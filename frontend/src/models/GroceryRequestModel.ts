import { UserModelComplete } from './UserModel';
export interface GroceryRequestModel {
	preferredStore: string;
	requestState: string;
	fromDate: string;
	toDate: string;
	note: string;
	groceryList: ShoppingItem[];
}

export interface GroceryRequestResponseModel {
	groceryRequestId: string;
	firstName: string;
	city: string;
	shoppingList: ShoppingItem[];
	fromDate: string;
	toDate: string;
	preferredStore: string;
}

export interface GroceryRequestDetailModel {
	groceryRequestId: string;
	requestState: string;
	shoppingList: ShoppingItem[];
	fromDate: string;
	toDate: string;
	preferredStore: string;
	client: UserModelComplete;
	contractor: UserModelComplete;
}

export interface ShoppingItem {
	description: string;
}

export interface PatchShopping {
	groceryRequestId: string;
	requestState: string;
}
