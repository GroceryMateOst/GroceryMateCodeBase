import { Button, Form, Input, InputNumber } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { UserModelComplete } from '../models/UserModel';
import UserService from '../services/user-service';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const UserPage = () => {
	const navigate = useNavigate();
	const [form] = Form.useForm<UserModelComplete>();

	useEffect(() => {
		const email = localStorage.getItem('userEmail');
		if (email) {
			const userService = new UserService();
			try {
				getUser(userService, email);
			} catch {
				navigate('/error');
			}
		}
	}, []);

	const getUser = async (userService: UserService, email: string) => {
		const userSettings = await userService.getUserSettings(email);
		form.setFieldsValue(userSettings);
	};

	const handleSubmit = (userSettings: UserModelComplete) => {
		console.log(userSettings);
	};

	return (
		<div className="w-full flex justify-center">
			<Form
				className="w-[325px]"
				name="basic"
				form={form}
				initialValues={{ remember: true }}
				autoComplete="off"
				layout="vertical"
				onFinish={handleSubmit}
			>
				<Form.Item
					name="emailAddress"
					label="E-mail"
					rules={[
						{
							type: 'email',
							message: 'Das ist eine gültige E-Mail Adresse!',
						},
						{
							required: true,
							message: 'Bitte gebe deine E-Mail Adresse ein!',
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="firstName"
					label="Vorname"
					tooltip="Was ist dein Vorname?"
					rules={[
						{
							required: true,
							message: 'Bitte gebe deinen Vorname ein!',
							whitespace: true,
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="secondName"
					label="Nachname"
					tooltip="Was ist dein Familienname?"
					rules={[
						{
							required: true,
							message: 'Bitte gebe deinen Nachnamen ein!',
							whitespace: true,
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="street"
					label="Strasse"
					rules={[
						{
							required: true,
							message: 'Bitte gebe an an welcher Strasse du wohnst.',
							whitespace: true,
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="houseNr"
					label="Hausnummer"
					rules={[
						{
							required: true,
							message: 'Bitte gib deine Hausnummer an.',
							whitespace: true,
						},
					]}
				>
					<InputNumber />
				</Form.Item>
				<Form.Item
					name="zipCode"
					label="PLZ"
					rules={[
						{
							required: true,
							message: 'Bitte gebe eine PLZ an!',
							whitespace: true,
						},
					]}
				>
					<InputNumber />
				</Form.Item>
				<Form.Item
					name="city"
					label="Ort"
					rules={[
						{
							required: true,
							message: 'Bitte gebe deinen Wohnort an!',
							whitespace: true,
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="state"
					label="Kanton"
					rules={[
						{
							required: true,
							message: 'bitte gebe den Kanton an in welchem du Wohnst!',
							whitespace: true,
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item name="addressDetails" label="Details zu deinem Wohnort">
					<TextArea />
				</Form.Item>
				<Form.Item>
					<Button type="primary" htmlType="submit">
						Speichern
					</Button>
				</Form.Item>
			</Form>
		</div>
	);
};

export default UserPage;
