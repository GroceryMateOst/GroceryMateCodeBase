import { Link, useNavigate } from 'react-router-dom';
import { Dropdown, MenuProps } from 'antd';
import {
	HomeOutlined,
	LoginOutlined,
	LogoutOutlined,
	MenuOutlined,
	ProfileOutlined,
	ShoppingCartOutlined,
	UserAddOutlined,
	UserOutlined,
} from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import { setIsAuthenticated } from '../../redux/userSlice';
import UserService from '../../services/user-service';
import { Text } from '../../localization/TextsDE';

const NavBarMenu = () => {
	const navigate = useNavigate();
	const dispatch = useAppDispatch();
	const isAuthenticated = useAppSelector((state) => state.user.isAuthenticated);

	const onDropdownClick: MenuProps['onClick'] = async ({ key }) => {
		if (key == '5') {
			const userService = new UserService();
			await userService.logout();
			dispatch(setIsAuthenticated(false));
			navigate('/');
		}
	};

	const items = [
		{
			key: '1',
			label: (
				<Link to="/">
					<HomeOutlined className="mr-3" />
					{Text.navBarMenuHome}
				</Link>
			),
		},
		{
			key: '2',
			label: (
				<Link to="/login">
					<LoginOutlined className="mr-3" />
					{Text.navBarMenuLogin}
				</Link>
			),
		},
		{
			key: '3',
			label: (
				<Link to="/register">
					<UserAddOutlined className="mr-3" />
					{Text.navBarMenuRegistration}
				</Link>
			),
		},
		{
			key: '4',
			label: (
				<Link to="/profile">
					<UserOutlined className="mr-3" />
					{Text.navBarMenuProfil}
				</Link>
			),
		},
		{
			key: '5',
			label: (
				<>
					<LogoutOutlined className="mr-3" />
					{Text.navBarMenuLogout}
				</>
			),
		},
		{
			key: '6',
			label: (
				<Link to="/published">
					<ProfileOutlined className="mr-3" />
					{Text.navBarMenuMyInserat}
				</Link>
			),
		},
		{
			key: '7',
			label: (
				<Link to="/accepted">
					<ShoppingCartOutlined className="mr-3" />
					{Text.navBarMenuMyOrders}
				</Link>
			),
		},
	];

	function getNavItems(
		userIsAuthenticated: boolean
	): Array<{ key: string; label: JSX.Element | string }> {
		const navItems = [items[0]];
		if (userIsAuthenticated) {
			return [...navItems, items[3], items[4], items[5], items[6]];
		} else {
			return [...navItems, items[1], items[2]];
		}
	}

	return (
		<Dropdown
			menu={{
				items: getNavItems(isAuthenticated),
				onClick: onDropdownClick,
			}}
			className="flex items-center"
		>
			<MenuOutlined className="text-[150%]" />
		</Dropdown>
	);
};

export default NavBarMenu;
