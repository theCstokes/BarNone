import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
import { BaseView } from "UEye/Elements/Core/BaseView";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import ControlTypes from "UEye/ControlTypes";

export default class Panel extends BaseContainer {

    //#region Private DOM Element(s).
    private e_topDock: HTMLElement;
    private e_actionBar: HTMLElement;
    private e_caption: HTMLElement
    private e_mode: HTMLElement;
    private e_content: HTMLElement;
    private e_bottomDock: HTMLElement;
    //#endregion

    //#region Private Field(s).
    private _view: BaseView;
    private _caption: string;
    //#endregion

    //#region Public Constructor(s).
    constructor(parent: HTMLElement) {
        super(parent, "UEye-Panel");

        this.e_topDock = Core.create("div", this.element, "Top-Dock");
        this.e_actionBar = Core.create("div", this.e_topDock, "Action-Bar");
        this.linkComponentContainer("actionBar", this.e_actionBar);

        this.e_caption = Core.create("div", this.e_topDock, "Name");
        this.e_mode = Core.create("div", this.e_topDock, "Mode");

        this.e_content = Core.create("div", this.element, "Content");
        this.linkComponentContainer("content", this.e_content);

        this.e_bottomDock = Core.create("div", this.element, "Bottom-Dock");
        this.linkComponentContainer("dockBottom", this.e_bottomDock);

        this.onBindView.on(view => this._view = view);
    }
    //#endregion

    //#region Public Property(s).
    public set caption(value: any) {
        this._caption = value;
        this.e_caption.textContent = this._caption;
        if (!Utils.isNullOrWhitespace(this._caption)) {
            Core.addClass(this.e_topDock, "Has-Caption");
        } else {
            Core.removeClass(this.e_topDock, "Has-Caption");
        }
    }
    public get caption(): any {
        return this._caption;
    }

    public set actions(value: IconButton[]) {
        var data = new InflaterData();
        value.map(action => {
            return ControlTypes.IconButton
                .inflate(this.e_actionBar, action, this._view, data) as IconButton;
        });
        this._view.setElements(data.componentMap);
        this.setComponentContainer("content", value);
    }
    public get actions(): IconButton[] {
        return this.getComponentContainer("content");
    }

    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
    public get content(): any[] {
        return this.getComponentContainer("content");
    }

    public set dockBottom(value: any[]) {
        console.log("dock is undefined: " + (value === undefined));
        if (value !== undefined) {
            Core.addClass(this.element, "Has-Bottom-Dock");
        } else {
            Core.removeClass(this.element, "Has-Bottom-Dock");
        }

        this.setComponentContainer("dockBottom", value);
    }
    public get dockBottom(): any[] {
        return this.getComponentContainer("dockBottom");
    }
    //#endregion

    // // Region Protected Member(s).
    public onModifiedChange(): void {
        if (this.modified) {
            Core.addClass(this.element, "Modified");
            this.e_mode.textContent = "Modified";
        } else {
            Core.removeClass(this.element, "Modified");
            this.e_mode.textContent = "";
        }
    }
    // protected onModify(): void {

    // }

    // protected onReadonly(): void {

    // }
    // // End Region

    // // Region Private Member(s).
    // private renderMode(mode: boolean, flag: string) {
    //     if (mode) {
    //         Core.addClass(this.e_topDock, flag);
    //         this.e_mode.textContent = flag;
    //         Core.addClass(this.e_mode, flag);
    //     } else {
    //         Core.removeClass(this.e_topDock, flag);
    //         this.e_mode.textContent = "";
    //         Core.removeClass(this.e_mode, flag);
    //     }
    // }

}