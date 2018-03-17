import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import { OnClickCallback, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

export default class DropDownListItem extends BaseListItem {

    // Private property field(s).
    private _name: string;
    private _onOpenActionCallback: OnClickCallback;
    private _icon: string;
    private _isCurrentUser: boolean;

    // Private dom element(s).
    private _nameElement: HTMLElement;
    private _openActionElement: HTMLElement;
    private _typeIcon: HTMLElement;

    public constructor(parent: HTMLElement) {
        super(parent, "Drop-Down-List-Item");

        var content = Core.create("div", this.element, "Content");
        this._nameElement = Core.create("div", content, "Name");
    }

    /** Set the name string. */
    public set name(value: string) {
        this._name = value;
        this._nameElement.textContent = this._name;
    }
    /** Get the name string. */
    public get name(): string {
        return this._name;
    }

    /** Set the callback for when the open button is clicked. */
    public set onOpen(value: OnClickCallback) {
        this._onOpenActionCallback = value;
    }
    /** Get the callback for when the open button is clicked. */
    public get onOpen(): OnClickCallback {
        return this._onOpenActionCallback;
    }

    public set isCurrentUser(value: boolean) {
        if (this._isCurrentUser !== value) {
            this._isCurrentUser = value;
            if (this._isCurrentUser) {
                Core.replaceClass(this.element, "Other-User", "Current-User");
            } else {
                Core.replaceClass(this.element, "Current-User", "Other-User");
            }
        }
    }
    public get isCurrentUser(): boolean {
        return this.isCurrentUser;
    }
   
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    public canSelect(): boolean {
        return true;
    }

    private _onOpenActionClickHandler() {
        if (this._onOpenActionCallback !== undefined) {
            this._onOpenActionCallback();
        }
    }
}