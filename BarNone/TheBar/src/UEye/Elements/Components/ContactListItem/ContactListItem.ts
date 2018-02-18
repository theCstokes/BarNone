import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";

/**
 *  Represents the individual list items in the secondary sidebar used to minimally describe content of the screen.
 *  At any given time the sole selected list item represents the object corresponding to which data is viewed and edited on the sub screen.
 */
export default class ContactListItem extends BaseListItem {
     /**  Represents name content of the ListItem component as a HTMLElement. Used as an identifier. */
    private _nameElement: HTMLElement;
     /**  Represents name content of the ListItem  component as a string value. Used as an identifier. */
    private _name: string;
    /** Constructor intializes and defines the ContactListItem component as an HTMLElement tag named UEye-Contact-List-Item (using Core.addClass). Returns a ContactListITem type with a name child element.
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns ContactListItem.   
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Contact-List-Item");

        this._nameElement = Core.create("div", this.element, "Name");

    }
     /** Method for setting value of property _name and changing the text content of _nameElement to reflect the same.
     * @param value Method parameter represents string viewed as name of the ContactListITem component.
     * */
    public set name(value: any) {
        this._name = value;
        this._nameElement.textContent = this._name;
    }
    /** Accessor to get _name property.
     * @returns Returns the string value of name content of ContactListItem element.
     * */
    public get name(): any {
        return this._name;
    }

    /** Method not used in this element
     * */
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    public canSelect(): boolean {
        return false;
    }
}