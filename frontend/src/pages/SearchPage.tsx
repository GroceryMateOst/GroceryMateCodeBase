import { SearchOutlined } from '@ant-design/icons';
import { Button, Input, Space } from 'antd';
import { useEffect, useState } from 'react';
import { GroceryRequestResponseModel } from '../models/GroceryRequestModel';
import ShoppingService from '../services/shopping-service';

const SearchPage = () => {
	const [plz, setPlz] = useState<number>();
	const [groceryRequests, setgroceryRequests] = useState<
		GroceryRequestResponseModel[]
	>([]);

	const onSearchClick = () => {
		console.log(plz);
	};

	useEffect(() => {}, []);

	const getFilteredRequests = async () => {
		const shoppingService = new ShoppingService();
		// const response = shoppingService.getGroceryListsBySearchParams('');
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
		</div>
	);
};

export default SearchPage;
