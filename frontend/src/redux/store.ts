import { configureStore } from '@reduxjs/toolkit';
import userReducer from './userSlice';
import shoppingReducer from './shoppingSlice';

const store = configureStore({
	reducer: {
		user: userReducer,
		shopping: shoppingReducer,
	},
	middleware: (getDefaultMiddleware) =>
		getDefaultMiddleware({
			serializableCheck: false,
		}),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
