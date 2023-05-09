import CreateRequestCard from '../components/General/CreateRequestCard';
import GroceryListOverView from '../components/GroceryListOverView/GroceryListOverView';
import Map from '../components/Map';

const HomePage = () => {
	return (
		<div className="px-20 mb-10">
			<div>
				<h2 className="my-0">Um Was geht es?</h2>
				<p className="mt-0 text-lg">
					Es gibt viele Menschen, die aus verschiedenen Gründen wie Behinderung,
					Krankheit oder fehlendem Transportmittel nicht in der Lage sind,
					Lebensmitteleinkäufe zu erledigen. Auf der anderen Seite gibt es in
					jeder Gemeinschaft viele Menschen, die bereit sind, Menschen in Not zu
					unterstützen. Unser Ziel ist es, mit dieser Webseite eine einfache
					Möglichkeit bereitzustellen, um sie miteinander zu verbinden.
				</p>
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
