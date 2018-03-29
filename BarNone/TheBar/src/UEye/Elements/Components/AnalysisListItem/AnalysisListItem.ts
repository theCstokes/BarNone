import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import { OnClickCallback, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
import { Translate } from "UEye/Translate";

export default class AnalysisListItem extends BaseListItem {

    // Private property field(s).
    private _name: string;

    private _value1: string;
    private _caption1: string;

    private _value2: string;
    private _caption2: string;

    private _value3: string;
    private _caption3: string;

    private _value: string;
    private _onActionCallback: OnClickCallback;
    private _icon: string;

    // Private dom element(s).
    private e_name: HTMLElement;
    private e_nameCaption: HTMLElement;
    private e_value: HTMLElement;
    private e_valueCaption: HTMLElement;
    private e_action: HTMLElement;
    // private _openActionElement: HTMLElement;
    // private _typeIcon: HTMLElement;

    public constructor(parent: HTMLElement) {
        super(parent, "Analysis-List-Item");

        var content = Core.create("div", this.element, "Content");

        var nameHolder = Core.create("div", content, "Name-Holder");
        var valueHolder = Core.create("div", content, "Value-Holder");

        this.e_name = Core.create("div", nameHolder, "Name");
        this.e_nameCaption = Core.create("div", nameHolder, "Name-Caption");
        this.e_nameCaption.textContent = Translate.AnalysisListItem.Name;

        this.e_value = Core.create("div", valueHolder, "Value");
        this.e_valueCaption = Core.create("div", valueHolder, "Value-Caption");
        this.e_valueCaption.textContent = Translate.AnalysisListItem.Value;

        this.e_action = Core.create("div", this.element, "Action fa");
        this.e_action.style.display = 'none';
        this.e_action.onclick = this._onActionClickHandler.bind(this);
    }

    public set name(value: string) {
        this._name = value;
        this.e_name.textContent = this._name;
    }
    public get name(): string {
        return this._name;
    }

    //#region Value Caption 1
    public set value1(value: string) {
        this._value1 = value;
        this.e_name.textContent = this._name;
    }
    public get value1(): string {
        return this._value1;
    }

    public set caption1(value: string) {
        this._caption1 = value;
        this.e_name.textContent = this._name;
    }
    public get caption1(): string {
        return this._caption1;
    }
    //#endregion

    //#region Value Caption 2
    public set value2(value: string) {
        this._value2 = value;
        this.e_name.textContent = this._name;
    }
    public get value2(): string {
        return this._value2;
    }

    public set caption2(value: string) {
        this._caption2 = value;
        this.e_name.textContent = this._name;
    }
    public get caption2(): string {
        return this._caption2;
    }
    //#endregion

    //#region Value Caption 3
    public set value3(value: string) {
        this._value3 = value;
        this.e_name.textContent = this._name;
    }
    public get value3(): string {
        return this._value3;
    }

    public set caption3(value: string) {
        this._caption3 = value;
        this.e_name.textContent = this._name;
    }
    public get caption3(): string {
        return this._caption3;
    }
    //#endregion

    public set icon(value: string) {
        this.e_action.style.display = (value === undefined ? 'none' : 'block');

        if (value !== this._icon) {
            Core.replaceClass(this.e_action, this._icon, value);
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