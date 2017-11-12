import Loader from "Vee/Elements/Core/Loader";
import { BaseDataOverride } from "Vee/Data/BaseDataOverride";

type Verb = "GET" | "PUT" | "POST" | "PATCH" | "DELETE";

export default class RequestBuilder {
	private _verb: Verb;
	private _route: string;
	private _headers: { [key: string]: string };

	private constructor(verb: Verb, route: string) {
		this._verb = verb;
		this._route = route;
		this._headers = {};
	}

	public static GET(route: string): RequestBuilder {
		return new RequestBuilder("GET", route);
	}

	public static PUT(route: string, args: { [key: string]: any } = {}): RequestBuilder {
		for (var key in args) {
			var routeKey = "{" + key + "}";
			route = route.replace(routeKey, args[key]);
		}
		return new RequestBuilder("PUT", route);
	}

	public header(key: string, value: string): RequestBuilder {
		this._headers[key] = value;
		return this;
	}

	public async execute(data: any = null, useOverride: boolean = false): Promise<string> {
		if (useOverride) {
			console.warn(this._route);
			var dataOverride: BaseDataOverride<any> = await Loader.sync(this._route);
			return JSON.stringify(dataOverride.data);
		}
		return await this._executeAPI(data);
	}

	public async _executeAPI(data: any = null): Promise<string> {
		return new Promise<string>((resolve, reject) => {
			var xhr = new XMLHttpRequest();
			xhr.open(this._verb, this._route, true);

			for (var key in this._headers) {
				if (!this._headers.hasOwnProperty(key)) continue;
				xhr.setRequestHeader(key, this._headers[key]);
			}

			xhr.onload = () => {
				if (xhr.readyState === 4) {
					if (xhr.status === 200) {
						resolve(xhr.responseText);
					} else {
						console.warn(xhr.statusText);
						reject();
					}
				}
			};

			xhr.onerror = () => {
				console.warn(xhr.statusText);
				reject();
			};

			xhr.send(JSON.stringify(data));
		});
	}
}