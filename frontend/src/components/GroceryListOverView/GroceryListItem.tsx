import './GroceryListItem.css';
import { GroceryRequestResponseModel } from '../../models/GroceryRequestModel';
import { Collapse } from 'antd';

const GroceryListItem = ({
	request,
}: {
	request: GroceryRequestResponseModel;
}) => {
	const { Panel } = Collapse;

	const formateDate = (dateString: string) => {
		const date = new Date(dateString);
		return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
	};

	return (
		<div className="bg-[#D9D9D9] max-w-[500px] mt-5">
			<div className="flex flex-row justify-between p-5 w-fit space-x-20">
				<div className="flex flex-col">
					<span className="font-bold">Einnkauf von:</span>
					<span>{request.firstName}</span>
				</div>
				<div className="flex flex-col">
					<span className="font-bold">Ort:</span>
					<span>{request.city}</span>
				</div>
				<div className="flex flex-col">
					<span className="font-bold">Einkaufs Zeitraum</span>
					<span>{`${formateDate(request.fromDate)} bis ${formateDate(
						request.toDate
					)}`}</span>
				</div>
			</div>
			<div>
				<Collapse className="bg-[#D9D9D9] grocerListItem">
					<Panel header="Mehr Anzeigen" key={1} className="bg-[#D9D9D9]">
						<div className="flex flex-col bg-[#D9D9D9]">
							{request.shoppingList.map((item, index) => (
								<span key={index}>• {item.description}</span>
							))}
						</div>
					</Panel>
				</Collapse>
			</div>
		</div>
	);
};

export default GroceryListItem;
