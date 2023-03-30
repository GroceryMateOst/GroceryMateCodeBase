import { Link } from 'react-router-dom';
import { UserOutlined } from '@ant-design/icons';
import NavBar from './NavBar';

const Header = () => {
	return (
		<div
			className="flex justify-between items-center green-background navbar px-6 py-6"
			id="header"
		>
			<NavBar />
			<Link to="/">
				<h1 className="my-0">Grocery Mate</h1>
			</Link>
			<Link to="/user" className="mt-0 ml-0">
				<UserOutlined
					style={{ fontSize: '200%', color: '#213547' }}
					className="text-center"
				/>
			</Link>
		</div>
	);
};

export default Header;
