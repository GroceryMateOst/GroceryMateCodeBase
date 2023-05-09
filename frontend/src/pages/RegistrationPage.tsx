import { useNavigate } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import { Button, Form, Input } from 'antd';
import { UserModel } from '../models/UserModel';
import UserService from '../services/user-service';
import { setIsLoading, setIsAuthenticated } from '../redux/userSlice';
import Spinner from '../components/General/LoadingSpinner';

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

	const tailFormItemLayout = {
		wrapperCol: {
			xs: {
				span: 24,
				offset: 0,
			},
			sm: {
				span: 16,
				offset: 8,
			},
		},
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
						label="E-mail"
						rules={[
							{
								type: 'email',
								message: 'Das ist keine gültige E-Mail Adresse!',
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
						name="firstname"
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
						name="name"
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
						name="password"
						label="Passwort"
						rules={[
							{
								required: true,
								message: 'Bitte gebe ein Passwort ein!',
							},
						]}
						hasFeedback
					>
						<Input.Password />
					</Form.Item>

					<Form.Item
						name="confirm"
						label="Bestätigung"
						dependencies={['password']}
						hasFeedback
						rules={[
							{
								required: true,
								message: 'Bitte bestätige dein Passwort!',
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

					<Form.Item {...tailFormItemLayout}>
						<Button type="primary" htmlType="submit">
							Registriere dich
						</Button>
					</Form.Item>
				</Form>
			)}
		</div>
	);
};

export default RegistrationPage;
