import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './redux/store';
import RegistrationPage from './pages/RegistrationPage';
import Header from './components/header/Header';
import HomePage from './pages/HomePage';
import Error404Page from './pages/Error404Page';
import UserPage from './pages/UserPage';
import Footer from './components/Footer';

function App() {
	return (
		<BrowserRouter>
			<Provider store={store}>
				<div className="h-screen flex flex-col">
					<Header />
					<div id="content">
						<Routes>
							<Route path="/" element={<HomePage />} />
							<Route path="/register" element={<RegistrationPage />} />
							<Route path="/profile" element={<UserPage />} />
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
