import { Link } from 'react-router-dom';
import { Text } from '../../localization/TextsDE';

const CreateRequestCard = () => {
	return (
		<div>
			<h2>{Text.createRequestCardTitle}</h2>
			<p>{Text.createRequestCardText}</p>
			<button className="p-4 bg-primary border-primary shadow-none rounded-3xl border-[1px] border-solid hover:scale-95">
				<Link to="/create">{Text.createRequestCardLink}</Link>
			</button>
		</div>
	);
};

export default CreateRequestCard;
