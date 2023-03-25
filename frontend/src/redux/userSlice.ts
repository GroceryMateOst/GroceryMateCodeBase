import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { UserModel } from '../models/UserModel';
import type { RootState } from './store';

// Define a type for the slice state
export interface UserState {
	user: UserModel;
	token: string;
	isAuthenticated: boolean;
	isLoading: boolean;
}
// Define the initial state using that type
const initialUser: UserModel = {
	emailaddress: '',
	password: '',
	secondname: '',
	firstname: '',
};

const initialState: UserState = {
	user: initialUser,
	token: '',
	isAuthenticated: false,
	isLoading: false,
};

export const userSlice = createSlice({
	name: 'user',
	initialState,
	reducers: {
		addUserToState: (state, action: PayloadAction<UserState>) => {
			state.user = action.payload.user;
			state.token = action.payload.token;
		},
		removeUserFromState: (state) => {
			state.user = initialUser;
			state.isAuthenticated = false;
		},
		setIsAuthenticated: (state, action: PayloadAction<boolean>) => {
			state.isAuthenticated = action.payload;
		},
		setIsLoading: (state, action: PayloadAction<boolean>) => {
			state.isLoading = action.payload;
		}
	},
});

export const {
	addUserToState,
	removeUserFromState,
	setIsAuthenticated,
	setIsLoading,
} = userSlice.actions;

// Other code such as selectors can use the imported `RootState` type
//export const selectCount = (state: RootState) => state.counter.value;

export default userSlice.reducer;
