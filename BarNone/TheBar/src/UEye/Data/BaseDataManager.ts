// import Resource from "UEye/Data/Resource";

const CREATE_URL = "/api/v1/Authorization/Create";
const AUTH_URL = "/api/v1/Authorization/Login";
const RESOURCE_URL = "/api/v1/";

export abstract class BaseDataManager {

	private static readonly grant_type = "password";
	private static readonly client_id = "099153c2625149bc8ecb3e85e03f0022";

	private static _auth: Auth;

	public static async authorize(username: string, password: string): Promise<boolean> {
		return BaseDataManager.authServerRequest(BaseDataManager.authorizationAddress, username, password);
	}

	public static async create(username: string, password: string): Promise<boolean> {
		return BaseDataManager.authServerRequest(BaseDataManager.creationAddress, username, password);
	}

	private static authServerRequest(path: string, username: string, password: string): Promise<boolean> {
		var http = new XMLHttpRequest();

		var args = "userName=" + username +
			"&password=" + password;

		http.open("POST", path, true);
		http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

		var result = new Promise<boolean>((resolve, reject) => {
			http.onreadystatechange = function () {
				if (http.readyState == 4 && http.status == 200) {
					BaseDataManager._auth = JSON.parse(http.responseText);
					resolve(true);
				} else if (http.readyState == 4) {
					resolve(false);
				}
			}
		});

		http.send(args);

		return result;
	}

	public static get auth(): Auth {
		if (this._auth === undefined) return new Auth();
		return this._auth;
	}

	public static get authorizationAddress(): string {
		return window.location.origin + AUTH_URL;
	}

	public static get creationAddress(): string {
		return window.location.origin + CREATE_URL;
	}

	public static get resourceAddress(): string {
		return window.location.origin + RESOURCE_URL;
	}
}

export class Auth {
	public access_token: string;
	public expires_in: number;
	public token_type: string;
}