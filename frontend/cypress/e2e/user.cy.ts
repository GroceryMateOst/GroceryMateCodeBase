/// <reference types="cypress" />

describe('User registers and gets a JWT token in return which is stored in localstorage then a logout is performed to ensure token is deleted', () => {
	it('Opens http://localhost:3000', () => {
		cy.visit('http://localhost:3000');
	});

	it('Navigates to registration', () => {
		cy.get('.ant-dropdown-trigger').trigger('mouseover');
		cy.get('.ant-dropdown').contains('Registration').click();
		cy.url().should('include', '/register');
	});

	it('Fills out the registration form', () => {
		cy.get('#basic_email').type('test@test101.com');
		cy.get('#basic_firstname').type('John');
		cy.get('#basic_name').type('Doe');
		cy.get('#basic_password').type('p@ssw0rd2023A');
		cy.get('#basic_confirm').type('p@ssw0rd2023A');
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

	it('Performs logout', () => {
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
