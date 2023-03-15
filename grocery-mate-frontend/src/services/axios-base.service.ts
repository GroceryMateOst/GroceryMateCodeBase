import Axios, { AxiosInstance, AxiosResponse } from 'axios';

export abstract class AxiosBaseService {
	protected instance: AxiosInstance;

	protected responseBody = (response: AxiosResponse): any => {
		return response.data;
	};

	constructor(baseUrl = '/') {
		// const backendURL = import.meta.env.BACKEND_URL;
		//  console.log(backendURL);
		this.instance = Axios.create({
			timeout: 15000,
			baseURL: `https://grocerymate-backend.azurewebsites.net/${baseUrl}`,
			//  baseURL: `http://${backendURL}:5000${baseUrl}`
		});
	}
}
