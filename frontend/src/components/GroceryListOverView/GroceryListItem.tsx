import './GroceryListItem.css';
import {
	GroceryRequestResponseModel,
	PatchShopping,
} from '../../models/GroceryRequestModel';
import { Collapse, Tooltip } from 'antd';
import { useAppSelector } from '../../redux/hooks';
import ShoppingService from '../../services/shopping-service';
import { InfoCircleOutlined } from '@ant-design/icons';

const GroceryListItem = ({
	request,
}: {
	request: GroceryRequestResponseModel;
}) => {
	const { Panel } = Collapse;
	const isAuthenticated = useAppSelector((state) => state.user.isAuthenticated);

	const onRequestAccept = async (event: React.MouseEvent<HTMLElement>) => {
		event.preventDefault();
		const body: PatchShopping = {
			groceryRequestId: request.groceryRequestId,
			requestState: 'accepted',
		};
		try {
			const shoppingService: ShoppingService = new ShoppingService();
			await shoppingService.updateShoppingState(body);
		} catch {
			console.log('error');
		}
	};

	const formateDate = (dateString: string) => {
		const date = new Date(dateString);
		return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
	};

	return (
		<div className="bg-[#D9D9D9] w-[550px] mt-5">
			<div className="flex flex-row justify-between p-5 w-fit space-x-20">
				<div className="flex flex-col">
					<span className="font-bold">Einkauf von:</span>
					<span>{request.firstName}</span>
				</div>
				<div className="flex flex-col">
					<span className="font-bold">Ort:</span>
					<span>{request.city}</span>
				</div>
				<div className="flex flex-col">
					<span className="font-bold">Einkaufs Zeitraum:</span>
					<span>{`${formateDate(request.fromDate)} bis ${formateDate(
						request.toDate
					)}`}</span>
				</div>
			</div>
			<div>
				<Collapse className="bg-[#D9D9D9] grocerListItem">
					<Panel header="Mehr Anzeigen" key={1} className="bg-[#D9D9D9]">
						<div className="flex flex-row justify-between">
							<div className="flex flex-col bg-[#D9D9D9]">
								{request.shoppingList.map((item, index) => (
									<span key={index}>• {item.description}</span>
								))}
							</div>
							{isAuthenticated && (
								<button
									onClick={onRequestAccept}
									className="p-4 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid hover:scale-95"
								>
									Anfrage annehmen
									<Tooltip title="Möchtest du diesen Einkauf übernehmen? Dann klicke auf diesen Button">
										<InfoCircleOutlined className="ml-4" />
									</Tooltip>
								</button>
							)}
						</div>
					</Panel>
				</Collapse>
			</div>
		</div>
	);
};

export default GroceryListItem;
