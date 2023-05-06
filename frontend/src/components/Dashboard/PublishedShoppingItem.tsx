import { GroceryRequestDetailModel } from '../../models/GroceryRequestModel';
import { Collapse } from 'antd';

interface PublishedShoppingItemProps {
	item: GroceryRequestDetailModel;
}

const PublishedShoppingItem = ({ item }: PublishedShoppingItemProps) => {
	const { Panel } = Collapse;

	const formateDate = (dateString: string) => {
		const date = new Date(dateString);
		return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
	};
	return (
		<div className="bg-[#D9D9D9] w-[550px] mt-5">
			{' '}
			<div className="flex flex-row justify-between p-5 w-fit space-x-20">
				<div className="flex flex-col">
					<span className="font-bold">Gewünschter Supermarkt:</span>
					<span>{item.preferredStore}</span>
				</div>
				{item.requestState !== 'Fulfilled' && (
					<div className="flex flex-col">
						<span className="font-bold">Einkaufs Zeitraum:</span>
						<span>{`${formateDate(item.fromDate)} bis ${formateDate(
							item.toDate
						)}`}</span>
					</div>
				)}
				{item.requestState !== 'Published' && (
					<div className="flex flex-col">
						<span className="font-bold">
							{item.requestState === 'Fulfilled'
								? 'Erledigt druch:'
								: 'Angenommen von:'}
						</span>
						<span>
							{item.contractor.user.firstName} {item.contractor.user.secondName}
						</span>
					</div>
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

export default PublishedShoppingItem;
