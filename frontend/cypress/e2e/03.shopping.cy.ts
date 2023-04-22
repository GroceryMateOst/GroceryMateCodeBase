/// <reference types="cypress" />

import { constants } from '../fixtures/constants';

describe('User creates a new GroceryRequest with Multiple groceries and submits it', () => {
	it('Opens http://localhost:3000', () => {
		cy.visit('http://localhost:3000');
	});

	it('should navigate to the "Create" page when clicking the corresponding link', () => {
		cy.get('a[href*="create"]').click();
		cy.url().should('include', '/create');
	});

	it('should allow users to create a shopping list', () => {
		cy.get('#normal_login_note').type('Please buy some groceries.');
		cy.get('#normal_login_preferredstore').type('Migros');
		cy.get('button').contains('Artikel hinzufügen').click();
		cy.get('.shoppingItems').find('input').first().type('5 Apples');
		cy.get('button').contains('Artikel hinzufügen').click();
		cy.get('.shoppingItems').find('input').eq(1).type('7 Bananas');
		cy.get('button').contains('Anfrage publizieren').click();

		cy.visit('/');
		cy.visit('/create');

		cy.get('#normal_login_note').should('have.value', '');
		cy.get('#normal_login_preferredstore').should('have.value', '');
	});
});
