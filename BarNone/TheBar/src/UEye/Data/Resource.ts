import { Auth, BaseDataManager } from "UEye/Data/BaseDataManager";
import RequestBuilder from "UEye/Data/RequestBuilder";
import StringUtils from "UEye/Core/StringUtils";
import { Filter, FilterBuilder } from "UEye/Core/FilterBuilder";

class LoadResult<TSource> {
	public count: number;
	public entities: TSource[];
}

export class WhereRequest {

}

export class LoadOptions {
	public filter?: WhereRequest[];
}

export interface ILoadOptions<T> {
	filter?: Filter<T>;
	useOverride?: boolean;
}

export default class Resource<TSource> {
	private _route: string;

	public constructor(route: string) {
		this._route = route;
	}

	public async load<T>(pOptions?: ILoadOptions<T>): Promise<TSource[]> {
		var options: ILoadOptions<T> = {};
		if (pOptions !== undefined) options = pOptions;

		var builder = RequestBuilder
			.GET(this._route, BaseDataManager.resourceAddress + this._route)
			.header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

		if (options.filter !== undefined) {
			builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
		}

		var result = await builder.execute(undefined, options.useOverride === true);

		var data: LoadResult<TSource> = JSON.parse(result);

		return data.entities;
	}

	public async update(id: number, source: TSource): Promise<TSource[]> {
		var result = await RequestBuilder
			.PUT(this._route, StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._route, id), {
				id: id
			})
			.header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
			.header("Content-Type", "application/json")
			.execute(source);

		var data: LoadResult<TSource> = JSON.parse(result);

		return data.entities;
	}
}