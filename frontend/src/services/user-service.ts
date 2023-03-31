import { AxiosBaseService } from './axios-base.service';
import { UserModel, LoginModel, LoginResponseModel } from '../models/UserModel';

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
		return data;
	}

	public logout(): void {
		localStorage.removeItem('bearerTokenGroceryMate');
	}
}
