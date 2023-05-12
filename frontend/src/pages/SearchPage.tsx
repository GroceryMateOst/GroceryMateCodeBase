import { SearchOutlined } from '@ant-design/icons';
import { Button, Empty, Input, Space } from 'antd';
import { useEffect, useState } from 'react';
import { GroceryRequestResponseModel } from '../models/GroceryRequestModel';
import ShoppingService from '../services/shopping-service';
import GroceryListItem from '../components/GroceryListOverView/GroceryListItem';
import Spinner from '../components/General/LoadingSpinner';
import { Text } from '../localization/TextsDE';

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
		<div className="mb-10 px-10 lg:px-20">
			<h1>{Text.searchPageTitle}</h1>
			<p>{Text.searchPageText}</p>
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
			<div className="flex flex-col items-start">
				{isLoading ? (
					<div className="m-40">
						<Spinner />
					</div>
				) : groceryRequests.length > 0 ? (
					groceryRequests.map((request, index) => (
						<div className="flex flex-col items-end" key={index}>
							{request.distance > 0 && (
								<div className="border-[#8fb69c] bg-[#8fb69c] w-fit h-fit p-2 rounded-full flex flex-col justify-center items-center relative top-10 right-[-30px]">{`${request.distance}km`}</div>
							)}
							<GroceryListItem request={request} />
						</div>
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
