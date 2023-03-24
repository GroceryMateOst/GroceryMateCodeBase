/// <reference types="cypress" />
describe('Just visit e2e test', () => {
	it('should visit', () => {
		cy.visit('/');
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

export {};
