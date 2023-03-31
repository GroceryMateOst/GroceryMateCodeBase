import { Link } from 'react-router-dom';
import { Dropdown, MenuProps } from 'antd';
import { MenuOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import { setIsAuthenticated } from '../../redux/userSlice';
import UserService from '../../services/user-service';

const NavBarMenu = () => {
	const dispatch = useAppDispatch();
	const isAuthenticated = useAppSelector((state) => state.user.isAuthenticated);

	const onDropdownClick: MenuProps['onClick'] = ({ key }) => {
		if (key == '4') {
			const userService = new UserService();
			userService.logout();
			dispatch(setIsAuthenticated(false));
		}
	};

	const items = [
		{
			key: '1',
			label: <Link to="/">Home</Link>,
		},
		{
			key: '2',
			label: <Link to="/login">Login</Link>,
		},
		{
			key: '3',
			label: <Link to="/register">Registration</Link>,
		},
		{
			key: '4',
			label: 'Logout',
		},
	];

	function getNavItems(
		userIsAuthenticated: boolean
	): Array<{ key: string; label: JSX.Element | string }> {
		const navItems = [items[0]];
		if (userIsAuthenticated) {
			return [...navItems, items[3]];
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
