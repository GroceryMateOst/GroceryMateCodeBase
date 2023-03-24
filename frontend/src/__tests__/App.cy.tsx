import App from '../App';
import { mount } from '@cypress/react18';

describe('<App>', () => {
	it('mounts', () => {
		mount(<App />);
	});
});

describe('Weather API', () => {
	it('returns the expected response', () => {
		cy.request('GET', 'http://localhost:5000/weatherforecast').then(
			(response) => {
				expect(response.status).to.eq(200);
			}
		);
	});
});
