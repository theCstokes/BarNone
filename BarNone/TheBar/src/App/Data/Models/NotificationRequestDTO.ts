import { Filter, FilterBuilder, IFilterHeader } from "UEye/Core/FilterBuilder";

export default class NotificationRequestDTO<T> {
	public constructor(init: Partial<NotificationRequestDTO<T>>) {
		Object.assign(this, init);
	}

	public type: string;
	
	public filter: Filter<T>;

	public create(): { type: string, filter: IFilterHeader } {
		return {
			type: this.type,
			filter: FilterBuilder.getHeader(this.filter)
		}
	}
}