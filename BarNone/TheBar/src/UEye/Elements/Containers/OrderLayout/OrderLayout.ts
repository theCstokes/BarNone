import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represents UI container that organises children HTML elements in a particular order (right to left and left to right).
 */
export default class OrderLayout extends BaseContainer {
     /**  Represents HTML content of the OrderLayout container */
    private _content: HTMLElement;
    /**  Boolean represents the order the stacked contents are viewed. */
    private _rightToLeft: boolean;
    /** Constructor intializes and defines OrderLayout as an HTMLElement tag named UEye-Order-Layout (using Core.addClass). 
	 * * @returns Returns OrderLayout
     * */
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Order-Layout");
        // this._content = UEyeCore.create("div", this.element);
        this.linkComponentContainer("content", this.element);
    }
    /** Method sets the content of the OrderLayout container, calling method inheirited by BaseContainer. 
	 * * @param value Array of contents of type any to be arranged as columns.   
     * */
    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
    /** 
      * Accesor gets the value of the contents of the OrderLayout container.
	 * * @returns An array of elements.
     * */
    public get content(): any[] {
        return this.getComponentContainer("content");
    }
    /** Method sets the boolean value of property _rightToLeft and adds HTML container of children elements in the specified order. 
	 * * @param value Boolean value that represents the order. If true, viewed right to left else left to right.
     * */
    public set rightToLeft(value: boolean) {
        this._rightToLeft = value;

        if(this._rightToLeft) {
            Core.addClass(this.element, "Right-To-Left");
        } else {
            Core.removeClass(this.element, "Right-To-Left");
        }
    }
    /** 
      * Accesor gets the boolean value of the _rightToLeft property.
	 * * @returns Boolean value that indicates the current order with which content elements can be viewed.
     * */
    public get rightToLeft(): boolean {
        return this._rightToLeft;
    }
    
  
}