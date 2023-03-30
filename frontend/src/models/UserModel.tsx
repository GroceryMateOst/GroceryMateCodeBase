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
