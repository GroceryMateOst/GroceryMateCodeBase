import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './redux/store';
import HomeContainer from './components/HomeContainer';
import Registration from './pages/Registration';
import Header from './components/header/Header';
import Error404 from './components/Error404';
import Login from './pages/Login';

function App() {
	return (
		<BrowserRouter>
			<Provider store={store}>
				<Header />
				<div id="content">
					<Routes>
						<Route path="/" element={<HomeContainer />} />
						<Route path="/register" element={<Registration />} />
						<Route path="/login" element={<Login />} />
						<Route path="*" element={<Error404 />} />
					</Routes>
				</div>
			</Provider>
		</BrowserRouter>
	);
}

export default App;
