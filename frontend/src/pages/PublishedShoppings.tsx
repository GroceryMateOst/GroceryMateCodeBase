import { useEffect, useState } from 'react';
import ShoppingService from '../services/shopping-service';
import { GroceryRequestDetailModel } from '../models/GroceryRequestModel';
import Spinner from '../components/General/LoadingSpinner';
import PublishedShoppingItem from '../components/Dashboard/PublishedShoppingItem';

const PublishedShoppings = () => {
	const [publishedRequests, setPublishedRequests] = useState<
		GroceryRequestDetailModel[]
	>([]);
	const [isLoading, setIsLoading] = useState(false);

	const publishedShoppings = publishedRequests
		.filter((item) => item.requestState === 'Published')
		.map((item, index) => <PublishedShoppingItem key={index} item={item} />);

	const acceptedShoppings = publishedRequests
		.filter((item) => item.requestState === 'Accepted')
		.map((item, index) => <PublishedShoppingItem key={index} item={item} />);

	const fulfilledIShoppings = publishedRequests
		.filter((item) => item.requestState === 'Fulfilled')
		.map((item, index) => <PublishedShoppingItem key={index} item={item} />);

	const getPublishedShoppings = async (shoppingService: ShoppingService) => {
		setIsLoading(true);
		try {
			const response = await shoppingService.getAllClientShoppings();
			console.log(response);
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
		<div className="px-20">
			<div>
				<h2>Meine publizierten Einkaufsanfragen</h2>
			</div>
			<div>
				{isLoading ? (
					<div>
						<Spinner />
					</div>
				) : (
					<div>
						<div>
							<h3>Offene Inserate</h3>
							{publishedShoppings.length > 0 ? (
								publishedShoppings
							) : (
								<p>Aktuell keine offenen Inserate vorhanden</p>
							)}
						</div>
						<div>
							<h3>Akzeptierte Inserate</h3>
							{acceptedShoppings.length > 0 ? (
								acceptedShoppings
							) : (
								<p>Aktuell keine offenen Inserate vorhanden</p>
							)}
						</div>
						<div>
							<h3>Abgeschlossene Inserate</h3>
							{fulfilledIShoppings.length > 0 ? (
								fulfilledIShoppings
							) : (
								<p>Keine abgeschlossene Inserate vorhanden</p>
							)}
						</div>
					</div>
				)}
			</div>
		</div>
	);
};

export default PublishedShoppings;
