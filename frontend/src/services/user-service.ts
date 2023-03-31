import { AxiosBaseService } from './axios-base.service';
import {
	UserModel,
	LoginModel,
	LoginResponseModel,
	UserModelComplete,
} from '../models/UserModel';

export default class UserService extends AxiosBaseService {
	constructor() {
		super('/User');
	}

	public async registerAccount(body: UserModel) {
		return this.instance.post<UserModel>('register', body);
	}

	public async loginUser(body: LoginModel) {
		const { data } = await this.instance.post<LoginResponseModel>(
			'login',
			body
		);
		const token: string = data.token;
		localStorage.setItem('bearerTokenGroceryMate', token);
		// ToDo is to replace by using only token.
		localStorage.setItem('userEmail', body.emailaddress);
		return data;
	}

	public logout(): void {
		localStorage.removeItem('bearerTokenGroceryMate');
		localStorage.removeItem('userEmail');
	}

	public async getUserSettings(email: string): Promise<UserModelComplete> {
		const response = await this.instance.get(`settings?email=${email}`);
		return {
			...response.data.user,
			...response.data.address,
			residencyDetails: '',
		};
	}
}
