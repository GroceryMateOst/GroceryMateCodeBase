import { Button, Form, Input } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { setIsLoading, setIsAuthenticated } from '../redux/userSlice';
import { useAppDispatch, useAppSelector } from '../redux/hooks';
import UserService from '../services/user-service';
import Spinner from '../components/General/LoadingSpinner';
import { Text } from '../localization/TextsDE';

interface LoginFormData {
	email: string;
	password: string;
}

const LoginPage = () => {
	const navigate = useNavigate();
	const dispatch = useAppDispatch();
	const location = useLocation();
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
			if (location.pathname === '/login') {
				navigate('/');
			}
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
					name="normal_login"
					className="login-form"
					layout="vertical"
					initialValues={{ remember: true }}
					onFinish={handleSubmit}
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
						<Input
							prefix={<UserOutlined className="site-form-item-icon" />}
							placeholder={Text.userPageEmail}
						/>
					</Form.Item>
					<Form.Item
						name="password"
						label={Text.loginPagePassword}
						rules={[{ required: true, message: Text.loginPagePasswordMissing }]}
					>
						<Input
							prefix={<LockOutlined className="site-form-item-icon" />}
							type="password"
							placeholder={Text.loginPagePassword}
						/>
					</Form.Item>
					<Form.Item style={{ marginBottom: '5px' }}>
						<Button
							type="primary"
							htmlType="submit"
							className="login-form-button"
							style={{ width: '120px' }}
						>
							{Text.loginPageButton}
						</Button>
					</Form.Item>
					<Form.Item>
						{Text.loginPageGoRegistration}
						<Link to="/register">
							<span className="text-primary">{Text.loginPageHere}</span>
						</Link>
						.
					</Form.Item>
				</Form>
			)}
		</div>
	);
};

export default LoginPage;
