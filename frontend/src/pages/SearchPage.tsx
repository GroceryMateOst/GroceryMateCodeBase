import { SearchOutlined } from '@ant-design/icons';
import { Button, Empty, Input, Space } from 'antd';
import { useEffect, useState } from 'react';
import { GroceryRequestResponseModel } from '../models/GroceryRequestModel';
import ShoppingService from '../services/shopping-service';
import GroceryListItem from '../components/GroceryListOverView/GroceryListItem';

const SearchPage = () => {
	const [plz, setPlz] = useState<number>();
	const [groceryRequests, setGroceryRequests] = useState<
		GroceryRequestResponseModel[]
	>([]);
	const [isLoading, setIsLoading] = useState(false);

	const getFilteredRequests = async (zipCode?: number) => {
		setIsLoading(true);
		const shoppingService = new ShoppingService();
		try {
			const response = await shoppingService.getGroceryListsBySearchParams(
				zipCode
			);
			setGroceryRequests(response);
		} finally {
			setIsLoading(false);
		}
	};

	useEffect(() => {
		// eslint-disable-next-line @typescript-eslint/no-floating-promises
		getFilteredRequests();
	}, []);

	const onSearchClick = async () => {
		await getFilteredRequests(plz);
	};

	return (
		<div className="px-20 mb-10">
			<h1>Suche</h1>
			<p>Bitte gib die Postleitzahl ein, um die Einkaufslisten zu filtern.</p>
			<Space.Compact>
				<Input
					type="number"
					onBlur={(e) =>
						setPlz(e.target.value ? parseInt(e.target.value) : undefined)
					}
				/>
				<Button onClick={onSearchClick} disabled={plz === undefined}>
					<SearchOutlined />
				</Button>
			</Space.Compact>
			<div>
				{isLoading ? null : groceryRequests.length > 0 ? (
					groceryRequests.map((request, index) => (
						<GroceryListItem request={request} key={index} />
					))
				) : (
					<Empty
						className="w-fit"
						description="Leider ergab deine Suche kein Ergebnis."
						image={Empty.PRESENTED_IMAGE_SIMPLE}
					/>
				)}
			</div>
		</div>
	);
};

export default SearchPage;
