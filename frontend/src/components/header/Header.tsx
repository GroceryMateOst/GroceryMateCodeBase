import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { UserOutlined } from '@ant-design/icons';
import NavBarMenu from './NavBarMenu';
import { useAppSelector, useAppDispatch } from '../../redux/hooks';
import { setIsAuthenticated } from '../../redux/userSlice';

const Header = () => {
	const isAuthenticated = useAppSelector((state) => state.user.isAuthenticated);

	const dispatch = useAppDispatch();

	useEffect(() => {
		const token = localStorage.getItem('bearerTokenGroceryMate');
		console.log(token);
		if (token != '' && token != null) {
			console.log('settings is authenticated');
			dispatch(setIsAuthenticated(true));
		}
	}, []);

	return (
		<div className="headerContainer ">
			<div
				className="grid grid-cols-[min-content,1fr,min-content] green-background px-6 py-6"
				id="header"
			>
				<NavBarMenu />
				<Link to="/" className="!flex justify-center">
					<h1 className="my-0">Grocery Mate</h1>
				</Link>
				<div className="flex justify-end">
					{isAuthenticated && (
						<Link to="/profile" className="mt-0 ml-0 flex items-center">
							<UserOutlined className="text-center text-[200%]" />
						</Link>
					)}
				</div>
			</div>
		</div>
	);
};

export default Header;
