import { Button, Form, Input, DatePicker } from 'antd';
import { DeleteOutlined, PlusCircleOutlined } from '@ant-design/icons';
import TextArea from 'antd/es/input/TextArea';
import {
	GroceryRequestModel,
	ShoppingItem,
} from '../models/GroceryRequestModel';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { changeCurrentShoppingItems } from '../redux/shoppingSlice';
import { useNavigate } from 'react-router-dom';
import ShoppingService from '../services/shopping-service';
import { setIsLoading } from '../redux/userSlice';
import { Text } from '../localization/TextsDE';

const { RangePicker } = DatePicker;

interface NewShoppingFormData {
	daterange: Date[];
	note: string;
	preferredstore: string;
}

const CreateShoppingRequest = () => {
	const [form] = Form.useForm();
	const navigate = useNavigate();
	const dispatch = useAppDispatch();

	const currentShopping: GroceryRequestModel = useAppSelector(
		(state) => state.shopping.currentShopping
	);

	const removeShoppingItem = (index: number) => {
		const newItems = [...currentShopping.groceryList];
		newItems.splice(index, 1);
		dispatch(changeCurrentShoppingItems(newItems));
	};

	const handleShoppingItemChange = (index: number, value: string) => {
		const newItems = [...currentShopping.groceryList];
		newItems[index] = { description: value };
		dispatch(changeCurrentShoppingItems(newItems));
	};

	const addShoppingItem = (value: string) => {
		const newItems = [...currentShopping.groceryList, { description: value }];
		dispatch(changeCurrentShoppingItems(newItems));
	};

	const handleSubmit = async (values: NewShoppingFormData) => {
		try {
			dispatch(setIsLoading(true));
			const shoppingService = new ShoppingService();
			const body: GroceryRequestModel = {
				fromDate: values.daterange[0].toJSON(),
				toDate: values.daterange[1].toJSON(),
				note: values.note,
				preferredStore: values.preferredstore,
				requestState: 'published',
				groceryList: currentShopping.groceryList,
			};
			await shoppingService.createShopping(body);
		} finally {
			dispatch(setIsLoading(false));
			navigate('/');
		}
	};

	return (
		<div className="w-full flex justify-center">
			<Form onFinish={handleSubmit} layout="vertical" form={form}>
				<div className="mb-8">
					<Form.Item name="note" label={Text.createShoppingRequestGeneral}>
						<TextArea />
					</Form.Item>
					<Form.Item
						name="daterange"
						label={Text.createShoppingRequestWhen}
						rules={[
							{
								required: true,
								message: Text.createShoppingRequestWhenMissing,
							},
						]}
					>
						<RangePicker format="DD.MM.YYYY" />
					</Form.Item>
					<Form.Item
						name="preferredstore"
						label={Text.createShoppingRequestPreferredStore}
					>
						<Input />
					</Form.Item>
					<p className="font-bold">{Text.createShoppingRequestAddArticle}</p>
					{currentShopping.groceryList.map(
						(option: ShoppingItem, index: number) => (
							<div key={index} className="flex items-center mb-2 shoppingItems">
								<Input
									value={option.description}
									onChange={(event) =>
										handleShoppingItemChange(index, event.target.value)
									}
								/>
								<Button
									className="border-none shadow-none"
									onClick={() => removeShoppingItem(index)}
								>
									<DeleteOutlined />
								</Button>
							</div>
						)
					)}
					<Button onClick={() => addShoppingItem('')} className="mt-2">
						{Text.createShoppingRequestButtonAdd} <PlusCircleOutlined />
					</Button>
				</div>
				<Form.Item>
					<Button type="primary" htmlType="submit">
						{Text.createShoppingRequestPublish}
					</Button>
				</Form.Item>
			</Form>
		</div>
	);
};

export default CreateShoppingRequest;
