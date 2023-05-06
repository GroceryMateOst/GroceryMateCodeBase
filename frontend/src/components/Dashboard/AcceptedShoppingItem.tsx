import {
	GroceryRequestDetailModel,
	PatchShopping,
} from '../../models/GroceryRequestModel';
import ShoppingService from '../../services/shopping-service';
import { Collapse, Tooltip } from 'antd';
import { InfoCircleOutlined } from '@ant-design/icons';
import PDFGenerator from './PDFGenerator';

interface PublishedShoppingItemProps {
	item: GroceryRequestDetailModel;
	updateState: (item: GroceryRequestDetailModel) => void;
}

const AcceptedShoppingItem = ({
	item,
	updateState,
}: PublishedShoppingItemProps) => {
	const { Panel } = Collapse;

	const formateDate = (dateString: string) => {
		const date = new Date(dateString);
		return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
	};

	const onRequestFulfill = async (event: React.MouseEvent<HTMLElement>) => {
		const body: PatchShopping = {
			groceryRequestId: item.groceryRequestId,
			requestState: 'fulfilled',
		};
		try {
			const shoppingService: ShoppingService = new ShoppingService();
			await shoppingService.updateShoppingState(body);
			updateState(item);
		} catch {
			console.log('error');
		}
	};

	const onPDFPrint = (event: React.MouseEvent<HTMLElement>) => {
		event.preventDefault();
	};

	return (
		<div className="bg-[#D9D9D9] w-[700px] mt-5 flex flex-col">
			{' '}
			<div className="flex flex-row justify-between p-5 w-fit space-x-20">
				<div className="flex flex-col">
					<span className="font-bold">Gewünschter Supermarkt:</span>
					<span>{item.preferredStore}</span>
				</div>
				<div className="flex flex-col">
					<span className="font-bold">Einkaufs Zeitraum:</span>
					<span>{`${formateDate(item.fromDate)} bis ${formateDate(
						item.toDate
					)}`}</span>
				</div>
				{item.requestState !== 'Published' && (
					<div className="flex flex-col">
						<span className="font-bold">Auftraggeber:</span>
						<div className="flex flex-col">
							<span>
								{item.client.user.firstName} {item.client.user.secondName}
							</span>
							<span>
								{item.client.address.street} {item.client.address.houseNr}
							</span>
							<span>
								{item.client.address.zipCode} {item.client.address.city}
							</span>
						</div>
					</div>
				)}
			</div>
			<div className="self-end mt-4">
				<div className="py-2 px-4 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid hover:scale-95 mx-4">
					<PDFGenerator item={item} />
				</div>
				{item.requestState == 'Accepted' && (
					<button
						onClick={onRequestFulfill}
						className="p-4 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid hover:scale-95 mx-4"
					>
						Einkauf abschliessen
						<Tooltip title="Hast du den Auftrag erledigt? Dann klicke bitte diesen Button">
							<InfoCircleOutlined className="ml-4" />
						</Tooltip>
					</button>
				)}
			</div>
			<div>
				<Collapse className="bg-[#D9D9D9] grocerListItem">
					<Panel
						header="Einkaufsliste anzeigen"
						key={1}
						className="bg-[#D9D9D9]"
					>
						<div className="flex flex-row justify-between">
							<div className="flex flex-col bg-[#D9D9D9]">
								{item.shoppingList.map((grocery, index) => (
									<span key={index}>• {grocery.description}</span>
								))}
							</div>
						</div>
					</Panel>
				</Collapse>
			</div>
		</div>
	);
};

export default AcceptedShoppingItem;
