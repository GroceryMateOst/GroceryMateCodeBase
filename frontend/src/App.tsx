import 'react-toastify/dist/ReactToastify.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './redux/store';
import RegistrationPage from './pages/RegistrationPage';
import Header from './components/header/Header';
import HomePage from './pages/HomePage';
import Error404Page from './pages/Error404Page';
import UserPage from './pages/UserPage';
import Footer from './components/General/Footer';
import LoginPage from './pages/LoginPage';
import CreateShoppingRequest from './pages/CreateShoppingRequest';
import { ToastContainer } from 'react-toastify';
import AuthenticatedRoute from './components/AuthenticatedRoute';
import PublishedShoppings from './pages/PublishedShoppings';
import AcceptedShoppings from './pages/AcceptedShoppings';
import SearchPage from './pages/SearchPage';

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
					<div id="content" className="m-10">
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
					</div>
					<Footer />
				</div>
			</Provider>
		</BrowserRouter>
	);
}

export default App;
