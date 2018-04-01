interface IList {
	id?: string|number;

	style: IListItem;
}

interface IListItem {

}

class DataListItem implements IListItem {

}

class List implements IList {
	
	private _style: IListItem;

	public get id(): string|number {
		return "";
	}

	public set id(value: string|number) {
		
	}

	public get style(): IListItem {
		return this._style;
	}

	public set style(value: IListItem) {

	}
}

class ComponentBuilder<TComponent, TComponentInterface> {

}

class ComponentConfig<TComponent, TComponentInterface> {
	public instance: ComponentBuilder<TComponent, TComponentInterface>
	public init: TComponentInterface;
}

class Test {
	private ListBuilder = new ComponentBuilder<List, IList>();

	private config: ComponentConfig<List, IList>;

	public run() {
		this.config = {
			instance: this.ListBuilder,
			init: {
				style: new DataListItem()
			}
		}
	}
}

