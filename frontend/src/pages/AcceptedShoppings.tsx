import { useEffect, useState } from 'react';
import ShoppingService from '../services/shopping-service';
import { GroceryRequestDetailModel } from '../models/GroceryRequestModel';
import Spinner from '../components/General/LoadingSpinner';
import AcceptedShoppingItem from '../components/Dashboard/AcceptedShoppingItem';

const AcceptedShoppings = () => {
	const [acceptedRequests, setAcceptedRequests] = useState<
		GroceryRequestDetailModel[]
	>([]);
	const [isLoading, setIsLoading] = useState(false);

	const updateShoppingState = (item: GroceryRequestDetailModel) => {
		item.requestState = 'Fulfilled';
		setAcceptedRequests([
			...acceptedRequests.filter(
				(shopping) => shopping.groceryRequestId !== item.groceryRequestId
			),
			item,
		]);
	};

	const acceptedShoppings = acceptedRequests
		.filter((item) => item.requestState === 'Accepted')
		.map((item) => (
			<AcceptedShoppingItem
				key={item.groceryRequestId}
				item={item}
				updateState={updateShoppingState}
			/>
		));

	const fulfilledIShoppings = acceptedRequests
		.filter((item) => item.requestState === 'Fulfilled')
		.map((item) => (
			<AcceptedShoppingItem
				key={item.groceryRequestId}
				item={item}
				updateState={updateShoppingState}
			/>
		));

	const getAcceptedShoppings = async (shoppingService: ShoppingService) => {
		setIsLoading(true);
		try {
			const response = await shoppingService.getAllContractorShoppings();
			setAcceptedRequests(response);
		} finally {
			setIsLoading(false);
		}
	};

	useEffect(() => {
		const shoppingService = new ShoppingService();
		// eslint-disable-next-line @typescript-eslint/no-floating-promises
		getAcceptedShoppings(shoppingService);
	}, []);

	return (
		<div className="px-20">
			<div>
				<h2>Meine akzeptierten Einkaufsanfragen</h2>
			</div>
			<div>
				{isLoading ? (
					<div className="mt-40">
						<Spinner />
					</div>
				) : (
					<div>
						<div>
							<h3>Angenommene Aufträge</h3>
							{acceptedShoppings.length > 0 ? (
								acceptedShoppings
							) : (
								<p>Aktuell keine offenen Aufträge vorhanden</p>
							)}
						</div>
						<div>
							<h3>Abgeschlossene Aufträge</h3>
							{fulfilledIShoppings.length > 0 ? (
								fulfilledIShoppings
							) : (
								<p>Keine abgeschlossene Aufträge vorhanden</p>
							)}
						</div>
					</div>
				)}
			</div>
		</div>
	);
};

export default AcceptedShoppings;
