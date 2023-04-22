import { Link } from 'react-router-dom';

const CreateRequestCard = () => {
	return (
		<div>
			<h2>Brauchst du Unterstützung bei einem Einkauf?</h2>
			<p>
				Veröffentliche mit wenigen Klicks deine Einkaufsanfrage and profitiere
				von unseren freiwilligen Helfern
			</p>
			<button className="p-4 bg-[#8fb69c] border-[#8fb69c] shadow-none rounded-3xl border-[1px] border-solid hover:scale-95">
				<Link to="/create">Klicke hier</Link>
			</button>
		</div>
	);
};

export default CreateRequestCard;
