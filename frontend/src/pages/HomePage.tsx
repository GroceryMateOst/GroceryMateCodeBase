import CreateRequestCard from '../components/General/CreateRequestCard';
import GroceryListOverView from '../components/GroceryListOverView/GroceryListOverView';
import Map from '../components/Map';
import { Text } from '../localization/TextsDE';

const HomePage = () => {
	return (
		<div className="mb-10 px-10 lg:px-20">
			<div>
				<h2 className="my-0">{Text.homePageTitle}</h2>
				<p className="mt-0 text-lg">{Text.homePageAbout}</p>
			</div>
			<GroceryListOverView />
			<CreateRequestCard />
		</div>
	);
};

export default HomePage;
