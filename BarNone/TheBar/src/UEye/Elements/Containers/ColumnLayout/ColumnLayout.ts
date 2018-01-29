import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represents Layout Container with multi-column content.
 */
export default class ColumnLayout extends BaseContainer {
      /** Constructor intializes and defines the ColumLayout n Container as an HTMLElement tag named UEye-Column-Layout (using Core.addClass). 
	 * * @returns Returns Column-Layout.   
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Column-Layout");
              //Links content to parent HTMLElement
        this.linkComponentContainer("columns", this.element);
    }
    /** Method sets the content of the ColumnLayout container, calling method inheirited by BaseContainer. 
	 * * @param value Array of contents of type any to be arranged as columns.   
     * */
    public set columns(value: any[]) {
        this.setComponentContainer("columns", value);
    }
     /** 
      * Accesor gets the value of the contents of the Column Layoutcontainer.
	 * * @returns An array of elements.
     * */
    public get columns(): any[] {
        return this.getComponentContainer("columns");
    }
    
   
}