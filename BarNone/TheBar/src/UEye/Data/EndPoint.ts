import { BaseDataManager } from "UEye/Data/BaseDataManager";

class ListResult<TSource> {
	public count: number;
	public entities: TSource[];
}

class EntityResult<TSource> {
	public entity: TSource;
}

const RESOURCE_URL = "/api/v1/";

export class EndPoint<TSource, TResult> {
	private _path: string;

	public constructor(path: string) {
		this._path = path;
	}

	public get resource(): EndPointResource<TSource, TResult> {
		return new EndPointResource(this._path);
	}
}

export class EndPointResource<TSource, TResult> {
	private _path: string;
	private _verb: string;
	private _params: { [key: string]: string } = {};

	public constructor(path: string) {
		this._path = path;
	}

	public param(key: string, value: string): EndPointResource<TSource, TResult> {
		this._params[key] = value;
		return this;
	}

	public async all(): Promise<TResult[]> {
		this._verb = "GET";

		var result = await this.execute();
		return (result as ListResult<TResult>).entities;
	}

	public async single(): Promise<TResult> {
		this._verb = "GET";

		var result = await this.execute();
		return (result as EntityResult<TResult>).entity;
	}

	public async update(data: TSource): Promise<TResult> {
		this._verb = "PUT";

		var result = await this.execute(data);
		return (result as EntityResult<TResult>).entity;
	}

	public async create(data: TSource): Promise<TResult> {
		this._verb = "POST";

		var result = await this.execute(data);
		console.log(result);
		return (result as TResult); //fix this server side but leave it for now.
	}

	private async execute(data: any = null): Promise<any> {
		return new Promise<any>((resolve, reject) => {
			var xhr = new XMLHttpRequest();
			xhr.open(this._verb, this.getRoute(), true);

			xhr.setRequestHeader("Content-Type", "application/json");
			xhr.setRequestHeader("Authorization", "Bearer " + BaseDataManager.auth.access_token);

			// for (var key in this._headers) {
			// 	if (!this._headers.hasOwnProperty(key)) continue;
			// 	xhr.setRequestHeader(key, this._headers[key]);
			// }

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

	private getRoute() {
		var route = this._path.slice(0);

		// Set Params.
		for (var key in this._params) {
			var routeKey = "{" + key + "}";
			route = route.replace(routeKey, this._params[key]);
		}

		//  Remove Unused Params.
		route = route.replace(
			new RegExp("\{[a-zA-Z0-9]\}", "g"),
			""
		);

		return window.location.origin + RESOURCE_URL + route;
	}
}