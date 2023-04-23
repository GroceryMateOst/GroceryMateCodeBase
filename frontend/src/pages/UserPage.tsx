﻿import { Button, Form, Input, Skeleton } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { UserModelComplete } from '../models/UserModel';
import UserService from '../services/user-service';
import { useEffect } from 'react';
import { setIsLoading } from '../redux/userSlice';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { useNavigate } from 'react-router-dom';

const UserPage = () => {
	const [form] = Form.useForm<UserModelComplete>();
	const dispatch = useAppDispatch();

	const isLoading = useAppSelector((state) => state.user.isLoading);

	const getUser = async (userService: UserService) => {
		dispatch(setIsLoading(true));
		try {
			const response = await userService.getUserSettings();
			form.setFieldsValue({
				...response?.user,
				...response?.address,
			});
		} finally {
			dispatch(setIsLoading(false));
		}
	};

	useEffect(() => {
		const userService = new UserService();
		// eslint-disable-next-line @typescript-eslint/no-floating-promises
		getUser(userService);
	}, []);

	const handleSubmit = async (userSettings: UserModelComplete) => {
		const userService = new UserService();
		await userService.updateUserSettings(userSettings);
	};

	return (
		<div>
			{isLoading ? (
				<div className="px-6">
					<Skeleton active />
				</div>
			) : (
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
							<Input />
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
							<Input />
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
						<Form.Item
							name="residencyDetails"
							label="Details zu deinem Wohnort"
						>
							<TextArea />
						</Form.Item>
						<Form.Item>
							<Button type="primary" htmlType="submit">
								Speichern
							</Button>
						</Form.Item>
					</Form>
				</div>
			)}
		</div>
	);
};

export default UserPage;
