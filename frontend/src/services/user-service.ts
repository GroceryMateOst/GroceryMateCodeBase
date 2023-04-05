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
		return this.instance
			.post<UserModel>('register', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async loginUser(body: LoginModel) {
		return this.instance
			.post<LoginResponseModel>('login', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public logout(): void {
		localStorage.removeItem('bearerTokenGroceryMate');
		localStorage.removeItem('userEmail');
	}

	public async getUserSettings(email: string) {
		return this.instance
			.get(`settings?email=${email}`)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async updateUserSettings(
		userSettings: UserModelComplete
	): Promise<void> {
		const address = {
			street: userSettings.street,
			houseNr: userSettings.houseNr,
			zipCode: userSettings.zipCode,
			city: userSettings.city,
			state: userSettings.state,
			country: userSettings.country ?? 'Hello',
		};
		const user = {
			firstName: userSettings.firstName,
			secondName: userSettings.secondName,
			emailAddress: userSettings.emailAddress,
			residencyDetails: userSettings.residencyDetails ?? ' ',
		};
		const email = localStorage.getItem('userEmail');
		await this.instance.post('settings', { user, address, email });
	}
}
