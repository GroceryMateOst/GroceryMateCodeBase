import { defineConfig } from 'cypress';

export default defineConfig({
	e2e: {
		baseUrl: 'http://localhost:3000',
		setupNodeEvents(on, config) {
			// implement node event listeners here
		},
	},
	component: {
		devServer: {
			framework: 'react',
			bundler: 'vite',
		},
		setupNodeEvents(on, config) {
			//require('@cypress/code-coverage/task')(on, config);
		},
	},
});
