import { BaseDataOverride } from "UEye/Data/BaseDataOverride";
import Loader from "UEye/Loader";

/**
 * REST API verbs
 */
type Verb = "GET" | "PUT" | "POST" | "PATCH" | "DELETE";

/**
 * REST API request builder.
 */
export class BaseRequestBuilder {
	/**
	 * REST verb.
	 */
	private _verb: Verb;

	/**
	 * Endpoint resource name.
	 */
	private _resource: string;

	/**
	 * Route to the api.
	 */
	private _route: string;

	/**
	 * Request headers.
	 */
	private _headers: { [key: string]: string };

	/**
	 * Initialize base request builder
	 * @param resource - endpoint resource name
	 * @param verb - REST verb
	 * @param route - api route
	 */
	public constructor(resource: string, verb: Verb, route: string) {
		this._resource = resource;
		this._verb = verb;
		this._route = route;
		this._headers = {};
	}	

	/**
	 * Endpoint resource name.
	 */
	public get resource(): string {
		return this._resource;
	}

	/**
	 * Add request header.
	 * @param key - header name.
	 * @param value - header value.
	 */
	public header(key: string, value: string): BaseRequestBuilder {
		this._headers[key] = value;
		return this;
	}

	/**
	 * Submit api request
	 * @param data - data to send
	 */
	public async execute(data: any = null): Promise<any> {
		return new Promise<any>((resolve, reject) => {
			var xhr = new XMLHttpRequest();
			xhr.open(this._verb, this._route, true);

			for (var key in this._headers) {
				if (!this._headers.hasOwnProperty(key)) continue;
				xhr.setRequestHeader(key, this._headers[key]);
			}

			xhr.onload = () => {
				if (xhr.readyState === 4) {
					if (xhr.status === 200) {
						resolve(JSON.parse(xhr.responseText));
					} else {
						console.warn(xhr.statusText);
						reject(xhr.status);
					}
				}
			};

			xhr.onerror = () => {
				console.warn(xhr.statusText);
				reject(xhr.status);
			};

			xhr.send(JSON.stringify(data));
		});
	}
}

/**
 * Get request builder.
 */
export class GetRequestBuilder extends BaseRequestBuilder {
	/**
	 * Use local data override flag.
	 */
	private _useOverride: boolean;

	/**
	 * Initialize GET request builder
	 * @param resource - resource name
	 * @param verb - verb
	 * @param route - api route
	 * @param useOverride - use local data override flag
	 */
	public constructor(resource: string, verb: Verb, route: string, useOverride: boolean) {
		super(resource, verb, route);
		this._useOverride = useOverride;
	}

	/**
	 * Submit api request
	 * @param data - data object
	 */
	public async execute(data: any = null): Promise<any> {
		if (this._useOverride) {
			var filePath = "App/Data/DataOverride/api/v1/" + this.resource;
			var dataOverride: any = await Loader.sync(filePath);

			if (dataOverride === undefined) return "";

			var DataOverrideType: { new(): BaseDataOverride<any> } = dataOverride.default;
			var override = new DataOverrideType();
			return override.listResult;
		}
		return await super.execute(data);
	}
}

/**
 * Put Request builder.
 */
export class PutRequestBuilder extends BaseRequestBuilder {
	/**
	 * Initialize PUT request builder.
	 * @param resource - resource
	 * @param verb - verb
	 * @param route - route for api
	 * @param args - args for route
	 */
	public constructor(resource: string, verb: Verb, route: string,  args: { [key: string]: any } = {}) {
		for (var key in args) {
			var routeKey = "{" + key + "}";
			route = route.replace(routeKey, args[key]);
		}

		super(resource, verb, route);
	}
}

/**
 * Request builder helpers.
 */
export class RequestBuilder {
	/**
	 * Create GET request builder for resource, route.
	 * @param resource - resource to use
	 * @param route - route to api
	 * @param useOverride - use local data override flag
	 */
	public static GET(resource: string, route: string, useOverride: boolean): BaseRequestBuilder {
		return new GetRequestBuilder(resource, "GET", route, useOverride);
	}

	/**
	 * Create PUT request builder for resource, route.
	 * @param resource - resource to use
	 * @param route - route to api
	 * @param args - args for route
	 */
	public static PUT(resource: string, route: string, args: { [key: string]: any } = {}): BaseRequestBuilder {
		return new PutRequestBuilder(resource, "PUT", route);
	}

	public static POST(resource: string, route: string, args: { [key: string]: any } = {}): BaseRequestBuilder {
		return new PutRequestBuilder(resource, "POST", route);
	}
}