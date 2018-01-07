import { IDataEvent } from "UEye/Core/DataEvent/IDataEvent";

export default class DataEvent<T> implements IDataEvent<T> {
	private handlers: { (data?: T): void; }[] = [];

	public on(handler: { (data?: T): void }): void {
		this.handlers.push(handler);
	}

	public off(handler: { (data?: T): void }): void {
		this.handlers = this.handlers.filter(h => h !== handler);
	}

	public trigger(data?: T) {
		this.handlers.slice(0).forEach(h => h(data));
	}

	public expose(): IDataEvent<T> {
		return this;
	}
}