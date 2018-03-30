import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import { OnClickCallback, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
import { Translate } from "UEye/Translate";

export default class AnalysisListItem extends BaseListItem {

    // Private property field(s).
    private _name: string;
    private _nameCaption: string;

    private _value1: string;
    private _caption1: string;

    private _value2: string;
    private _caption2: string;

    private _value3: string;
    private _caption3: string;

    // private _value: string;
    private _onActionCallback: OnClickCallback;
    private _icon: string;

    // Private dom element(s).
    private e_nameData: HTMLElement;
    private e_name: HTMLElement;
    private e_nameCaption: HTMLElement;

    private e_data1: HTMLElement;
    private e_value1: HTMLElement;
    private e_value1Caption: HTMLElement;
    
    private e_data2: HTMLElement;
    private e_value2: HTMLElement;
    private e_value2Caption: HTMLElement;
    
    private e_data3: HTMLElement;
    private e_value3: HTMLElement;
    private e_value3Caption: HTMLElement;

    // private e_value: HTMLElement;
    // private e_valueCaption: HTMLElement;
    // private e_action: HTMLElement;
    // private _openActionElement: HTMLElement;
    private e_icon: HTMLElement;

    public constructor(parent: HTMLElement) {
        super(parent, "Analysis-List-Item");

        this.e_icon = Core.create("div", this.element, "Icon fa");

        var content = Core.create("div", this.element, "Content");

        this.e_nameData = Core.create("div", content, "Data");
        this.e_name = Core.create("div", this.e_nameData, "Value");
        this.e_nameCaption = Core.create("div", this.e_nameData, "Caption");

        this.e_data1 = Core.create("div", content, "Data");
        this.e_value1 = Core.create("div", this.e_data1, "Value");
        this.e_value1Caption = Core.create("div", this.e_data1, "Caption");
        
        this.e_data2 = Core.create("div", content, "Data");
        this.e_value2 = Core.create("div", this.e_data2, "Value");
        this.e_value2Caption = Core.create("div", this.e_data2, "Caption");

        this.e_data3 = Core.create("div", content, "Data");
        this.e_value3 = Core.create("div", this.e_data3, "Value");
        this.e_value3Caption = Core.create("div", this.e_data3, "Caption");
        
        // this.e_nameCaption.textContent = Translate.AnalysisListItem.Name;

        // this.e_value = Core.create("div", valueHolder, "Value");
        // this.e_valueCaption = Core.create("div", valueHolder, "Value-Caption");
        // this.e_valueCaption.textContent = Translate.AnalysisListItem.Value;

        
        // this.e_action.style.display = 'none';
        // this.e_action.onclick = this._onActionClickHandler.bind(this);
    }

    // public set name(value: string) {
    //     this._name = value;
    //     this.e_name.textContent = this._name;
    // }
    // public get name(): string {
    //     return this._name;
    // }

    //#region Value Caption 1
    public set name(value: string) {
        if (value === this._name) return;
        this._name = value;
        this.e_name.textContent = this._name;
        if (Utils.isNullOrWhitespace(this._name)) {
            Core.removeClass(this.e_nameData, "Has-Value");
        } else {
            Core.addClass(this.e_nameData, "Has-Value");
        }
    }
    public get name(): string {
        return this._name;
    }

    public set nameCaption(value: string) {
        if (value === this._nameCaption) return;
        this._nameCaption = value;
        this.e_nameCaption.textContent = this._nameCaption;
        // if (Utils.isNullOrWhitespace(this._nameCaption)) {
        //     Core.removeClass(this.e_data1, "Has-Value");
        // } else {
        //     Core.addClass(this.e_data1, "Has-Value");
        // }
    }
    public get nameCaption(): string {
        return this._caption1;
    }
    //#endregion

    //#region Value Caption 1
    public set value1(value: string) {
        if (value === this._value1) return;
        this._value1 = value;
        this.e_value1.textContent = this._value1;
        if (Utils.isNullOrWhitespace(this._value1)) {
            Core.removeClass(this.e_data1, "Has-Value");
        } else {
            Core.addClass(this.e_data1, "Has-Value");
        }
    }
    public get value1(): string {
        return this._value1;
    }

    public set caption1(value: string) {
        if (value === this._caption1) return;
        this._caption1 = value;
        this.e_value1Caption.textContent = this._caption1;
        // if (Utils.isNullOrWhitespace(this._value1)) {
        //     Core.removeClass(this.e_data1, "Has-Value");
        // } else {
        //     Core.addClass(this.e_data1, "Has-Value");
        // }
    }
    public get caption1(): string {
        return this._caption1;
    }
    //#endregion

    //#region Value Caption 2
    public set value2(value: string) {
        if (value === this._value2) return;
        this._value2 = value;
        this.e_value2.textContent = this._value2;
        if (Utils.isNullOrWhitespace(this._value2)) {
            Core.removeClass(this.e_data2, "Has-Value");
        } else {
            Core.addClass(this.e_data2, "Has-Value");
        }
    }
    public get value2(): string {
        return this._value2;
    }

    public set caption2(value: string) {
        if (value === this._caption2) return;
        this._caption2 = value;
        this.e_value2Caption.textContent = this._caption2;
        // if (Utils.isNullOrWhitespace(this._value1)) {
        //     Core.removeClass(this.e_data2, "Has-Value");
        // } else {
        //     Core.addClass(this.e_data2, "Has-Value");
        // }
    }
    public get caption2(): string {
        return this._caption2;
    }
    //#endregion

    //#region Value Caption 3
    public set value3(value: string) {
        if (value === this._caption3) return;
        this._value3 = value;
        this.e_value3.textContent = this._value3;
        if (Utils.isNullOrWhitespace(this._value3)) {
            Core.removeClass(this.e_data3, "Has-Value");
        } else {
            Core.addClass(this.e_data3, "Has-Value");
        }
    }
    public get value3(): string {
        return this._value3;
    }

    public set caption3(value: string) {
        if (value === this._caption3) return;
        this._caption3 = value;
        this.e_value3Caption.textContent = this._caption3;
        // if (Utils.isNullOrWhitespace(this._value1)) {
        //     Core.removeClass(this.e_data3, "Has-Value");
        // } else {
        //     Core.addClass(this.e_data3, "Has-Value");
        // }
    }
    public get caption3(): string {
        return this._caption3;
    }
    //#endregion

    public set icon(value: string) {
        this.e_icon.style.display = (value === undefined ? 'none' : 'block');

        if (value !== this._icon) {
            Core.replaceClass(this.e_icon, this._icon, value);
            this._icon = value;
        }
    }
    public get icon(): string {
        return this._icon;
    }

    public set onAction(value: OnClickCallback) {
        this._onActionCallback = value;
    }
    public get onAction(): OnClickCallback {
        return this._onActionCallback;
    }

    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    public canSelect(): boolean {
        return false;
    }

    private _onActionClickHandler() {
        if (this._onActionCallback !== undefined) {
            this._onActionCallback();
        }
    }
}