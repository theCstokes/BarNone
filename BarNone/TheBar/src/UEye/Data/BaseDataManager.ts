/**
 * Auth create url string.
 */
import CallableEvent, { ICallableEvent } from "UEye/CallableEvent/CallableEvent";
import StringUtils from "UEye/Core/StringUtils";

const CREATE_URL = "/api/v1/Authorization/Create";

/**
 * Auth login url string.
 */
const AUTH_URL = "/api/v1/Authorization/Login";

/**
 * Resource url string.
 */
const RESOURCE_URL = "/api/v1/";

enum ProtocolEnum {
	HTTP = "http:",
	WS = "ws:"
}

/**
 * Base data manager.
 */
export abstract class BaseDataManager {
	/** API grant type string.*/
	private static readonly grant_type = "password";
	/** API client id. */
	private static readonly client_id = "099153c2625149bc8ecb3e85e03f0022";
	/** Auth object. */
	private static _auth: Auth | undefined;
	/** On auth expire event. */
	private static _onAuthExpire: CallableEvent = new CallableEvent();

	/**
	 * Get token from auth API.
	 * @param username - username for login
	 * @param password - password for login
	 */
	public static async authorize(username: string, password: string): Promise<boolean> {
		return BaseDataManager.authServerRequest(BaseDataManager.authorizationAddress, username, password);
	}

	public static async logout(): Promise<boolean> {
		// return BaseDataManager.authServerRequest(BaseDataManager.authorizationAddress, username, password);
		BaseDataManager._auth = undefined;
		return await true;
	}

	public static async fail(error: any): Promise<boolean> {
		if (error === 401) {
			if (BaseDataManager._auth !== undefined) {
				BaseDataManager._auth = undefined;
			}
			this._onAuthExpire.trigger();
		}

		return await true;
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
					BaseDataManager._auth = new Auth(http.responseText);
					resolve(true);
				} else if (http.readyState == 4) {
					resolve(false);
				}
			}
		});

		http.send(args);

		return result;
	}

	public static get onAuthExpire(): ICallableEvent {
		return BaseDataManager._onAuthExpire.expose();
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
	 * Notification url string.
	 */
	public static get notificationAddress(): string {
		// StringUtils.format("ws://localhost:58428?access_token={0}",
		// 	BaseDataManager.auth.access_token);

		return StringUtils.format(
			"{0}//{1}?access_token={2}",
			ProtocolEnum.WS,
			window.location.host,
			BaseDataManager.auth.access_token
		);
		// return window.location.origin + CREATE_URL;
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
	public constructor(data?: string) {
		if (data !== undefined) {
			var obj = JSON.parse(data);
			this.access_token = obj.access_token;
			this.expires_in = obj.expires_in;
			this.token_type = obj.token_type;
		}
	}
	/** Auth token */
	private _access_token: string;

	public set access_token(value: string) {
		if (this._access_token !== value) {
			this._access_token = value;
			var token = this._parseJwt(this._access_token);
			this.userID = parseInt(token.UserID);
		}
	}
	public get access_token(): string {
		return this._access_token;
	}

	// public token: { UserID: number };

	public userID: number;

	/** Expiry time. */
	public expires_in: number;
	/** Token type */
	public token_type: string;

	private _parseJwt(token: string) {
		var base64Url = token.split('.')[1];
		var base64 = base64Url.replace('-', '+').replace('_', '/');
		return JSON.parse(window.atob(base64));
	}
}