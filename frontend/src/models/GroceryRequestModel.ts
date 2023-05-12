import { UserModelGrocery } from './UserModel';
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
	distance: number;
}

export interface GroceryRequestDetailModel {
	groceryRequestId: string;
	unreadMessages: number;
	requestState: string;
	shoppingList: ShoppingItem[];
	fromDate: string;
	toDate: string;
	preferredStore: string;
	client: UserModelGrocery;
	contractor: UserModelGrocery;
}

export interface ShoppingItem {
	description: string;
}

export interface PatchShopping {
	groceryRequestId: string;
	requestState: string;
}
