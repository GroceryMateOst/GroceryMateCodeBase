export interface UserModel {
	userId?: string;
	emailaddress: string;
	password: string;
	secondname: string;
	firstname: string;
	residencyDetails: string;
}

export interface LoginModel {
	emailaddress: string;
	password: string;
}

export interface LoginResponseModel {
	token: string;
	expiration: string;
}

export interface UserModelGrocery {
	user: {
		userId: string;
		firstName: string;
		secondName: string;
		emailAddress: string;
		residencyDetails: string;
	};
	address: {
		street: string;
		houseNr: string;
		zipCode: string;
		city: string;
		state: string;
		latitude: number;
		longitude: number;
	};
}

export interface UserModelComplete {
	emailAddress: string;
	firstName: string;
	secondName: string;
	street: string;
	houseNr: string;
	zipCode: string;
	city: string;
	state: string;
	residencyDetails: string;
}
