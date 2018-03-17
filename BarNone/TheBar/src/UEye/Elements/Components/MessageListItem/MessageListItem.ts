import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import { OnClickCallback, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

export default class MessageListItem extends BaseListItem {

    // Private property field(s).
    private _value: string;
    private _userName: string;
    private _date: string;
    // private _onOpenActionCallback: OnClickCallback;
    // private _icon: string;
    private _isCurrentUser: boolean;

    // Private dom element(s).
    private e_content: HTMLElement;
    private e_value: HTMLElement;
    private e_info: HTMLElement;
    private e_userName: HTMLElement;
    private e_time: HTMLElement;
    // private _nameElement: HTMLElement;
    // private _openActionElement: HTMLElement;
    // private _typeIcon: HTMLElement;

    public constructor(parent: HTMLElement) {
        super(parent, "Message-List-Item");

        this.e_content = Core.create("div", this.element, "Content");

        this.e_value = Core.create("div", this.e_content, "Value");

        this.e_info = Core.create("div", this.e_content, "Info");
        this.e_userName = Core.create("div", this.e_info, "User-Name");
        this.e_time = Core.create("div", this.e_info, "Time");

        // this.e_userName.textContent = "User"
        // this.e_time.textContent = "Time"
        
        // this._icon = "fa-folder";
        // this._typeIcon = Core.create("div", content, "Type-Icon fa", this._icon);

        
        // this._nameElement.onclick = this._onOpenActionClickHandler.bind(this);
        // this._openActionElement = Core.create("div", this.element, "Open-Action fa fa-arrow-circle-right");
        // this._openActionElement.onclick = this._onOpenActionClickHandler.bind(this);
    }

    public set value(value: string) {
        this._value = value;
        this.e_value.textContent = this._value;
    }
    public get value(): string {
        return this._value;
    }

    public set userName(value: string) {
        this._userName = value;
        this.e_userName.textContent = this._userName;
    }
    public get userName(): string {
        return this._userName;
    }

    public set date(value: string) {
        this._date = value;
        this.e_time.textContent = this._date;
    }
    public get date(): string {
        return this._date;
    }

    // /**
    //  * Set the callback for when the open button is clicked.
    //  */
    // public set onOpen(value: OnClickCallback) {
    //     this._onOpenActionCallback = value;
    // }
    // /**
    //  * Get the callback for when the open button is clicked.
    //  */
    // public get onOpen(): OnClickCallback {
    //     return this._onOpenActionCallback;
    // }

    // public set icon(value: string) {
    //     if (value !== this._icon) {
    //         Core.replaceClass(this._typeIcon, this._icon, value);
    //         this._icon = value;
    //     }
    // }
    // public get icon(): string {
    //     return this._icon;
    // }

    public set isCurrentUser(value: boolean) {
        if (this._isCurrentUser !== value) {
            this._isCurrentUser = value;
            if (this._isCurrentUser) {
                Core.replaceClass(this.element, "Other-User", "Current-User");
                Core.replaceClass(this.e_content, "Other-User", "Current-User");
                Core.replaceClass(this.e_value, "Other-User", "Current-User");
                Core.replaceClass(this.e_info, "Other-User", "Current-User");
            } else {
                Core.replaceClass(this.element, "Current-User", "Other-User");
                Core.replaceClass(this.e_content, "Current-User", "Other-User");
                Core.replaceClass(this.e_value, "Current-User", "Other-User");
                Core.replaceClass(this.e_info, "Current-User", "Other-User");
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
        return false;
    }

    // private _onOpenActionClickHandler() {
    //     if (this._onOpenActionCallback !== undefined) {
    //         this._onOpenActionCallback();
    //     }
    // }
}