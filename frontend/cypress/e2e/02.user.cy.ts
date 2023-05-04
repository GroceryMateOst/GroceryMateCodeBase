/// <reference types="cypress" />

import { constants } from '../fixtures/constants';

describe('User registers and gets a JWT token in return which is stored in localstorage, changes his user-data then a logout is performed to ensure token is deleted', () => {
	it('Opens http://localhost:3000', () => {
		cy.visit('http://localhost:3000');
	});

	it('Navigates to registration', () => {
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Registration').click();
		cy.url().should('include', '/register');
	});

	it('Fills out the registration form', () => {
		cy.get('#basic_email').type(constants.EMAIL);
		cy.get('#basic_firstname').type(constants.FIRST_NAME);
		cy.get('#basic_name').type(constants.LAST_NAME);
		cy.get('#basic_password').type(constants.PASSWORD);
		cy.get('#basic_confirm').type(constants.PASSWORD);
		cy.get('button[type="submit"]').click();
		cy.url().should('eq', 'http://localhost:3000/');
	});

	it('Stores JWT token in local storage', () => {
		const token = localStorage.getItem('bearerTokenGroceryMate');
		expect(token).to.not.equal('');
	});

	it('Shows logout but no registration & login in dropdown menu', () => {
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Logout').should('exist');
		cy.get('.ant-dropdown').contains('Registration').should('not.exist');
		cy.get('.ant-dropdown').contains('Login').should('not.exist');
	});

	it('Displays user icon that links to profile on the top right', () => {
		cy.get('.anticon-user')
			.should('exist')
			.parent()
			.should('have.attr', 'href', '/profile');
	});

	it('opens the user settings and ensures the registration data is filled', () => {
		cy.get('.anticon-user').click();
		cy.get('#basic_emailAddress').should('have.value', constants.EMAIL);
		cy.get('#basic_firstName').should('have.value', constants.FIRST_NAME);
		cy.get('#basic_secondName').should('have.value', constants.LAST_NAME);
	});

	it('should save user settings and display them after navigating away', () => {
		cy.get('#basic_emailAddress').clear().type(constants.EMAIL);
		cy.get('#basic_firstName').clear().type(constants.FIRST_NAME);
		cy.get('#basic_secondName').clear().type(constants.LAST_NAME);
		cy.get('#basic_street').clear().type(constants.STREET);
		cy.get('#basic_houseNr').clear().type(constants.NUMBER);
		cy.get('#basic_zipCode').clear().type(constants.ZIP);
		cy.get('#basic_residencyDetails').clear().type(constants.DETAILS);

		cy.get('button[type="submit"]').click();

		cy.visit('/');
		cy.visit('/profile');

		cy.get('#basic_emailAddress').should('have.value', constants.EMAIL);
		cy.get('#basic_firstName').should('have.value', constants.FIRST_NAME);
		cy.get('#basic_secondName').should('have.value', constants.LAST_NAME);
		cy.get('#basic_street').should('have.value', constants.STREET);
		cy.get('#basic_houseNr').should('have.value', constants.NUMBER);
		cy.get('#basic_zipCode').should('have.value', constants.ZIP);
		cy.get('#basic_city').should('have.value', constants.CITY);
		cy.get('#basic_state').should('have.value', constants.STATE);
		cy.get('#basic_residencyDetails').should('have.value', constants.DETAILS);
	});

	it('Performs logout', () => {
		cy.visit('/');
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Logout').click();
		cy.url().should('eq', 'http://localhost:3000/');
	});

	it('deleted the jwt token', () => {
		const token = localStorage.getItem('bearerTokenGroceryMate');
		expect(token).to.equal(null);
	});
});

export {};
