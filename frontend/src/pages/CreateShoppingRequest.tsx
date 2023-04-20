import { Button, Form, Input, DatePicker } from 'antd';
import { DeleteOutlined, PlusCircleOutlined } from '@ant-design/icons';
import TextArea from 'antd/es/input/TextArea';
import {
	ShoppingModel,
	ShoppingList,
	ShoppingItem,
} from '../models/ShoppingModel';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import {
	addCurrentShopping,
	changeCurrentShoppingItems,
} from '../redux/shoppingSlice';
import { Link, useNavigate } from 'react-router-dom';
import ShoppingService from '../services/shopping-service';
import { setIsLoading } from '../redux/userSlice';

const { RangePicker } = DatePicker;

interface NewShoppingFormData {
	daterange: Date[];
	note: string;
	preferedstore: string;
}

const CreateShoppingRequest = () => {
	const [form] = Form.useForm();
	const navigate = useNavigate();
	const dispatch = useAppDispatch();

	const currentShopping: ShoppingModel = useAppSelector(
		(state) => state.shopping.currentShopping
	);

	const removeShoppingItem = (index: number) => {
		const newItems = [...currentShopping.shoppingList.items];
		newItems.splice(index, 1);
		dispatch(changeCurrentShoppingItems(newItems));
	};

	const handleShoppingItemChange = (index: number, value: string) => {
		console.log(`index: ${index} value: ${value}`);
		const newItems = [...currentShopping.shoppingList.items];
		console.log(newItems);
		newItems[index] = { description: value };
		dispatch(changeCurrentShoppingItems(newItems));
	};

	const addShoppingItem = (value: string) => {
		const newItems = [
			...currentShopping.shoppingList.items,
			{ description: value },
		];
		dispatch(changeCurrentShoppingItems(newItems));
	};

	const handleSubmit = async (values: NewShoppingFormData) => {
		try {
			dispatch(setIsLoading(true));
			const shoppingService = new ShoppingService();
			const body: ShoppingModel = {
				userId: '',
				contractorId: '',
				fromDate: values.daterange[0],
				toDate: values.daterange[1],
				note: values.note,
				shoppingList: currentShopping.shoppingList,
			};
			await shoppingService.createShopping(body);
		} finally {
			dispatch(setIsLoading(false));
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
					<Form.Item name="notes" label="Generelle Hinweise zum Einkauf">
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
					{currentShopping.shoppingList.items.map(
						(option: ShoppingItem, index: number) => (
							<div key={index} className="flex items-center mb-2">
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
