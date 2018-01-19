/**
 * Auth create url string.
 */
const CREATE_URL = "/api/v1/Authorization/Create";

/**
 * Auth login url string.
 */
const AUTH_URL = "/api/v1/Authorization/Login";

/**
 * Resource url string.
 */
const RESOURCE_URL = "/api/v1/";

/**
 * Base data manager.
 */
export abstract class BaseDataManager {
	/**
	 * API grant type string.
	 */
	private static readonly grant_type = "password";

	/**
	 * API client id.
	 */
	private static readonly client_id = "099153c2625149bc8ecb3e85e03f0022";

	/**
	 * Auth object.
	 */
	private static _auth: Auth;

	/**
	 * Get token from auth API.
	 * @param username - username for login
	 * @param password - password for login
	 */
	public static async authorize(username: string, password: string): Promise<boolean> {
		return BaseDataManager.authServerRequest(BaseDataManager.authorizationAddress, username, password);
	}

	/**
	 * Create user and get token from auth API.
	 * @param username - username for create
	 * @param password - password for create
	 */
	public static async create(username: string, password: string): Promise<boolean> {
		return BaseDataManager.authServerRequest(BaseDataManager.creationAddress, username, password);
	}

	/**
	 * Submit auth request. 
	 * @param path - path to auth type
	 * @param username - username
	 * @param password - password
	 */
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

	/**
	 * Get auth object.
	 * Empty auth if login or create has not been called.
	 */
	public static get auth(): Auth {
		if (this._auth === undefined) return new Auth();
		return this._auth;
	}

	/**
	 * Auth url string.
	 */
	public static get authorizationAddress(): string {
		return window.location.origin + AUTH_URL;
	}

	/**
	 * Create url string.
	 */
	public static get creationAddress(): string {
		return window.location.origin + CREATE_URL;
	}

	/**
	 * Resource url string.
	 */
	public static get resourceAddress(): string {
		return window.location.origin + RESOURCE_URL;
	}
}

/**
 * Auth object.
 */
export class Auth {
	/**
	 * Auth token
	 */
	public access_token: string;

	/**
	 * Expiry time.
	 */
	public expires_in: number;

	/**
	 * Token type
	 */
	public token_type: string;
}