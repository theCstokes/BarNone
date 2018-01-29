import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Tab from "UEye/Elements/Containers/Tab/Tab";
import ControlTypes from "UEye/ControlTypes";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import Core from "UEye/Elements/Core/Core";
import { BaseView } from "UEye/Elements/Core/BaseView";
import InflaterData from "UEye/Elements/Inflater/InflaterData";

class TabElement implements IListItem {
    public id: number | string;

    public title: string;

    public selected: boolean;

    public content: any[];    
}

export default class TabLayout extends BaseContainer {

    private _view: BaseView;

    private _contentElement: HTMLElement;
    private _tabButtonList: HTMLElement;

    private _tabs: TabElement[];
    private _tabContainers: BaseContainer[];
    

    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Tab-Layout");

        this._tabButtonList = Core.create("ul", this.element, "Tab-Button-List");
        this._contentElement = Core.create("div", this.element, "Content");

        this.onShow.on(view => this._view = view);
    }

    public set tabs(value: TabElement[]) {
        this._tabs = value;
        this._renderTabs();
    }
    public get tabs(): TabElement[] {
        return this._tabs;
    }

    private _renderTabs(): void {
        if (this._tabs === undefined) return;

        var data = new InflaterData();
        this._tabContainers = this._tabs.map(tab => {
            var button = Core.create("li", this._tabButtonList, "Tab-Button");
            button.textContent = tab.title;
            if (tab.selected) {
                Core.addClass(button, "Selected");
            }
            var ueyeTab = ControlTypes.Tab.create(this._contentElement, tab as any, this._view, data) as Tab;
            ueyeTab.selected = tab.selected;
            return ueyeTab;
        });
        this._view.setElements(data.componentMap);
    }
}