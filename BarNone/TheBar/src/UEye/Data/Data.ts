import { Filter, FilterBuilder } from "UEye/Core/FilterBuilder";
// import RequestBuilder from "UEye/Data/RequestBuilder";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import { RequestBuilder } from "UEye/Data/RequestBuilder";
import StringUtils from "UEye/Core/StringUtils";

class ListResult<TSource> {
    public count: number;
    public entities: TSource[];
}

class EntityResult<TSource> {
    public entity: TSource;
}

export interface ILoadOptions<T> {
    filter?: Filter<T>;
}

export interface ILoadDetailOptions<T> {
    filter?: Filter<T>;
    includeDetails?: boolean;
}

export class BaseResource {
    protected _resource: string;
}

export class Resource<TData> extends BaseResource {
    private _useOverride: boolean;

    public constructor(resource: string, useOverride: boolean = false) {
        super();
        this._resource = resource;
        this._useOverride = useOverride;
    }

    public async all(pOptions?: ILoadOptions<TData>): Promise<TData[]> {
        var options: ILoadOptions<TData> = {};
        if (pOptions !== undefined) options = pOptions;

        var route = StringUtils.format("{0}{1}", BaseDataManager.resourceAddress, this._resource);

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        if (options.filter !== undefined) {
            builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
        }

        var result = await builder.execute();

        var data: ListResult<TData> = JSON.parse(result);

        return data.entities;
    }

    public async single(id: number): Promise<TData> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        var result = await builder.execute();

        var data: EntityResult<TData> = JSON.parse(result);

        return data.entity;
    }

    // public create(data: TData) {

    // }

    public async update(id: number, source: TData): Promise<TData[]> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var result = await RequestBuilder
            .PUT(this._resource, route, { id: id })
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
            .header("Content-Type", "application/json")
            .execute(source);

        var data: ListResult<TData> = JSON.parse(result);

        return data.entities;
    }
}

export class DetailResource<TData> extends BaseResource {
    private _useOverride: boolean;

    public constructor(resource: string, useOverride: boolean = false) {
        super();
        this._resource = resource;
        this._useOverride = useOverride;
    }

    public async all(pOptions?: ILoadDetailOptions<TData>): Promise<TData[]> {
        var options: ILoadDetailOptions<TData> = {};
        if (pOptions !== undefined) options = pOptions;

        var route = StringUtils.format("{0}{1}", BaseDataManager.resourceAddress, this._resource);

        if (options.includeDetails) {
            route = StringUtils.format("{0}/{1}", route, "Details");
        }

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        if (options.filter !== undefined) {
            builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
        }

        var result = await builder.execute();

        var data: ListResult<TData> = JSON.parse(result);

        return data.entities;
    }

    public async single(id: number, pOptions?: ILoadDetailOptions<TData>): Promise<TData> {
        var options: ILoadDetailOptions<TData> = {};
        if (pOptions !== undefined) options = pOptions;

        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        if (options.includeDetails) {
            route = StringUtils.format("{0}/{1}", route, "Details");
        }

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        if (options.filter !== undefined) {
            builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
        }

        var result = await builder.execute();

        var data: EntityResult<TData> = JSON.parse(result);

        return data.entity;
    }

    public async update(id: number, source: TData): Promise<TData[]> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var result = await RequestBuilder
            .PUT(this._resource, route, { id: id })
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
            .header("Content-Type", "application/json")
            .execute(source);

        var data: ListResult<TData> = JSON.parse(result);

        return data.entities;
    }
}

// interface  {
//     all(): any[];
//     single(id: number): any[];

//     create(data: any);
//     update(data: any);


// }