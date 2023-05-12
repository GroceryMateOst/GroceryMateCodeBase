import './GroceryListItem.css';
import {
	GroceryRequestResponseModel,
	PatchShopping,
} from '../../models/GroceryRequestModel';
import { Collapse, Tooltip } from 'antd';
import { useAppSelector } from '../../redux/hooks';
import ShoppingService from '../../services/shopping-service';
import { InfoCircleOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { Text } from '../../localization/TextsDE';

const GroceryListItem = ({
	request,
}: {
	request: GroceryRequestResponseModel;
}) => {
	const { Panel } = Collapse;
	const navigate = useNavigate();
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
		} finally {
			navigate('/accepted');
		}
	};

	const formateDate = (dateString: string) => {
		const date = new Date(dateString);
		return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
	};

	return (
		<div className="bg-secondary mt-5 max-w-[600px]">
			<div className="flex flex-row  justify-between p-5 w-fit flex-wrap">
				<div className="flex flex-col mr-20 mb-2">
					<span className="font-bold">{Text.groceryListItemGroceryOf}</span>
					<span>{request.firstName}</span>
				</div>
				<div className="flex flex-col mr-20 mb-2">
					<span className="font-bold">{Text.groceryListItemPlace}</span>
					<span>{request.city}</span>
				</div>
				<div className="flex flex-col">
					<span className="font-bold">{Text.groceryListItemTime}</span>
					<span>{`${formateDate(request.fromDate)} bis ${formateDate(
						request.toDate
					)}`}</span>
				</div>
			</div>
			<div>
				<Collapse className="bg-secondary grocerListItem">
					<Panel
						header={Text.groceryListItemShowMore}
						key={1}
						className="bg-secondary"
					>
						<div className="flex flex-row justify-between flex-wrap">
							<div className="flex flex-col bg-secondary">
								{request.shoppingList.map((item, index) => (
									<span key={index}>• {item.description}</span>
								))}
							</div>
							{isAuthenticated && (
								<button
									onClick={onRequestAccept}
									className="p-4 bg-primary border-primary shadow-none rounded-3xl border-[1px] border-solid hover:scale-95 mt-5"
								>
									{Text.groceryListItemAcceptButton}
									<Tooltip title={Text.groceryListItemToolTip}>
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
