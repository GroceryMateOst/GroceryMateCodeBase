import {
	GroceryRequestDetailModel,
	PatchShopping,
} from '../../models/GroceryRequestModel';
import ShoppingService from '../../services/shopping-service';
import { Collapse, Tooltip, Checkbox, Modal } from 'antd';
import { InfoCircleOutlined } from '@ant-design/icons';
import PDFGenerator from './PDFGenerator';
import ChatOverlay from '../Chat/ChatOverlay';
import { Text } from '../../localization/TextsDE';
import './AcceptedShoppingItem.css';
import { useEffect, useState } from 'react';
import Map from '../Map';
import Delayed from './Dealay';
import { CheckboxValueType } from 'antd/es/checkbox/Group';

interface PublishedShoppingItemProps {
	item: GroceryRequestDetailModel;
	updateState: (item: GroceryRequestDetailModel) => void;
	markMessageAsRead: (item: GroceryRequestDetailModel) => void;
}

const AcceptedShoppingItem = ({
	item,
	updateState,
	markMessageAsRead,
}: PublishedShoppingItemProps) => {
	const { Panel } = Collapse;
	const [isShoppingFinished, setIsShoppingFinished] = useState(false);
	const [isModalOpen, setIsModalOpen] = useState(false);
	const [windowWidth, setWindowWidth] = useState(window.innerWidth);

	useEffect(() => {
		const handleWindowResize = () => {
			setWindowWidth(window.innerWidth);
		};
		window.addEventListener('resize', handleWindowResize);

		return () => {
			window.removeEventListener('resize', handleWindowResize);
		};
	});

	const calculateSize = () => {
		return windowWidth < 600
			? windowWidth < 375
				? '200px'
				: '300px'
			: '500px';
	};

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

	const onCheckBoxChange = (element: CheckboxValueType[]) => {
		setIsShoppingFinished(element.length === item.shoppingList.length);
	};

	return (
		<>
			<div className="bg-secondary max-w-[700px] mt-5 flex flex-col accepted-shopping-item">
				{' '}
				<div className="flex flex-row justify-between p-5 w-fit flex-wrap">
					<div className="flex flex-col mr-20 mb-2">
						<span className="font-bold">{Text.publishedShoppingItemStore}</span>
						<span>{item.preferredStore}</span>
					</div>
					<div className="flex flex-col mr-20 mb-2">
						<span className="font-bold">
							{Text.publishedShoppingItemTimeRange}
						</span>
						<span>{`${formateDate(item.fromDate)} bis ${formateDate(
							item.toDate
						)}`}</span>
					</div>
					{item.requestState !== 'Published' && (
						<div
							className="flex flex-col hover:cursor-pointer hover:text-primary"
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
								{item.client.user.residencyDetails && (
									<div className="flex flex-col text-black">
										<span className="font-bold">
											{Text.acceptedShoppingItem}
										</span>
										<span>{item.client.user.residencyDetails}</span>
									</div>
								)}
							</div>
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
									{item.requestState !== 'Fulfilled' ? (
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
									) : (
										<div>
											{item.shoppingList.map((grocery, index) => (
												<p key={index} className="my-1">
													•
													<span className="line-through">
														{grocery.description}
													</span>
												</p>
											))}
										</div>
									)}
								</div>
							</div>
							<div className="flex flex-col mt-3">
								<span className="font-bold">
									{Text.acceptedShoppingItemNote.replace(
										'{}',
										item.client.user.firstName
									)}
								</span>
								<span>{item.note}</span>
							</div>
						</Panel>
					</Collapse>
				</div>
				<div className="self-end mt-4 flex flex-wrap">
					{item.requestState !== 'Fulfilled' && (
						<div className=" self-center p-3 bg-primary border-primary shadow-none rounded-3xl border-[1px] border-solid hover:scale-95 mx-4 text-sm mb-5">
							<PDFGenerator item={item} />
						</div>
					)}
					{item.requestState == 'Accepted' && (
						<button
							onClick={onRequestFulfill}
							className={`${
								!isShoppingFinished ? 'cursor-not-allowed' : 'hover:scale-95'
							} p-3 bg-primary border-primary shadow-none rounded-3xl border-[1px] border-solid mx-4 text-sm h-12 mb-5`}
							disabled={!isShoppingFinished}
						>
							{Text.acceptedShoppingItemButtonDone}
							<Tooltip title={Text.acceptedShoppingItemTip}>
								<InfoCircleOutlined className="ml-4" />
							</Tooltip>
						</button>
					)}
					<div>
						<div className="p-3 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid hover:scale-95 mx-4 text-sm">
							<ChatOverlay item={item} markMessageAsRead={markMessageAsRead} />
						</div>
					</div>
				</div>
			</div>
			<Modal
				open={isModalOpen}
				footer={false}
				onCancel={() => setIsModalOpen(false)}
				width={windowWidth < 600 ? (windowWidth < 375 ? 250 : 350) : 550}
			>
				<Delayed placeHolderSize={calculateSize()}>
					<Map
						zoom={20}
						latitude={item.client.address.latitude}
						longitude={item.client.address.longitude}
						width={calculateSize()}
						height={calculateSize()}
						address={`${item.client.address.street} ${item.client.address.houseNr} ${item.client.address.zipCode} ${item.client.address.city}`}
					/>
				</Delayed>
			</Modal>
		</>
	);
};

export default AcceptedShoppingItem;
