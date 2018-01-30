import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Tab from "UEye/Elements/Containers/Tab/Tab";
import ControlTypes from "UEye/ControlTypes";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import Core from "UEye/Elements/Core/Core";
import { BaseView } from "UEye/Elements/Core/BaseView";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";

class TabConfig implements IListItem {
    public id: number | string;

    public title: string;

    public selected: boolean;

    public content: any[];    
   
}

class TabButtonContentBind {
    public button: HTMLElement;
    public content: Tab;
}

export default class TabLayout extends BaseContainer {

    private _view: BaseView;

    private _contentElement: HTMLElement;
    private _tabList: HTMLElement;
    private _tabElements: TabConfig[];
    private _tabContainers: TabButtonContentBind[];
    private _onClickCallback: OnClickCallback;
    

    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Tab-Layout");

        this._tabList = Core.create("ul", this.element, "Tab-Button-List");
        
        this._contentElement = Core.create("div", this.element, "Content");

     
        this.onShow.on(view => this._view = view);
    }

    public set tabs(value: TabConfig[]) {
        this._tabElements = value;
        this._renderTabs();
    }
    public get tabs(): TabConfig[] {
        return this._tabElements;
    }
    
    private _renderTabs(): void {
        if (this._tabElements === undefined) return;
      
        var data = new InflaterData();
        this._tabContainers = this._tabElements.map((tabManager, index) => {
            var button = Core.create("li", this._tabList, "Tab-Button") ;
            button.textContent = tabManager.title;
            if(index==0){
                tabManager.selected=true;
            }
            if (tabManager.selected) {
                Core.addClass(button, "Selected");
            }else if (tabManager.selected==null){
                tabManager.selected=false;
            }
            var ueyeTab = ControlTypes.Tab.create(this._contentElement, tabManager as any, this._view, data) as Tab;
            ueyeTab.selected = tabManager.selected;
            button.onclick = this.onClickHandler.bind(this);
            var newTab= new TabButtonContentBind();
            newTab.button=button;
            newTab.content=ueyeTab;
            return newTab;
        
        });
        this._view.setElements(data.componentMap);
        console.log("Tab Container", this._tabContainers);
     ;
    }
   
    private onClickHandler(e: Event): void {
        this._tabContainers.forEach(tc => {
            if(tc.button === e.target){
                tc.content.selected = true;
                Core.replaceClass(tc.button,"Tab-Button", "Tab-Button Selected");
            }
            else{
                Core.replaceClass(tc.button,"Tab-Button Selected", "Tab-Button");
                tc.content.selected=false;
            }
        });
		// if (this._onClickCallback !== undefined) {
		// 	this._onClickCallback();
		// }
	}
    
    public get onClick(): OnClickCallback {
        return this._onClickCallback;
    }
    public set onClick(value: OnClickCallback) {
		this._onClickCallback = value;
	}
}