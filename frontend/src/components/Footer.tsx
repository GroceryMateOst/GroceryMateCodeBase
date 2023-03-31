import GroceryMateLogo from '../assets/GroceryMateLogo';

const Footer = () => {
	return (
		<div className="bg-gray-500 self p-6 mt-auto">
			<GroceryMateLogo />
			<p>{import.meta.env.VITE_SERVER_BASE_URL}</p>
		</div>
	);
};

export default Footer;
