/* eslint-disable @typescript-eslint/no-misused-promises */
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HomeContainer from './components/HomeContainer';

function App() {
	return (
		<BrowserRouter>
			<Routes>
				<Route path="/" element={<HomeContainer />} />
				<Route path="*" element={<div>Hello</div>} />
			</Routes>
		</BrowserRouter>
	);
}

export default App;
