import { Auth, BaseDataManager } from "Vee/Data/BaseDataManager";
import RequestBuilder from "Vee/Data/RequestBuilder";
import StringUtils from "Vee/Core/StringUtils";
import { Filter, FilterBuilder } from "Vee/Core/FilterBuilder";

class LoadResult<TSource> {
	public count: number;
	public entities: TSource[];
}

export class WhereRequest {

}

export class LoadOptions {
	public filter?: WhereRequest[];
}

export default class Resource<TSource> {
	private _route: string;

	public constructor(route: string) {
		this._route = route;
	}

	public async load<T>(f?: Filter<T>): Promise<TSource[]> {
		var builder = RequestBuilder
			.GET(BaseDataManager.resourceAddress + this._route)
			.header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

		if (f !== undefined) {
			builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(f)));
		}

		var result = await builder.execute();

		var data: LoadResult<TSource> = JSON.parse(result);

		return data.entities;
	}

	public async update(id: number, source: TSource): Promise<TSource[]> {
		var result = await RequestBuilder
			.PUT(StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._route, id), {
				id: id
			})
			.header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
			.header("Content-Type", "application/json")
			.execute(source);

		var data: LoadResult<TSource> = JSON.parse(result);

		return data.entities;
	}
}