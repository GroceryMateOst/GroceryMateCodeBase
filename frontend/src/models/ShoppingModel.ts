export interface ShoppingModel {
	userId: string;
	contractorId: string;
	fromDate: Date;
	toDate: Date;
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
