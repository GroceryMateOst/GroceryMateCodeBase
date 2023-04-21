import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
	GroceryRequestModel,
	ShoppingItem,
} from '../models/GroceryRequestModel';

export interface ShoppingState {
	allShoppings: GroceryRequestModel[];
	currentShopping: GroceryRequestModel;
}

const initialShopping: GroceryRequestModel = {
	preferredStore: ' ',
	requestState: ' ',
	fromDate: ' ',
	toDate: ' ',
	note: ' ',
	groceryList: [],
};

const initialState: ShoppingState = {
	allShoppings: [],
	currentShopping: initialShopping,
};

export const shoppingSlice = createSlice({
	name: 'shopping',
	initialState,
	reducers: {
		addCurrentShopping: (state, action: PayloadAction<GroceryRequestModel>) => {
			state.currentShopping = action.payload;
		},
		addAllShoppings: (state, action: PayloadAction<GroceryRequestModel[]>) => {
			state.allShoppings = action.payload;
		},
		changeCurrentShoppingItems: (
			state,
			action: PayloadAction<ShoppingItem[]>
		) => {
			state.currentShopping.groceryList = action.payload;
		},
	},
});

export const {
	addCurrentShopping,
	addAllShoppings,
	changeCurrentShoppingItems,
} = shoppingSlice.actions;

export default shoppingSlice.reducer;
