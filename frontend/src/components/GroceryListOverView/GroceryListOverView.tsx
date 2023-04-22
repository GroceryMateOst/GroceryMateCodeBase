import { useEffect, useState } from 'react';
import { GroceryRequestResponseModel } from '../../models/GroceryRequestModel';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import ShoppingService from '../../services/shopping-service';
import { setIsLoading } from '../../redux/userSlice';
import GroceryListItem from './GroceryListItem';

const GroceryListOverView = () => {
	const [groceryRequests, setGroceryRequests] = useState<
		GroceryRequestResponseModel[]
	>([]);
	const dispatch = useAppDispatch();
	const isLoading = useAppSelector((state) => state.user.isLoading);

	const getGroceryRequests = async (shoppingService: ShoppingService) => {
		dispatch(setIsLoading(true));
		try {
			const response = await shoppingService.getAllShoppings();
			setGroceryRequests(response);
		} finally {
			dispatch(setIsLoading(false));
		}
	};

	useEffect(() => {
		const shoppingService = new ShoppingService();
		// eslint-disable-next-line @typescript-eslint/no-floating-promises
		getGroceryRequests(shoppingService);
	}, []);

	return (
		<>
			<div>
				<h2>Einkaufslisten</h2>
			</div>
			<div>
				{isLoading
					? null
					: groceryRequests.map((request, index) => (
							<GroceryListItem request={request} key={index} />
					  ))}
			</div>
		</>
	);
};

export default GroceryListOverView;
