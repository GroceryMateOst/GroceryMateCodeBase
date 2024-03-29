/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
	theme: {
		extend: {
			colors: {
				primary: '#8fb69c',
				secondary: '#D9D9D9',
			},
		},
	},
	plugins: [require('daisyui')],
	corePlugins: {
		preflight: false,
	},
	daisyui: {
		themes: false,
	},
};
