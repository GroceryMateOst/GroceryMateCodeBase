import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import NavBarMenu from './NavBarMenu';
import { useAppDispatch } from '../../redux/hooks';
import { setIsAuthenticated } from '../../redux/userSlice';

const Header = () => {
	const dispatch = useAppDispatch();

	useEffect(() => {
		const token = localStorage.getItem('bearerTokenGroceryMate');
		if (token != '' && token != null) {
			dispatch(setIsAuthenticated(true));
		}
	}, []);

	return (
		<div className="headerContainer ">
			<div
				className="flex flex-row justify-between bg-[#8fb69c] px-10 lg:px-20 py-6"
				id="header"
			>
				<Link to="/">
					<h1 className="my-0 hover:scale-95 transition-all duration-300 w-fit m-0">
						Grocery Mate
					</h1>
				</Link>
				<NavBarMenu />
			</div>
		</div>
	);
};

export default Header;
