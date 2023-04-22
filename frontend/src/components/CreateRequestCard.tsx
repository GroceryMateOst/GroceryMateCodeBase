import { Card } from 'antd';
import { Link } from 'react-router-dom';

const CreateRequestCard = () => {
	return (
		<Card
			title="Brauchst du Unterstützung bei einem Einkauf?"
			extra={<Link to="/create">Klicke hier</Link>}
			style={{ width: 500 }}
		>
			<p>
				Veröffentliche mit wenigen Klicks deine Einkaufsanfrage and profitiere
				von unseren freiwilligen Helfern
			</p>
		</Card>
	);
};

export default CreateRequestCard;
