import Axios, { AxiosInstance, AxiosResponse } from 'axios';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

export abstract class AxiosBaseService {
	protected instance: AxiosInstance;

	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	protected responseBody = (response: AxiosResponse): any => {
		return response.data;
	};

	protected errorHandling = (error: any): void => {
		const response = error?.response;
		console.log('toast1');
		if (response && response.data) {
			if (response.data.type === 'not-found') {
				const navigate = useNavigate();
				navigate('/error');
			} else {
				console.log('toast2');
				toast.error(response.data, {
					position: 'top-center',
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: 'light',
				});
			}
		} else {
			console.error(response);
		}
		throw error;
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
