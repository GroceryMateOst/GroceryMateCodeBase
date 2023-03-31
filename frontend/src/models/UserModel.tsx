export interface UserModel {
	emailaddress: string;
	password: string;
	secondname: string;
	firstname: string;
}

export interface LoginModel {
	emailaddress: string;
	password: string;
}

export interface LoginResponseModel {
	token: string;
	expiration: string;
}

export interface UserModelComplete {
	emailAddress: string;
	firstName: string;
	secondName: string;
	street: string;
	houseNr: number;
	zipCode: number;
	city: string;
	state: string;
	country: string;
	residencyDetails: string;
}
