import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { Button, Form, Input } from 'antd';
import { UserModel } from '../models/UserModel';
import UserService from '../services/user-service';
import { setIsLoading, setIsAuthenticated } from '../redux/userSlice';
import Spinner from '../components/General/LoadingSpinner';
import { Text } from '../localization/TextsDE';

interface RegisterFormData {
	email: string;
	firstname: string;
	name: string;
	password: string;
	confirm: string;
}

const RegistrationPage = () => {
	const [form] = Form.useForm();
	const navigate = useNavigate();
	const dispatch = useAppDispatch();

	const isLoading = useAppSelector((state) => state.user.isLoading);

	const handleSubmit = async (values: RegisterFormData) => {
		dispatch(setIsLoading(true));
		const userService = new UserService();
		const registerBody: UserModel = {
			emailaddress: values.email,
			password: values.password,
			secondname: values.name,
			firstname: values.firstname,
			residencyDetails: '',
		};
		try {
			await userService.registerAccount(registerBody);
			const response = await userService.loginUser({
				emailaddress: values.email,
				password: values.password,
			});
			localStorage.setItem('bearerTokenGroceryMate', response.token);
			localStorage.setItem('userEmail', values.email);
			dispatch(setIsAuthenticated(true));
			navigate('/');
		} finally {
			dispatch(setIsLoading(false));
		}
	};

	return (
		<div className="w-full flex justify-center">
			{isLoading ? (
				<div className="mt-40">
					<Spinner />
				</div>
			) : (
				<Form
					className="w-[325px]"
					name="basic"
					layout="vertical"
					form={form}
					initialValues={{ remember: true }}
					onFinish={handleSubmit}
					autoComplete="off"
				>
					<Form.Item
						name="email"
						label={Text.userPageEmail}
						rules={[
							{
								type: 'email',
								message: Text.userPageEmailInvalide,
							},
							{
								required: true,
								message: Text.userPageEmailMissing,
							},
						]}
					>
						<Input />
					</Form.Item>

					<Form.Item
						name="firstname"
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
						name="name"
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

					<Form.Item
						name="password"
						label={Text.loginPagePassword}
						rules={[
							{
								required: true,
								message: Text.loginPagePasswordMissing,
							},
						]}
						hasFeedback
					>
						<Input.Password />
					</Form.Item>

					<Form.Item
						name="confirm"
						label={Text.registrationPageConfirm}
						dependencies={['password']}
						hasFeedback
						rules={[
							{
								required: true,
								message: Text.registrationPageConfrimMissing,
							},
							({ getFieldValue }) => ({
								validator(_, value) {
									if (!value || getFieldValue('password') === value) {
										return Promise.resolve();
									}
									return Promise.reject(
										new Error('Die beiden Passwörter stimmen nicht überein!')
									);
								},
							}),
						]}
					>
						<Input.Password />
					</Form.Item>

					<Form.Item>
						<Button type="primary" htmlType="submit">
							{Text.registrationPageButton}
						</Button>
					</Form.Item>
				</Form>
			)}
		</div>
	);
};

export default RegistrationPage;
