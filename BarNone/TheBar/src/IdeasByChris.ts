interface IList {
	id?: string | number;

	style: { new(): IListItem };
}

interface IListItem {
	id?: string | number;
}

class DataListItem implements IListItem {
	public id?: string | number;

	public constructor() {

	}
}

class List implements IList {

	private _style: { new(): IListItem };

	public get id(): string | number {
		return "";
	}

	public set id(value: string | number) {

	}

	public get style(): { new(): IListItem } {
		return this._style;
	}

	public set style(value: { new(): IListItem }) {

	}
}

class ComponentBuilder<TComponent extends TComponentInterface, TComponentInterface> {

	public constructor(
		private b: { new(): TComponent }
	) {

	}

	public bind(v: TComponentInterface): ComponentConfig<TComponent, TComponentInterface> {
		return {
			instance: this as ComponentBuilder<TComponent, TComponentInterface>,
			init: v
		} as ComponentConfig<TComponent, TComponentInterface>;
	}

}

class ComponentConfig<TComponent extends TComponentInterface, TComponentInterface> {
	public instance: ComponentBuilder<TComponent, TComponentInterface>;
	public init: TComponent & TComponentInterface;
}

class Builder {
	public static List = new ComponentBuilder<List, IList>(List);

	// private config: ComponentConfig<List, IList>;

	// public run() {
	// 	this.config = {
	// 		instance: this.ListBuilder,
	// 		init: {
	// 			style: new DataListItem()
	// 		}
	// 	}
	// }
}

abstract class BaseView {
	public abstract config: ComponentConfig<any, any>[];
}

class View extends BaseView {
	public config: ComponentConfig<any, any>[] = [
		Builder.List
			.bind({
				style: DataListItem
			})
	];

	public config2: ComponentConfig<any, any>[] = [
		{
			instance: Builder.List,
			init: {
				style: DataListItem
			}
		} as ComponentConfig<List, IList>
	];

}

