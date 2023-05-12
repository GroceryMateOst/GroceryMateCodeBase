import { Suspense } from 'react';
import * as React from 'react';
import 'react-toastify/dist/ReactToastify.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './redux/store';
import { ToastContainer } from 'react-toastify';
import Header from './components/header/Header';
import Footer from './components/General/Footer';
import Loading from './pages/LoadingPage';

const RegistrationPage = React.lazy(() => import('./pages/RegistrationPage'));
const HomePage = React.lazy(() => import('./pages/HomePage'));
const Error404Page = React.lazy(() => import('./pages/Error404Page'));
const UserPage = React.lazy(() => import('./pages/UserPage'));
const LoginPage = React.lazy(() => import('./pages/LoginPage'));
const CreateShoppingRequest = React.lazy(
	() => import('./pages/CreateShoppingRequest')
);
const AuthenticatedRoute = React.lazy(
	() => import('./components/AuthenticatedRoute')
);
const PublishedShoppings = React.lazy(
	() => import('./pages/PublishedShoppings')
);
const AcceptedShoppings = React.lazy(() => import('./pages/AcceptedShoppings'));
const SearchPage = React.lazy(() => import('./pages/SearchPage'));

function App() {
	return (
		<BrowserRouter>
			<Provider store={store}>
				<div className="h-screen flex flex-col">
					<ToastContainer
						position="top-center"
						autoClose={5000}
						hideProgressBar={false}
						newestOnTop={false}
						closeOnClick
						rtl={false}
						pauseOnFocusLoss
						draggable
						pauseOnHover
						theme="light"
					/>
					<Header />
					<div id="content" className="my-10">
						<Suspense fallback={<Loading />}>
							<Routes>
								<Route path="/" element={<HomePage />} />
								<Route path="/register" element={<RegistrationPage />} />
								<Route path="/login" element={<LoginPage />} />
								<Route path="/search" element={<SearchPage />} />
								<Route
									path="/profile"
									element={<AuthenticatedRoute element={<UserPage />} />}
								/>
								<Route
									path="/create"
									element={
										<AuthenticatedRoute
											element={<CreateShoppingRequest />}
											redirectElement={<LoginPage />}
										/>
									}
								/>
								<Route
									path="/published"
									element={
										<AuthenticatedRoute
											element={<PublishedShoppings />}
											redirectElement={<LoginPage />}
										/>
									}
								/>
								<Route
									path="/accepted"
									element={
										<AuthenticatedRoute
											element={<AcceptedShoppings />}
											redirectElement={<LoginPage />}
										/>
									}
								/>
								<Route path="*" element={<Error404Page />} />
							</Routes>
						</Suspense>
					</div>
					<Footer />
				</div>
			</Provider>
		</BrowserRouter>
	);
}

export default App;
