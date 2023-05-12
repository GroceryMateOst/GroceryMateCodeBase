import { useEffect, useState } from 'react';
import ShoppingService from '../services/shopping-service';
import { GroceryRequestDetailModel } from '../models/GroceryRequestModel';
import Spinner from '../components/General/LoadingSpinner';
import PublishedShoppingItem from '../components/Dashboard/PublishedShoppingItem';
import { Text } from '../localization/TextsDE';

const PublishedShoppings = () => {
	const [publishedRequests, setPublishedRequests] = useState<
		GroceryRequestDetailModel[]
	>([]);
	const [isLoading, setIsLoading] = useState(false);

	const markMessageAsRead = (item: GroceryRequestDetailModel) => {
		item.unreadMessages = 0;
		setPublishedRequests([
			...publishedRequests.filter(
				(shopping) => shopping.groceryRequestId !== item.groceryRequestId
			),
			item,
		]);
	};

	const publishedShoppings = publishedRequests
		.filter((item) => item.requestState === 'Published')
		.map((item) => (
			<PublishedShoppingItem
				key={item.groceryRequestId}
				item={item}
				markMessageAsRead={markMessageAsRead}
			/>
		));

	const acceptedShoppings = publishedRequests
		.filter((item) => item.requestState === 'Accepted')
		.map((item) => (
			<PublishedShoppingItem
				key={item.groceryRequestId}
				item={item}
				markMessageAsRead={markMessageAsRead}
			/>
		));

	const fulfilledIShoppings = publishedRequests
		.filter((item) => item.requestState === 'Fulfilled')
		.map((item) => (
			<PublishedShoppingItem
				key={item.groceryRequestId}
				item={item}
				markMessageAsRead={markMessageAsRead}
			/>
		));

	const getPublishedShoppings = async (shoppingService: ShoppingService) => {
		setIsLoading(true);
		try {
			const response = await shoppingService.getAllClientShoppings();
			setPublishedRequests(response);
		} finally {
			setIsLoading(false);
		}
	};
	useEffect(() => {
		const shoppingService = new ShoppingService();
		// eslint-disable-next-line @typescript-eslint/no-floating-promises
		getPublishedShoppings(shoppingService);
	}, []);

	return (
		<div className="px-10 lg:px-20">
			<div>
				<h2>{Text.publishedShoppingsTitle}</h2>
			</div>
			<div>
				{isLoading ? (
					<div>
						<Spinner />
					</div>
				) : (
					<div>
						<div>
							<h3>{Text.publishedShoppingsOppen}</h3>
							{publishedShoppings.length > 0 ? (
								publishedShoppings
							) : (
								<p>{Text.publishedShoppingsNoOppen}</p>
							)}
						</div>
						<div>
							<h3>{Text.publishedShoppingsAccepted}</h3>
							{acceptedShoppings.length > 0 ? (
								acceptedShoppings
							) : (
								<p>{Text.publishedShoppingsNoAccepted}</p>
							)}
						</div>
						<div>
							<h3>{Text.publishedShoppingsClose}</h3>
							{fulfilledIShoppings.length > 0 ? (
								fulfilledIShoppings
							) : (
								<p>{Text.publishedShoppingsNoClose}</p>
							)}
						</div>
					</div>
				)}
			</div>
		</div>
	);
};

export default PublishedShoppings;
