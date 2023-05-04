import { Link, useNavigate } from 'react-router-dom';
import { Dropdown, MenuProps } from 'antd';
import { MenuOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import { setIsAuthenticated } from '../../redux/userSlice';
import UserService from '../../services/user-service';

const NavBarMenu = () => {
	const navigate = useNavigate();
	const dispatch = useAppDispatch();
	const isAuthenticated = useAppSelector((state) => state.user.isAuthenticated);

	const onDropdownClick: MenuProps['onClick'] = async ({ key }) => {
		if (key == '4') {
			const userService = new UserService();
			await userService.logout();
			dispatch(setIsAuthenticated(false));
			navigate('/');
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
		{
			key: '5',
			label: <Link to="/published">Meine Inserate</Link>,
		},
		{
			key: '6',
			label: <Link to="/accepted">Meine Aufträge</Link>,
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
