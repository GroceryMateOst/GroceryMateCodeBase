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
			<Form
				name="normal_login"
				className="login-form"
				onFinish={handleSubmit}
				layout="vertical"
				form={form}
			>
				<div className="mb-8">
					<Form.Item name="note" label="Generelle Hinweise zum Einkauf">
						<TextArea />
					</Form.Item>
					<Form.Item
						name="daterange"
						label="Wann soll der Einkauf geschehen?"
						rules={[
							{
								required: true,
								message: 'Bitte geben Sie eine Zeitspanne ein',
							},
						]}
					>
						<RangePicker format="DD.MM.YYYY" />
					</Form.Item>
					<Form.Item name="preferredstore" label="Bevorzugter Supermarkt">
						<Input />
					</Form.Item>
					<p className="font-bold">
						Bitte fügen Sie die benötigten Artikel hinzu
					</p>
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
						Artikel hinzufügen <PlusCircleOutlined />
					</Button>
				</div>
				<Form.Item>
					<Button type="primary" htmlType="submit">
						Anfrage publizieren
					</Button>
				</Form.Item>
			</Form>
		</div>
	);
};

export default CreateShoppingRequest;