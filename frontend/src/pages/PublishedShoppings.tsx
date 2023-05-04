import { useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { setIsLoading } from '../redux/userSlice';
import ShoppingService from '../services/shopping-service';
import { GroceryRequestDetailModel } from '../models/GroceryRequestModel';

const PublishedShoppings = () => {
	const [publishedRequests, setPublishedRequests] = useState<
		GroceryRequestDetailModel[]
	>([]);
	const dispatch = useAppDispatch();
	const isLoading = useAppSelector((state) => state.user.isLoading);

	const getPublishedShoppings = async (shoppingService: ShoppingService) => {
		dispatch(setIsLoading(true));
		try {
			const response = await shoppingService.getAllClientShoppings();
			console.log(response);
			setPublishedRequests(response);
		} finally {
			dispatch(setIsLoading(false));
		}
	};
	useEffect(() => {
		const shoppingService = new ShoppingService();
		// eslint-disable-next-line @typescript-eslint/no-floating-promises
		getPublishedShoppings(shoppingService);
	}, []);

	return (
		<>
			<div>
				<h2>Meine publizierten Einkaufsanfragen</h2>
			</div>
		</>
	);
};

export default PublishedShoppings;
