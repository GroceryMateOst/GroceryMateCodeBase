import { Link } from 'react-router-dom';
import { UserOutlined } from '@ant-design/icons';
import NavBar from './NavBar';

const Header = () => {
	return (
		<div className="headerContainer ">
			<div
				className="flex justify-between items-center green-background navbar px-6 py-6"
				id="header"
			>
				<NavBar />
				<h1>Grocery Mate</h1>
				<Link to="/login" className="mt-0 ml-0">
					<div style={{ fontSize: '140%' }} className="inline-block mr-3">
						Profil
					</div>
					<UserOutlined
						style={{ fontSize: '200%', color: '#213547' }}
						className="text-center"
					/>
				</Link>
			</div>
		</div>
	);
};

export default Header;
