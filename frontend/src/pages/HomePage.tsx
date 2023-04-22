import CreateRequestCard from '../components/CreateRequestCard';
import GroceryListOverView from '../components/GroceryListOverView/GroceryListOverView';

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
		</div>
	);
};

export default HomePage;
