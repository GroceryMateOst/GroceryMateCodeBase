import { Button, Form, Input } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';
import { setIsLoading, setIsAuthenticated } from '../redux/userSlice';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import UserService from '../services/user-service';
import Spinner from '../components/LoadingSpinner';

interface LoginFormData {
	email: string;
	password: string;
}

const LoginPage = () => {
	const navigate = useNavigate();
	const dispatch = useAppDispatch();

	const isLoading = useAppSelector((state) => state.user.isLoading);

	const handleSubmit = async (values: LoginFormData) => {
		try {
			dispatch(setIsLoading(true));
			const userService = new UserService();
			const response = await userService.loginUser({
				emailaddress: values.email,
				password: values.password,
			});
			localStorage.setItem('bearerTokenGroceryMate', response.token);
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
			<Form
				name="normal_login"
				className="login-form"
				initialValues={{ remember: true }}
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
							required: true,
							message: 'Bitte gebe deine E-Mail Adresse ein!',
						},
					]}
				>
					<Input
						prefix={<UserOutlined className="site-form-item-icon" />}
						placeholder="E-Mail"
					/>
				</Form.Item>
				<Form.Item
					name="password"
					label="Passwort"
					rules={[{ required: true, message: 'Bitte gib dein Passwort ein!' }]}
				>
					<Input
						prefix={<LockOutlined className="site-form-item-icon" />}
						type="password"
						placeholder="Passwort"
					/>
				</Form.Item>
				<Form.Item style={{ marginBottom: '5px' }} {...tailFormItemLayout}>
					<Button
						type="primary"
						htmlType="submit"
						className="login-form-button"
						style={{ width: '120px' }}
					>
						Log in
					</Button>
				</Form.Item>
				<Form.Item {...tailFormItemLayout}>
					Oder <Link to="/register">registrie dich</Link> jetzt
				</Form.Item>
			</Form>
			{isLoading && (
				<div className="my-16">
					<Spinner />
				</div>
			)}
		</div>
	);
};

export default LoginPage;
