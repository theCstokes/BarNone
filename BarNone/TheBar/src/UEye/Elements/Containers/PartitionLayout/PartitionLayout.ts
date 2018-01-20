import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represents partition of Single page layout into muliple containers, namely left side, right side and center content.
 */
export default class PartitionLayout extends BaseContainer {
     /**  Represents HTML element for the container occupying the left side of the SPA (Single Page Application) */
    private _leftSide: HTMLElement;
      /**  Represents HTML element for the container occupying the center holder of the SPA (Single Page Application) */
    private _holder: HTMLElement;
      /**  Represents HTML element for the container occupting the right side of the SPA (Single Page Application) */
    private _rightSide: HTMLElement;    
    /** Constructor intializes and defines PartitionLayout as an HTMLElement tag named UEye-Partition-Layout (using Core.addClass). 
	 * * @returns Returns OrderLayout with three container HTML children 
     * */    
    public constructor(parent: HTMLElement) {
        super (parent);
        Core.addClass(this.element, "UEye-Partition-Layout");
        
        this._leftSide = Core.create("div", this.element, "Left-Side");
        this.linkComponentContainer("leftSide", this._leftSide);

        this._holder = Core.create("div", this.element, "Holder");

        this._rightSide = Core.create("div", this._holder, "Right-Side");
        this.linkScreenContainer("rightSide", this._rightSide);
    }
     /** Method sets the contents of leftSide container, calling method inheirited by BaseContainer. 
	 * * @param value Array of contents of type any to be viewed in leftSide.   
     * */
    public set leftSide(value: any[]) {
        this.setComponentContainer("leftSide", value);
    }
    /** Accessor gets the contents of leftSide container.
	 * * @returns Array of contents of type any to be viewed in leftSide.   
     * */
    public get leftSide(): any[] {
        return this.getComponentContainer("leftSide");
    }
     /** Method sets the contents of rightSide container, calling method inheirited by BaseContainer. 
	 * * @param value Array of contents of type any to be viewed in rightSide.   
     * */
    public set rightSide(value: any[]) {
        this.setScreenContainer("rightSide", value);
    }
     /** Accessor gets the contents of rightSide container.
	 * * @returns Array of contents of type any to be viewed in rightSide.   
     * */
    public get rightSide(): any[] {
        return this.getScreenContainer("rightSide");
    }

   
}