import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
	GroceryRequestDetailModel,
	GroceryRequestModel,
	ShoppingItem,
} from '../models/GroceryRequestModel';
import { UserModelComplete } from '../models/UserModel';

export interface ShoppingState {
	allShoppings: GroceryRequestModel[];
	currentShopping: GroceryRequestModel;
	acceptedShoppings: GroceryRequestDetailModel[];
	publishedShoppings: GroceryRequestDetailModel[];
}

const initialUser: UserModelComplete = {
	emailAddress: ' ',
	firstName: ' ',
	secondName: ' ',
	street: ' ',
	houseNr: ' ',
	zipCode: ' ',
	city: ' ',
	state: ' ',
	residencyDetails: ' ',
};

const initialDetailShopping: GroceryRequestDetailModel = {
	groceryRequestId: '',
	preferredStore: ' ',
	requestState: ' ',
	fromDate: ' ',
	toDate: ' ',
	shoppingList: [],
	contractor: initialUser,
	client: initialUser,
};

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
	acceptedShoppings: [],
	publishedShoppings: [],
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
		addPublishedShoppings: (
			state,
			action: PayloadAction<GroceryRequestDetailModel[]>
		) => {
			state.publishedShoppings = action.payload;
		},
		addAcceptedShoppings: (
			state,
			action: PayloadAction<GroceryRequestDetailModel[]>
		) => {
			state.acceptedShoppings = action.payload;
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
	addAcceptedShoppings,
	addPublishedShoppings,
} = shoppingSlice.actions;

export default shoppingSlice.reducer;
