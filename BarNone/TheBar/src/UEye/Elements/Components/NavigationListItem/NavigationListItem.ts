import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
/**
 * Represents the individual list items in the navigation sidebar used to minimally describe content of the screen.
 *  At any given time the sole selected list item represents the screen currently being viewed.
 */
export default class NavigationListItem extends BaseListItem {
    /**  Represents name content of the ListItem component as a HTMLElement. Used as an identifier. */
    private _nameElement: HTMLElement;
    /**  Represents icon content of the ListItem component as a HTMLElement. Used as an identifier. */
    private _iconElement: HTMLElement;  
    /**  Represents icon content of the ListItem component as a string value. Used as an identifier. */
    private _icon: string;
        /**  Represents name content of the ListItem  component as a string value. Used as an identifier. */
    private _name: string;
     /** Constructor intializes and defines the NavigationListItem component as an HTMLElement tag named UEye-Navigation-List-Item (using Core.create). Returns a NavigationListITem type with name and icon children elements.
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns NavigationListItem.   
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Navigation-List-Item");
        
		this._iconElement = Core.create('div', this.element, 'fa', 'Icon');
        this._nameElement = Core.create("div", this.element, "Name");

    }
    /** Method for setting value of property _name and changing the text content of _nameElement to reflect the same.
     * @param value Method parameter represents string viewed as name of the NavigationListITem component.
     * */
    public set name(value: any) {
        this._name = value;
        this._nameElement.textContent = this._name;
    }
    /** Method for setting value of property _icon and deleting the existing icon, creating a new icon HTMLElement and replacing the original.
     * @param value Method parameter represents string of _icon (requires the fa prefix to represent use of Font Awesome).
     * */
    public set icon(value: string) {
		if (this._icon !== value) {
           
			this._icon = value;	
			if (value !== undefined) {
				Core.addClass(this._iconElement, "Visible");
			}
			Core.addClass(this._iconElement, this._icon);
		}
    }
     /** Accessor to get _name property.
     * @returns Returns the string value of name content of NavigationListItem element.
     * */
    public get name(): any {
        return this._name;
    }
    /** Accessor to get _icon property.
     * @returns Returns the string value of icon content of Button element.
     * */
    public get icon(): string {
		return this._icon;
	}
    /** Method that toggles enable and disable state of a Button element.
     * @returns Nothing (return part of property definition).
     * */
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    public canSelect(): boolean {
        return true;
    }
}