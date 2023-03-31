import { Button, Form, Input } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { UserModel } from '../models/UserModel';
import UserService from '../services/user-service';

const UserPage = () => {
	const handleSubmit = (values: UserModel) => {
		const userService = new UserService();
		userService.getUserSettings();
	};

	return (
		<div className="w-full flex justify-center">
			<Form
				className="w-[325px]"
				name="basic"
				initialValues={{ remember: true }}
				autoComplete="off"
				layout="vertical"
				onFinish={handleSubmit}
			>
				<Form.Item
					name="email"
					label="E-mail"
					rules={[
						{
							type: 'email',
							message: 'Das ist eine gültige E-Mail Adresse!',
						},
						{
							// required: true,
							message: 'Bitte gebe deine E-Mail Adresse ein!',
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="firstname"
					label="Vorname"
					tooltip="Was ist dein Vorname?"
					rules={[
						{
							// required: true,
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
							// required: true,
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
							// required: true,
							message: 'Bitte gebe an an welcher Strasse du wohnst.',
							whitespace: true,
						},
					]}
				>
					<Input />
				</Form.Item>
				<Form.Item
					name="plzOrt"
					label="PLZ/Ort"
					rules={[
						{
							// required: true,
							message: 'Bitte gebe eine PLZ und den dazugehörigen Ort an!',
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
