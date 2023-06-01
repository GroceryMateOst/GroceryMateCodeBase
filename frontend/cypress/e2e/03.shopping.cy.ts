/// <reference types="cypress" />

import { constants } from '../fixtures/constants';

describe('User creates a new GroceryRequest with Multiple groceries and submits it', () => {
	it('Opens http://localhost:3000', () => {
		cy.visit('http://localhost:3000');
	});

	it('Should login the user', () => {
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Anmelden').click();
		cy.get('#normal_login_email').type(constants.EMAIL);
		cy.get('#normal_login_password').type(constants.PASSWORD);
		cy.get('button[type="submit"]').click();
		cy.url().should('eq', 'http://localhost:3000/');
	});
	it('should navigate to the "Create" page when clicking the corresponding link', () => {
		cy.get('a[href*="create"]').click();
		cy.url().should('include', '/create');
	});

	it('should allow users to create a shopping list', () => {
		cy.get('#note').type('Please buy some groceries.');
		cy.get('#preferredstore').type('Migros');
		cy.get('.ant-picker-range') // Select the input field of the RangePicker component
			.click() // Open the date picker
			.then(() => {
				cy.get('td[title="2023-06-01"]') // Search for the <td> tag with title="2023-05-01"
					.click(); // Select the desired start date

				cy.get('td[title="2023-06-10"]') // Search for the <td> tag with title="2023-05-10"
					.click(); // Select the desired end date
			});
		cy.get('button').contains('Artikel hinzufügen').click();
		cy.get('.shoppingItems').find('input').first().type('5 Apples');
		cy.get('button').contains('Artikel hinzufügen').click();
		cy.get('.shoppingItems').find('input').eq(1).type('7 Bananas');
		cy.get('button').contains('Anfrage publizieren').click();
	});

	it('Performs logout', () => {
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Abmelden').click();
		cy.url().should('eq', 'http://localhost:3000/');
	});

	it('Navigates to registration', () => {
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Registrieren').click();
		cy.url().should('include', '/register');
	});

	it('Fills out the registration form', () => {
		cy.get('#basic_email').type(constants.CLIENT.EMAIL);
		cy.get('#basic_firstname').type(constants.CLIENT.FIRST_NAME);
		cy.get('#basic_name').type(constants.CLIENT.LAST_NAME);
		cy.get('#basic_password').type(constants.CLIENT.PASSWORD);
		cy.get('#basic_confirm').type(constants.CLIENT.PASSWORD);
		cy.get('button[type="submit"]').click();
		cy.url().should('eq', 'http://localhost:3000/');
	});

	it('should fill out the address data', () => {
		cy.visit('/profile');
		cy.get('#basic_emailAddress').clear().type(constants.CLIENT.EMAIL);
		cy.get('#basic_firstName').clear().type(constants.CLIENT.FIRST_NAME);
		cy.get('#basic_secondName').clear().type(constants.CLIENT.LAST_NAME);
		cy.get('#basic_street').clear().type(constants.CLIENT.STREET);
		cy.get('#basic_houseNr').clear().type(constants.CLIENT.NUMBER);
		cy.get('#basic_zipCode').clear().type(constants.CLIENT.ZIP).blur();
		cy.wait(5000);
		cy.get('#basic_residencyDetails').clear().type(constants.DETAILS);

		cy.get('button[type="submit"]').click();
	});

	it('should be able to search the published request by giving a zip code', () => {
		cy.visit('/search');
		cy.get('.ant-input').type(constants.CLIENT.ZIP).blur();
		cy.get('.search-button').click();
	});

	it('should show the previous published request', () => {
		cy.contains(`${constants.FIRST_NAME}`).should('be.visible');
	});

	it('should show the correct distance', () => {
		cy.get('div.distance')
			.first()
			.should(($div) => {
				const distanceText = $div.text().trim();
				const distance = parseFloat(distanceText);

				expect(distance).to.be.gte(10);
				expect(distance).to.be.lte(15);
			});
	});

	it('the request should be collabsable to get more information', () => {
		cy.get('div.grocerListItem').click();

		cy.get('div.ant-collapse-content').within(() => {
			cy.get('span')
				.should('contain', '5 Apples')
				.should('contain', '7 Bananas');
		});
	});

	it('should have a button to accept the request', () => {
		cy.get('div.grocerListItem').click();

		cy.get('div.ant-collapse-content').within(() => {
			cy.get('button').click();
		});
	});
});
