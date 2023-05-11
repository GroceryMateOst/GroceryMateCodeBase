import {
	GroceryRequestDetailModel,
	PatchShopping,
} from '../../models/GroceryRequestModel';
import ShoppingService from '../../services/shopping-service';
import { Collapse, Tooltip, Checkbox, Modal } from 'antd';
import { InfoCircleOutlined } from '@ant-design/icons';
import PDFGenerator from './PDFGenerator';
import { Text } from '../../localization/TextsDE';
import './AcceptedShoppingItem.css';
import { Suspense, useState } from 'react';
import Map from '../Map';
import Delayed from './Dealay';

interface PublishedShoppingItemProps {
	item: GroceryRequestDetailModel;
	updateState: (item: GroceryRequestDetailModel) => void;
}

const AcceptedShoppingItem = ({
	item,
	updateState,
}: PublishedShoppingItemProps) => {
	const { Panel } = Collapse;
	const [isShoppingFinished, setIsShoppingFinished] = useState(false);
	const [isModalOpen, setIsModalOpen] = useState(false);

	const formateDate = (dateString: string) => {
		const date = new Date(dateString);
		return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
	};

	const onRequestFulfill = async (event: React.MouseEvent<HTMLElement>) => {
		event.preventDefault();
		const body: PatchShopping = {
			groceryRequestId: item.groceryRequestId,
			requestState: 'fulfilled',
		};
		try {
			const shoppingService: ShoppingService = new ShoppingService();
			await shoppingService.updateShoppingState(body);
			updateState(item);
		} catch (err) {
			console.error(err);
		}
	};

	const onCheckBoxChange = (element: any[]) => {
		setIsShoppingFinished(element.length === item.shoppingList.length);
	};

	return (
		<>
			<div className="bg-[#D9D9D9] w-[700px] mt-5 flex flex-col accepted-shopping-item">
				{' '}
				<div className="flex flex-row justify-between p-5 w-fit space-x-20">
					<div className="flex flex-col">
						<span className="font-bold">{Text.publishedShoppingItemStore}</span>
						<span>{item.preferredStore}</span>
					</div>
					<div className="flex flex-col">
						<span className="font-bold">
							{Text.publishedShoppingItemTimeRange}
						</span>
						<span>{`${formateDate(item.fromDate)} bis ${formateDate(
							item.toDate
						)}`}</span>
					</div>
					{item.requestState !== 'Published' && (
						<div
							className="flex flex-col hover:cursor-pointer hover:text-[#8fb69c]"
							onClick={() => setIsModalOpen(true)}
						>
							<span className="font-bold text-black">
								{Text.acceptedShoppingItemClient}
							</span>
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
				<div>
					<Collapse className="bg-[#D9D9D9] grocerListItem">
						<Panel
							header={Text.publishedShoppingItemShowList}
							key={1}
							className="bg-[#D9D9D9]"
						>
							<div className="flex flex-row justify-between">
								<div className="flex flex-col bg-[#D9D9D9]">
									<Checkbox.Group
										className="flex flex-col"
										onChange={(e) => onCheckBoxChange(e)}
									>
										{item.shoppingList.map((grocery, index) => (
											<Checkbox key={index} value={index}>
												{grocery.description}
											</Checkbox>
										))}
									</Checkbox.Group>
								</div>
							</div>
						</Panel>
					</Collapse>
				</div>
				<div className="self-end mt-4 flex row pb-5">
					<div className=" self-center p-3 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid hover:scale-95 mx-4 text-sm ">
						<PDFGenerator item={item} />
					</div>

					{item.requestState == 'Accepted' && (
						<button
							onClick={onRequestFulfill}
							className={`${
								!isShoppingFinished ? 'cursor-not-allowed' : 'hover:scale-95'
							} p-3 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid mx-4 text-sm`}
							disabled={!isShoppingFinished}
						>
							{Text.acceptedShoppingItemButtonDone}
							<Tooltip title={Text.acceptedShoppingItemTip}>
								<InfoCircleOutlined className="ml-4" />
							</Tooltip>
						</button>
					)}
				</div>
			</div>
			<Modal
				open={isModalOpen}
				footer={false}
				onCancel={() => setIsModalOpen(false)}
				width={550}
			>
				<Delayed>
					<Map
						zoom={20}
						latitude={47.30682745}
						longitude={9.082206350049724}
						width="500px"
						height="500px"
						address={`${item.client.address.street} ${item.client.address.houseNr} ${item.client.address.zipCode} ${item.client.address.city}`}
					/>
				</Delayed>
			</Modal>
		</>
	);
};

export default AcceptedShoppingItem;
