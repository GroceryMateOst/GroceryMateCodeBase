import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ShoppingModel, ShoppingItem } from '../models/ShoppingModel';

export interface ShoppingState {
	allShoppings: ShoppingModel[];
	currentShopping: ShoppingModel;
}

const initialShopping: ShoppingModel = {
	userId: '',
	contractorId: '',
	fromDate: '',
	toDate: '',
	note: '',
	shoppingList: {
		preferedStore: '',
		items: [
			{
				description: '',
			},
		],
	},
};

const initialState: ShoppingState = {
	allShoppings: [],
	currentShopping: initialShopping,
};

export const shoppingSlice = createSlice({
	name: 'shopping',
	initialState,
	reducers: {
		addCurrentShopping: (state, action: PayloadAction<ShoppingModel>) => {
			state.currentShopping = action.payload;
		},
		addAllShoppings: (state, action: PayloadAction<ShoppingModel[]>) => {
			state.allShoppings = action.payload;
		},
		changeCurrentShoppingItems: (
			state,
			action: PayloadAction<ShoppingItem[]>
		) => {
			state.currentShopping.shoppingList.items = action.payload;
		},
	},
});

export const {
	addCurrentShopping,
	addAllShoppings,
	changeCurrentShoppingItems,
} = shoppingSlice.actions;

export default shoppingSlice.reducer;
