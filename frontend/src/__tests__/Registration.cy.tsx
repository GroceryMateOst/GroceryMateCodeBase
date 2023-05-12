import Registration from '../pages/RegistrationPage';
import { mount } from '@cypress/react18';
import { BrowserRouter as Router } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from '../redux/store';

describe('Registration component', () => {
	beforeEach(() => {
		mount(
			<Router>
				<Provider store={store}>
					<Registration />
				</Provider>
			</Router>
		);
	});

	it('Displays error message for invalid email address on blur', () => {
		cy.get('#basic_email').type('invalidemail').blur();
		cy.get('.ant-form-item-explain-error')
			.should('be.visible')
			.contains('Das ist eine ungültige E-Mail Adresse!');
	});

	it('Displays error message for non matching passwords', () => {
		cy.get('#basic_password').type('HALLO2023*').blur();
		cy.get('#basic_confirm').type('HALLO2023').blur();
		cy.get('.ant-form-item-explain-error')
			.should('be.visible')
			.contains('Die beiden Passwörter stimmen nicht überein!');
	});
});
