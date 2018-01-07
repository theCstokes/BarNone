import Loader from "Vee/Elements/Core/Loader";
import { BaseDataOverride } from "Vee/Data/BaseDataOverride";

type Verb = "GET" | "PUT" | "POST" | "PATCH" | "DELETE";

export default class RequestBuilder {
	private _verb: Verb;
	private _resource: string;
	private _route: string;
	private _headers: { [key: string]: string };

	private constructor(resource: string, verb: Verb, route: string) {
		this._resource = resource;
		this._verb = verb;
		this._route = route;
		this._headers = {};
	}

	public static GET(resource: string, route: string): RequestBuilder {
		return new RequestBuilder(resource, "GET", route);
	}

	public static PUT(resource: string, route: string, args: { [key: string]: any } = {}): RequestBuilder {
		for (var key in args) {
			var routeKey = "{" + key + "}";
			route = route.replace(routeKey, args[key]);
		}
		return new RequestBuilder(resource, "PUT", route);
	}

	public header(key: string, value: string): RequestBuilder {
		this._headers[key] = value;
		return this;
	}

	public async execute(data: any = null, useOverride: boolean = false): Promise<string> {
		if (useOverride) {
			var filePath = "Application/Data/DataOverride/api/v1/" + this._resource;
			var dataOverride: any = await Loader.sync(filePath);

			if (dataOverride === undefined) return "";

			var DataOverrideType: { new(): BaseDataOverride<any> } = dataOverride.default;
			var override = new DataOverrideType();
			return override.response;
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