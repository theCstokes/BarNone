import { IDataEvent } from "UEye/Core/DataEvent/IDataEvent";

/**
 * Data event manager.
 */
export default class DataEvent<T> implements IDataEvent<T> {
	/**
	 * Registered event handlers.
	 */
	private handlers: { (data?: T): void; }[];

	/**
	 * Initialize data event.
	 */
	public constructor() {
		this.handlers = [];
	}

	/**
	 * Add handler for event.
	 * @param handler - the event handler
	 */
	public on(handler: { (data: T): void }): void {
		this.handlers.push(handler);
	}

	/**
	 * Remove handler for event
	 * @param handler - the event handler
	 */
	public off(handler: { (data: T): void }): void {
		this.handlers = this.handlers.filter(h => h !== handler);
	}

	/**
	 * Trigger event with data object
	 * @param data - data object
	 */
	public trigger(data: T) {
		this.handlers.slice(0).forEach(h => h(data));
	}

	/**
	 * Expose public event interface.
	 */
	public expose(): IDataEvent<T> {
		return this;
	}
}