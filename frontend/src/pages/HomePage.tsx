import CreateRequestCard from '../components/General/CreateRequestCard';
import GroceryListOverView from '../components/GroceryListOverView/GroceryListOverView';
import Map from '../components/Map';
import { Text } from '../localization/TextsDE';

const HomePage = () => {
	return (
		<div className="px-20 mb-10">
			<div>
				<h2 className="my-0">{Text.homePageTitle}</h2>
				<p className="mt-0 text-lg">{Text.homePageAbout}</p>
			</div>
			<GroceryListOverView />
			<CreateRequestCard />
			<Map
				zoom={20}
				latitude={47.30682745}
				longitude={9.082206350049724}
				width="500px"
				height="500px"
			/>
		</div>
	);
};

export default HomePage;
