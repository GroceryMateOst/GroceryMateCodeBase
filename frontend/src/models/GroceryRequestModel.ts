export interface GroceryRequestModel {
	preferredStore: string;
	requestState: string;
	fromDate: string;
	toDate: string;
	note: string;
	groceryList: ShoppingItem[];
}

export interface ShoppingList {
	items: ShoppingItem[];
}

export interface ShoppingItem {
	description: string;
}
