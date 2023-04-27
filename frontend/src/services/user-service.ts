import { AxiosBaseService } from './axios-base.service';
import {
	UserModel,
	LoginModel,
	LoginResponseModel,
	UserModelComplete,
} from '../models/UserModel';
import { Address } from '../models/AddressModel';

export default class UserService extends AxiosBaseService {
	constructor() {
		super('/User');
	}

	public async registerAccount(body: UserModel): Promise<void> {
		return this.instance
			.post<UserModel>('Authentication/register', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public async loginUser(
		body: LoginModel
	): Promise<{ token: string; expiration: string; email: string }> {
		return this.instance
			.post<LoginResponseModel>('Authentication/login', body)
			.then(this.responseBody)
			.catch(this.errorHandling);
	}

	public logout(): void {
		localStorage.removeItem('bearerTokenGroceryMate');
	}

	public async getUserSettings(): Promise<{
		user: UserModel;
		address: Address;
	}> {
		return this.instance
			.get(`Settings`)
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
		};
		const user = {
			firstName: userSettings.firstName,
			secondName: userSettings.secondName,
			emailAddress: userSettings.emailAddress,
			residencyDetails: userSettings.residencyDetails ?? ' ',
		};
		return this.instance
			.post('Settings', { user, address })
			.then(this.responseBody)
			.catch(this.errorHandling);
	}
}
