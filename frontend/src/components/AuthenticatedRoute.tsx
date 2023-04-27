import { Route, useNavigate } from 'react-router-dom';
import { useAppSelector } from '../redux/hooks';
import HomePage from '../pages/HomePage';

interface AuthenticatedRouteProps {
	element: React.ReactNode;
	redirectElement?: React.ReactNode;
}

const AuthenticatedRoute = ({
	element,
	redirectElement,
}: AuthenticatedRouteProps) => {
	const isAuthenticated = useAppSelector((state) => state.user.isAuthenticated);
	return isAuthenticated ? (
		<>{element}</>
	) : redirectElement ? (
		<>{redirectElement}</>
	) : (
		<HomePage />
	);
};

export default AuthenticatedRoute;
