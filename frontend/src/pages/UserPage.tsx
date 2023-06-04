import { Button, Form, Input, Skeleton, Space } from 'antd';
import TextArea from 'antd/es/input/TextArea';
import { UserModelComplete } from '../models/UserModel';
import UserService from '../services/user-service';
import { useEffect, useState } from 'react';
import { setIsLoading } from '../redux/userSlice';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { toast } from 'react-toastify';
import { Text } from '../localization/TextsDE';
import { SaveOutlined } from '@ant-design/icons';

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
		toast.success('Deine Änderungen wurden gespeichert.', {
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
							label={Text.userPageEmail}
							rules={[
								{
									type: 'email',
									message: Text.userPageEmailInvalide,
								},
								{
									required: true,
									message: Text.UserPageEmailMissing,
								},
							]}
						>
							<Input />
						</Form.Item>
						<Form.Item
							name="firstName"
							label={Text.userPageFirstName}
							rules={[
								{
									required: true,
									message: Text.userPageFirstNameMissing,
									whitespace: true,
								},
							]}
						>
							<Input />
						</Form.Item>
						<Form.Item
							name="secondName"
							label={Text.userPageSecondName}
							rules={[
								{
									required: true,
									message: Text.userPageSecondNameMissing,
									whitespace: true,
								},
							]}
						>
							<Input />
						</Form.Item>
						<Space.Compact block>
							<Form.Item
								name="street"
								label={Text.userPageStreet}
								rules={[
									{
										required: true,
										message: Text.userPageStreetMissing,
										whitespace: true,
									},
								]}
								style={{ width: '75%' }}
							>
								<Input />
							</Form.Item>
							<Form.Item
								name="houseNr"
								label={Text.userPageHouseNr}
								rules={[
									{
										required: true,
										message: Text.userPageHouseNrMissing,
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
								label={Text.userPageZipCode}
								rules={[
									{
										required: true,
										message: Text.userPageZipCodeMissing,
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
								label={Text.userPageCity}
								rules={[
									{
										required: true,
										message: Text.userPageCityMissing,
										whitespace: true,
									},
								]}
								style={{ width: '70%' }}
							>
								<Input disabled/>
							</Form.Item>
						</Space.Compact>
						<Form.Item
							name="state"
							label={Text.userPageState}
							rules={[
								{
									required: true,
									message: Text.userPageStateMissing,
									whitespace: true,
								},
							]}
						>
							<Input disabled />
						</Form.Item>
						<Form.Item name="residencyDetails" label={Text.userPageDetails}>
							<TextArea />
						</Form.Item>
						<Form.Item>
							<Button
								type="primary"
								htmlType="submit"
								disabled={!formHasChanged}
							>
								<SaveOutlined className="mr-[2px]" />
								{Text.save}
							</Button>
						</Form.Item>
					</Form>
				</div>
			)}
		</div>
	);
};

export default UserPage;
