import { useEffect, useState } from 'react';
import { GroceryRequestResponseModel } from '../../models/GroceryRequestModel';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import ShoppingService from '../../services/shopping-service';
import { setIsLoading } from '../../redux/userSlice';
import GroceryListItem from './GroceryListItem';
import { Link } from 'react-router-dom';
import { RightOutlined } from '@ant-design/icons';
import Spinner from '../General/LoadingSpinner';
import { Text } from '../../localization/TextsDE';

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
				<h2>{Text.groceryListOverViewTitle}</h2>
			</div>
			<div>
				{isLoading ? (
					<div className="mt-10">
						<Spinner />
					</div>
				) : (
					groceryRequests.map((request, index) => (
						<GroceryListItem request={request} key={index} />
					))
				)}
			</div>
			<div className="mt-4 font-bold">
				<Link to="/search" className="!text-primary hover:!text-black">
					{Text.groceryListOverViewToAll}
					<RightOutlined />
				</Link>
			</div>
		</>
	);
};

export default GroceryListOverView;
