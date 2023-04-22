export interface GroceryRequestModel {
	preferredStore: string;
	requestState: string;
	fromDate: string;
	toDate: string;
	note: string;
	groceryList: ShoppingItem[];
}

export interface GroceryRequestResponseModel {
	firstName: string;
	city: string;
	shoppingList: ShoppingItem[];
	fromDate: string;
	toDate: string;
	preferredStore: string;
}

export interface ShoppingItem {
	description: string;
}
