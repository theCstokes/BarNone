import Core from "Vee/Elements/Core/Core";

export abstract class BaseElement {
	private _element: HTMLElement;
	private _id: string;
	private _modified: boolean;
	private _readonly: boolean;
	private _error: string;

	public constructor(parent: HTMLElement, ...styles: string[]) {
		this._element = Core.create('div', parent, ...styles);
	}

	public get element(): HTMLElement {
		return this._element;
	}

	public get id(): string {
		return this._id;
	}
	public set id(value: string) {
		this._id = value;
	}

	public get modified(): boolean {
		return this._modified;
	}
	public set modified(value: boolean) {
		this._modified = value;
		this.onModifiedChange();
	}

	public get readonly(): boolean {
		return this._readonly;
	}
	public set readonly(value: boolean) {
		this._readonly = value;
		this.onReadonlyChange();
	}

	public get error(): string {
		return this._error;
	}
	public set error(value: string) {
		this._error = value;
		this.onErrorChange();
	}

	public onShow(): void {
		
	}

	public abstract onModifiedChange(): void;
	public abstract onReadonlyChange(): void;
	public abstract onErrorChange(): void;
}