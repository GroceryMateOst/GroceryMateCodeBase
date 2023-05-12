import { Text } from '../../localization/TextsDE';
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
		<div className="bg-secondary max-w-[600px] mt-5">
			{' '}
			<div className="flex flex-row justify-between p-5 w-fit flex-wrap">
				<div className="flex flex-col mr-20 mb-2">
					<span className="font-bold">{Text.publishedShoppingItemStore}</span>
					<span>{item.preferredStore}</span>
				</div>
				{item.requestState !== 'Fulfilled' && (
					<div className="flex flex-col mr-20 mb-2">
						<span className="font-bold">
							{Text.publishedShoppingItemTimeRange}
						</span>
						<span>{`${formateDate(item.fromDate)} bis ${formateDate(
							item.toDate
						)}`}</span>
					</div>
				)}
				{item.requestState !== 'Published' && (
					<div className="flex flex-col mb-2">
						<span className="font-bold">
							{item.requestState === 'Fulfilled'
								? Text.publishedShoppingDoneBy
								: Text.publishedShoppingItemAcceptedBy}
						</span>
						<span>
							{item.contractor.user.firstName} {item.contractor.user.secondName}
						</span>
					</div>
				)}
			</div>
			<div>
				<Collapse className="bg-secondary grocerListItem">
					<Panel
						header={Text.publishedShoppingItemShowList}
						key={1}
						className="bg-secondary"
					>
						<div className="flex flex-row justify-between">
							<div className="flex flex-col bg-secondary">
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
