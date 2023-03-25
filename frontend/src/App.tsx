import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './redux/store';
import HomeContainer from './components/HomeContainer';
import Registration from './pages/Registration';
import NavBar from './components/NavBar';
import Error404 from './components/Error404';

function App() {
	return (
		<BrowserRouter>
			<Provider store={store}>
				<NavBar />
				<Routes>
					<Route path="/" element={<HomeContainer />} />
					<Route path="/register" element={<Registration />} />
					<Route path="*" element={<Error404 />} />
				</Routes>
			</Provider>
		</BrowserRouter>
	);
}

export default App;
