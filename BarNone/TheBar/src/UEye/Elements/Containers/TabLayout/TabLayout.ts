import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Tab from "UEye/Elements/Containers/Tab/Tab";
import ControlTypes from "UEye/ControlTypes";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import Core from "UEye/Elements/Core/Core";
import { BaseView } from "UEye/Elements/Core/BaseView";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
import StringUtils from "UEye/Core/StringUtils";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import { View } from "UEye/View/View";

class TabConfig implements IListItem {
    public id: number | string;

    public title: string;

    public selected: boolean;

    public content: any[];

    public actions?: any[];
}

class TabButtonContentBind {
    public button: HTMLElement;

    public content: Tab;

    public actions?: IconButton[];
}

export default class TabLayout extends BaseContainer {

    private _view: BaseView;

    private e_content: HTMLElement;
    private e_tabList: HTMLElement;
    private e_actionBar: HTMLElement;
    private e_tabBar: HTMLElement;

    private _tabElements: TabConfig[];
    private _tabContainers: TabButtonContentBind[];
    private _onClickCallback: OnClickCallback;


    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Tab-Layout");

        this.e_tabBar = Core.create("div", this.element, "Tab-Bar");
        this.e_tabList = Core.create("ul", this.e_tabBar, "Tab-Button-List");
        this.e_actionBar = Core.create("div", this.e_tabBar, "Action-Bar");

        this.e_content = Core.create("div", this.element, "Content");


        this.onBindView.on(view => {

            // window.addEventListener('resize', () => {
            //     console.log("rrrrrrr");
            // }, true);

            this._view = view;
            // var height = (this.element.parentElement!.offsetHeight - this.element.offsetTop);

            var top = (this.e_tabList.offsetTop + this.e_tabList.offsetHeight);
            this.e_content.style.top = StringUtils.format("{0}px", top);

            // this.element.style.height = StringUtils.format("{0}px", height);
            // this._contentElement.style.height = StringUtils.format("{0}px", height - 32);
        });
    }

    public set tabs(value: TabConfig[]) {
        this._tabElements = value;
        this._renderTabs();
    }
    public get tabs(): TabConfig[] {
        return this._tabElements;

    }
    public set view(value: View) {
        this.onBindView.on(value => {

            // window.addEventListener('resize', () => {
            //     console.log("rrrrrrr");
            // }, true);

            this._view = value;
            // var height = (this.element.parentElement!.offsetHeight - this.element.offsetTop);

            var top = (this.e_tabList.offsetTop + this.e_tabList.offsetHeight);
            this.e_content.style.top = StringUtils.format("{0}px", top);

            // this.element.style.height = StringUtils.format("{0}px", height);
            // this._contentElement.style.height = StringUtils.format("{0}px", height - 32);
        });
    }
    private _renderTabs(): void {
        if (this._tabElements === undefined) return;

        var data = new InflaterData();
        this._tabContainers = this._tabElements.map((tabManager, index) => {
            var button = Core.create("li", this.e_tabList, "Tab-Button");
            button.textContent = tabManager.title;
            if (index == 0) {
                tabManager.selected = true;
            }
            if (tabManager.selected) {
                Core.addClass(button, "Selected");
            } else if (tabManager.selected == null) {
                tabManager.selected = false;
            }
            var ueyeTab = ControlTypes.Tab.inflate(this.e_content, tabManager as any, this._view, data) as Tab;
            ueyeTab.selected = tabManager.selected;
            button.onclick = this.onClickHandler.bind(this);

            ueyeTab.onModified = (modified) => {
                this.modified = modified;
            }

            var newTab = new TabButtonContentBind();
            newTab.button = button;
            newTab.content = ueyeTab;
            newTab.actions = tabManager.actions && tabManager.actions.map(action => {
                return ControlTypes.IconButton
                    .inflate(this.e_actionBar, action, this._view, data) as IconButton;
            });

            return newTab;

        });
        this._view.setElements(data.componentMap);
        console.log("Tab Container", this._tabContainers);
        ;
    }

    private onClickHandler(e: Event): void {
        this._tabContainers.forEach(tc => {
            if (tc.button === e.target) {
                tc.content.selected = true;
                Core.replaceClass(tc.button, "Tab-Button", "Tab-Button Selected");
                tc.actions && tc.actions.forEach(a => a.visible = true);
            }
            else {
                Core.replaceClass(tc.button, "Tab-Button Selected", "Tab-Button");
                tc.content.selected = false;
                tc.actions && tc.actions!.forEach(a => a.visible = false);
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

    public onModifiedChange() {
        if (this.modified) {
            Core.addClass(this.element, "Modified");
        } else {
            Core.removeClass(this.element, "Modified");
        }
    }
}