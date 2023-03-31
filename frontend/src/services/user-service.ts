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
		console.log(response);
		return {
			...response.data.user,
			...response.data.address,
		};
	}

	public async updateUserSettings(
		userSettings: UserModelComplete
	): Promise<void> {
		const address = {
			street: userSettings.street,
			houseNr: parseInt(userSettings.houseNr),
			zipCode: parseInt(userSettings.zipCode),
			city: userSettings.city,
			state: userSettings.state,
			country: userSettings.country,
		};
		const user = {
			firstName: userSettings.firstName,
			secondName: userSettings.secondName,
			emailAddress: userSettings.emailAddress,
			residencyDetails: userSettings.residencyDetails,
		};
		const email = localStorage.getItem('userEmail');
		await this.instance.post('settings', { user, address, email });
	}
}
