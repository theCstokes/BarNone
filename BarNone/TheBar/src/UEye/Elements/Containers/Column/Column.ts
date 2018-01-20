import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represents container that arranges contents as sucessive columns.
 */
export default class Column extends BaseContainer {
     /** Constructor intializes and defines the Column Container as an HTMLElement tag named UEye-Column (using Core.addClass). 
	 * * @returns Returns Column.   
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Column");

        // this._columnElements = UEyeCore.create("div", this.element, "Column-Elements");'
        //Links content to parent HTMLElement
        this.linkComponentContainer("content", this.element);
    }
    /** Method sets the content of the Column container, calling method inheirited by BaseContainer. 
	 * * @param value Array of contents of type any to be arranged as columns.   
     * */
    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
     /** 
      * Accesor gets the value of the contents of the Column container.
	 * * @returns An array of elements.
     * */
    public get content(): any[] {
        return this.getComponentContainer("content");
    }

   
}