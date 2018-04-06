import { Filter, FilterBuilder } from "UEye/Core/FilterBuilder";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import { RequestBuilder } from "UEye/Data/RequestBuilder";
import StringUtils from "UEye/Core/StringUtils";

/**
 * Result structure for lists.
 */
class ListResult<TSource> {
    public count: number;
    public entities: TSource[];
}

/**
 * Result structure for entity.
 */
class EntityResult<TSource> {
    public entity: TSource;
}

/**
 * Load options for standard apis.
 */
export interface ILoadOptions<T> {
    /**
     * Search filter.
     */
    filter?: Filter<T>;

    params?: { [key: string]: string|number }
}

/**
 * Load options for detail apis.
 */
export interface ILoadDetailOptions<T> {
    /**
     * Search filter.
     */
    filter?: Filter<T>;

    /**
     * Include details in results flag.
     */
    includeDetails?: boolean;

    params?: { [key: string]: string|number }
}

/**
 * Base resource.
 */
export class BaseResource {
    /**
     * Resource name for api.
     */
    protected _resource: string;

    /**
     * Flag set to use local data override.
     */
    protected _useOverride: boolean;
}

/**
 * API resource for accessing endpoint.
 */
export class Resource<TData> extends BaseResource {

    /**
     * Initialize resource
     * @param resource - resource name for api.
     * @param useOverride - use local data override flag
     */
    public constructor(resource: string, useOverride: boolean = false) {
        super();
        this._resource = resource;
        this._useOverride = useOverride;
    }

    /**
     * Access GET api for this resource.
     * @param pOptions - options for request
     */
    public async all(pOptions?: ILoadOptions<TData>): Promise<TData[]> {
        var options: ILoadOptions<TData> = {};
        if (pOptions !== undefined) options = pOptions;

        var resource = StringUtils.replace(this._resource, options.params);
        var route = StringUtils.format("{0}{1}", BaseDataManager.resourceAddress, resource);

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        if (options.filter !== undefined) {
            builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
        }

        try {
            var result = await builder.execute();
            return (result as ListResult<TData>).entities;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    /**
     * Access GET by id for this resource
     * @param id - id of entity
     */
    public async single(id: number): Promise<TData> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);



        try {
            var result = await builder.execute();
            return (result as EntityResult<TData>).entity;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    /**
     * Access PUT for this resource
     * @param id - id for entity
     * @param source - data
     */
    public async update(id: number, source: TData): Promise<TData[]> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var builder = await RequestBuilder
            .PUT(this._resource, route, { id: id })
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
            .header("Content-Type", "application/json");

        try {
            var result = await builder.execute(source);;
            return (result as ListResult<TData>).entities;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    /**
     * Access POST for this resource
     * @param id - id for entity
     * @param source - data
     */
    public async post(id: number, source: TData): Promise<TData> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var builder = await RequestBuilder
            .POST(this._resource, route, { id: id })
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
            .header("Content-Type", "application/json");

        try {
            var result = await builder.execute(source);;
            return result as TData;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    
}

/**
 * API resource for accessing detail endpoint.
 */
export class DetailResource<TData> extends BaseResource {

    /**
     * Initialize resource
     * @param resource - resource name for api.
     * @param useOverride - use local data override flag
     */
    public constructor(resource: string, useOverride: boolean = false) {
        super();
        this._resource = resource;
        this._useOverride = useOverride;
    }

    /**
     * Access GET api for this resource.
     * @param pOptions - options for request
     */
    public async all(pOptions?: ILoadDetailOptions<TData>): Promise<TData[]> {
        var options: ILoadDetailOptions<TData> = {};
        if (pOptions !== undefined) options = pOptions;

        var resource = StringUtils.replace(this._resource, options.params);
        var route = StringUtils.format("{0}{1}", BaseDataManager.resourceAddress, resource);

        if (options.includeDetails) {
            route = StringUtils.format("{0}/{1}", route, "Details");
        }

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        if (options.filter !== undefined) {
            builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
        }

        try {
            var result = await builder.execute();
            return (result as ListResult<TData>).entities;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    /**
     * Access GET by id for this resource
     * @param id - id of entity
     */
    public async single(id: number, pOptions?: ILoadDetailOptions<TData>): Promise<TData> {
        var options: ILoadDetailOptions<TData> = {};
        if (pOptions !== undefined) options = pOptions;

        var resource = StringUtils.replace(this._resource, options.params);
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, resource, id);

        if (options.includeDetails) {
            route = StringUtils.format("{0}/{1}", route, "Details");
        }

        var builder = RequestBuilder
            .GET(this._resource, route, this._useOverride)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token);

        if (options.filter !== undefined) {
            builder.header("Filter", JSON.stringify(FilterBuilder.getHeader(options.filter)));
        }

        try {
            var result = await builder.execute();
            return (result as EntityResult<TData>).entity;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    /**
     * Access PUT for this resource
     * @param id - id for entity
     * @param source - data
     */
    public async update(id: number, source: Partial<TData>): Promise<TData> {
        var route = StringUtils.format("{0}{1}/{2}", BaseDataManager.resourceAddress, this._resource, id);

        var builder = await RequestBuilder
            .PUT(this._resource, route, { id: id })
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
            .header("Content-Type", "application/json");

        try {
            var result = await builder.execute(source);
            return (result as EntityResult<TData>).entity;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }

    public async create(source: Partial<TData>): Promise<TData> {
        var route = StringUtils.format("{0}{1}", BaseDataManager.resourceAddress, this._resource);

        var builder = await RequestBuilder
            .POST(this._resource, route)
            .header("Authorization", "Bearer " + BaseDataManager.auth.access_token)
            .header("Content-Type", "application/json");

        try {
            var result = await builder.execute(source);
            return (result as EntityResult<TData>).entity;
        } catch (error) {
            BaseDataManager.fail(error);
            throw error;
        }
    }
}