import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import { OnClickCallback } from "Vee/Elements/Core/BindTypes";

export default class Breadcrumb extends BaseComponent {

	private _crumbHolder: HTMLElement;
	private _crumbElements: HTMLElement[];
	private _items: any[];
	private _onClickCallback: OnClickCallback;

	public constructor(parent: HTMLElement) {
		super(parent);
		Core.addClass(this.element, "Vee-Breadcrumb");

		this._crumbHolder = Core.create('ul', this.element, 'Crumb-Holder');
	}

	public get items(): any[] {
		return this._items;
	}
	public set items(value: any[]) {
		this._items = value;
		this._createItems();
	}

	public get onClick(): any {
		return this._onClickCallback;
	}
	public set onClick(value: any) {
		this._onClickCallback = value;
	}

	

	public onEnabledChange(): void {
		throw new Error("Method not implemented.");
	}
	public onModifiedChange(): void {
		throw new Error("Method not implemented.");
	}
	public onReadonlyChange(): void {
		throw new Error("Method not implemented.");
	}
	public onErrorChange(): void {
		throw new Error("Method not implemented.");
	}

	private _createItems() {
		this._crumbElements = this._items.map((item, idx) => {
			var el = Core.create('li', this._crumbHolder, "Crumb");
			if (idx < (this._items.length - 1)) {
				Core.addClass(el, 'Unselected');
			}
			el.textContent = item.value;
			el.onclick = this.onClickHandler.bind(this);
			return el;
		});
	}

	private onClickHandler(): void {
		if (this._onClickCallback !== undefined) {
			this._onClickCallback();
		}
	}
}