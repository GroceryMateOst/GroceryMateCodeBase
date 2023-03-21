import { Button } from 'antd';
import { useState } from 'react';
import ClickService from '../services/click-service';
import reactLogo from '../assets/react.svg';

const HomeContainer = () => {
	const [count, setCount] = useState(0);

	const test = async () => {
		const clickService = new ClickService();
		const newCount = count + 1;
		await clickService.sendClicks(newCount);
		setCount(newCount);
	};

	const countUp = () => {
		test().catch(() => console.error('Shit'));
	};

	return (
		<div className="App">
			<div>
				<a href="https://vitejs.dev" target="_blank" rel="noreferrer">
					<img src="/vite.svg" className="logo" alt="Vite logo" />
				</a>
				<a href="https://reactjs.org" target="_blank" rel="noreferrer">
					<img src={reactLogo} className="logo react" alt="React logo" />
				</a>
			</div>
			<h1>Vite + React</h1>
			<div className="card">
				<Button onClick={countUp} className="bg-green-400">
					count is {count} to {import.meta.env.BACKEND_URL}
				</Button>
				<p>
					Edit <code>src/App.tsx</code> and save to test HMR
				</p>
			</div>
			<p className="read-the-docs">
				Click on the Vite and React logos to learn more
			</p>
		</div>
	);
};

export default HomeContainer;
