export interface ShoppingModel {
	userId: string;
	contractorId: string;
	fromDate: string;
	toDate: string;
	note: string;
	shoppingList: ShoppingList;
}

export interface ShoppingList {
	preferedStore: string;
	items: ShoppingItem[];
}

export interface ShoppingItem {
	description: string;
}
