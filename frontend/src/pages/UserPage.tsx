import { Button, Form, Input, Skeleton, Space } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { UserModelComplete } from '../models/UserModel';
import UserService from '../services/user-service';
import { useEffect, useState } from 'react';
import { setIsLoading } from '../redux/userSlice';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { toast } from 'react-toastify';

const UserPage = () => {
	const [formHasChanged, setFormHasChanged] = useState(false);
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
		toast.success('Deine Änderungen wurden gespeicher.', {
			position: 'top-center',
			autoClose: 5000,
			hideProgressBar: false,
			closeOnClick: true,
			pauseOnHover: true,
			draggable: true,
			progress: undefined,
			theme: 'light',
		});
		setFormHasChanged(false);
	};

	const getCityByZipCode = async (zipCode: string) => {
		const userService = new UserService();
		const response = await userService.getCityByZip(zipCode);
		form.setFieldValue('city', response.city);
		form.setFieldValue('state', response.state);
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
						onFieldsChange={() => setFormHasChanged(true)}
					>
						<Form.Item
							name="emailAddress"
							label="E-mail"
							rules={[
								{
									type: 'email',
									message: 'Das ist eine ungültige E-Mail Adresse!',
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
							rules={[
								{
									required: true,
									message: 'Bitte gebe deinen Vornamen ein!',
									whitespace: true,
								},
							]}
						>
							<Input />
						</Form.Item>
						<Form.Item
							name="secondName"
							label="Nachname"
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
						<Space.Compact block>
							<Form.Item
								name="street"
								label="Strasse"
								rules={[
									{
										required: true,
										message: 'Bitte gebe an, an welcher Strasse du wohnst.',
										whitespace: true,
									},
								]}
								style={{ width: '75%' }}
							>
								<Input />
							</Form.Item>
							<Form.Item
								name="houseNr"
								label="Hausnr."
								rules={[
									{
										required: true,
										message: 'Bitte gib deine Hausnummer an.',
										whitespace: true,
									},
								]}
								style={{ width: '25%' }}
							>
								<Input />
							</Form.Item>
						</Space.Compact>
						<Space.Compact block>
							<Form.Item
								name="zipCode"
								label="PLZ"
								rules={[
									{
										required: true,
										message: 'Bitte gebe eine Postleizahl an!',
										whitespace: true,
									},
								]}
								style={{ width: '30%' }}
							>
								<Input
									onBlur={(element) => getCityByZipCode(element.target.value)}
								/>
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
								style={{ width: '70%' }}
							>
								<Input />
							</Form.Item>
						</Space.Compact>
						<Form.Item
							name="state"
							label="Kanton"
							rules={[
								{
									required: true,
									message: 'Bitte gebe den Kanton an in welchem du Wohnst!',
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
							<Button
								type="primary"
								htmlType="submit"
								disabled={!formHasChanged}
							>
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
