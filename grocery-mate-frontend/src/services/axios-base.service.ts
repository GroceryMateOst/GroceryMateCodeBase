import Axios,{ AxiosInstance, AxiosResponse } from "axios";

export abstract class AxiosBaseService{
    protected instance: AxiosInstance;

    protected responseBody = (response: AxiosResponse): any => {
        return response.data;
      };

    constructor(baseUrl = '/'){
        this.instance = Axios.create({
            timeout: 15000,
            baseURL: `https://localhost:7167${baseUrl}`
        })
    }
}