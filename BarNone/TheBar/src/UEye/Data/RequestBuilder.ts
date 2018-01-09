// import Loader from "UEye/Elements/Core/Loader";
import { BaseDataOverride } from "UEye/Data/BaseDataOverride";
import Loader from "UEye/Loader";
import { exec } from "child_process";

type Verb = "GET" | "PUT" | "POST" | "PATCH" | "DELETE";

export class BaseRequestBuilder {
	private _verb: Verb;
	private _resource: string;
	private _route: string;
	private _headers: { [key: string]: string };

	public constructor(resource: string, verb: Verb, route: string) {
		this._resource = resource;
		this._verb = verb;
		this._route = route;
		this._headers = {};
	}	

	public get resource(): string {
		return this._resource;
	}

	public header(key: string, value: string): BaseRequestBuilder {
		this._headers[key] = value;
		return this;
	}

	public async execute(data: any = null): Promise<string> {
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

export class GetRequestBuilder extends BaseRequestBuilder {
	private _useOverride: boolean;

	public constructor(resource: string, verb: Verb, route: string, useOverride: boolean) {
		super(resource, verb, route);
		this._useOverride = useOverride;
	}

	public async execute(data: any = null): Promise<string> {
		if (this._useOverride) {
			var filePath = "Application/Data/DataOverride/api/v1/" + this.resource;
			var dataOverride: any = await Loader.sync(filePath);

			if (dataOverride === undefined) return "";

			var DataOverrideType: { new(): BaseDataOverride<any> } = dataOverride.default;
			var override = new DataOverrideType();
			return override.response;
		}
		return await super.execute(data);
	}
}

export class PutRequestBuilder extends BaseRequestBuilder {
	public constructor(resource: string, verb: Verb, route: string,  args: { [key: string]: any } = {}) {
		for (var key in args) {
			var routeKey = "{" + key + "}";
			route = route.replace(routeKey, args[key]);
		}

		super(resource, verb, route);
	}
}

export class RequestBuilder {
	public static GET(resource: string, route: string, useOverride: boolean): BaseRequestBuilder {
		return new GetRequestBuilder(resource, "GET", route, useOverride);
	}

	public static PUT(resource: string, route: string, args: { [key: string]: any } = {}): BaseRequestBuilder {
		return new PutRequestBuilder(resource, "PUT", route);
	}
}