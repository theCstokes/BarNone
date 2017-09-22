import { Auth, BaseDataManager } from "Vee/Data/BaseDataManager";
import RequestBuilder from "Vee/Data/RequestBuilder";

export default class Resource<TSource> {
	private _route: string;

	public constructor(route: string) {
		this._route = route;
	}

	public async load(): Promise<TSource[]> {
		var result = await RequestBuilder
			.GET(BaseDataManager.resourceAddress + this._route)
			.header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
			.execute();

		var data: TSource[] = JSON.parse(result);

		return data;
	}
}