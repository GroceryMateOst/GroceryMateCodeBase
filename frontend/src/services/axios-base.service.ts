import Axios, { AxiosInstance, AxiosResponse } from 'axios';

export abstract class AxiosBaseService {
	protected instance: AxiosInstance;

	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	protected responseBody = (response: AxiosResponse): any => {
		return response.data;
	};

	constructor(baseUrl = '/') {
		this.instance = Axios.create({
			timeout: 15000,
			// eslint-disable-next-line @typescript-eslint/restrict-template-expressions
			baseURL: `${import.meta.env.VITE_SERVER_BASE_URL}${baseUrl}`,
		});
		this.instance.interceptors.request.use((config) => {
			const token = localStorage.getItem('bearerTokenGroceryMate');
			if (token) {
				config.headers.Authorization = `Bearer ${token}`;
			}
			return config;
		});
	}
}
