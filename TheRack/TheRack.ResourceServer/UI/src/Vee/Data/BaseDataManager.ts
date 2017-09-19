import Resource from "Vee/Data/Resource";

export const AUTH_URL = "http://localhost:61761/api/v1/authorize/login";
export const DEFAULT_URL = "http://localhost:63202/api/v1/";

export abstract class BaseDataManager {

	private static readonly grant_type = "password";
	private static readonly client_id = "099153c2625149bc8ecb3e85e03f0022";

	private static _auth: Auth;

	public static async authorize(username: string, password: string): Promise<boolean> {

		var http = new XMLHttpRequest();

		var args = "username=" + username +
			"&password=" + password +
			"&grant_type=" + BaseDataManager.grant_type +
			"&client_id=" + BaseDataManager.client_id;

		http.open("POST", AUTH_URL, true);
		// http.setRequestHeader("Access-Control-Allow-Origin", "*");
    // http.setRequestHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
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
		return this._auth;
	}
}

export class Auth {
	public access_token: string;
	public expires_in: number;
	public token_type: string;
}