import { Link } from 'react-router-dom';
import { Dropdown, Space } from 'antd';
import { MenuOutlined } from '@ant-design/icons';

const NavBar = () => {
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
			label: <Link to="/register">Register</Link>,
		},
	];
	return (
		<Dropdown
			menu={{
				items,
			}}
		>
			<a onClick={(e) => e.preventDefault()}>
				<Space style={{ fontSize: '150%' }}>
					<MenuOutlined />
					Menü
				</Space>
			</a>
		</Dropdown>
	);
};

export default NavBar;
