import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represents Frame container that describes the base layout of the Single Page UI.
 */
export default class Frame extends BaseContainer {

    // private _globalDock: HTMLElement;

    /**  Represents the layout header dock as an HTMLElement */

    private _contextDock: HTMLElement;
    /**  Represents the dock layout for the navigation side bar HTMLElement  */
    private _navDock: HTMLElement;
    /**  Represents the dock layout for the help side bar HTMLElement  */
    private _helpDock: HTMLElement;
    /**  Represents parent layout that contains and views user information in the UI (Above nav bar). */
    private _statusArea: HTMLElement;
    /**  Represents image of used for user profile*/
    private _statusImageElement: HTMLImageElement;
    /**  Represents Button element of the _statusImageElement. */
    private _statusImageButtonElement: HTMLElement;
    /** Represents title of the _statusArea . */
    private _statusTitleElement: HTMLElement;
    /**  Represents content elements of the parent HTMLElement  */
    private _content: HTMLElement;

    private _statusImageSource: string;
    private _showHelp: boolean;
    private _statusTitle: string;
    /** 
     * Constructor intializes and defines the FrameContainer as an
     * encompassing HTMLElement tag named UEye-Frame (using Core.addClass). 
     * @returns Returns Frame with a variety of Docks (context, global, nav and statusArea) 
     * to describe the different sections of the Single Page Frame.
     */
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Frame");

        //  this._globalDock = Core.create("div", this.element, "Global-Dock");
        // this.linkComponentContainer("globalDock", this._globalDock);

        this._contextDock = Core.create("div", this.element, "Context-Dock");
        this.linkComponentContainer("contextDock", this._contextDock);

        this._statusArea = Core.create("div", this.element, "Status-Area");
        var _statusImageArea = Core.create("div", this._statusArea, "Status-Image-Area");
        this._statusImageElement = Core.create("img", _statusImageArea, "Status-Image") as HTMLImageElement;
        this._statusTitleElement = Core.create("div", this._statusArea, "Status-Title");

        var statusImageHoverElement = Core.create("div", this._statusArea, "Status-Image-Hover");
        this._statusImageButtonElement = Core.create("div", statusImageHoverElement, "Status-Image-Button");
        this._statusImageButtonElement.textContent = "Test";

        this.linkComponentContainer("addOns", this.element);

        this._navDock = Core.create("div", this.element, "Nav-Dock");
        this.linkComponentContainer("navDock", this._navDock);

        this._content = Core.create("div", this.element, "Content");
        this.linkScreenContainer("content", this._content);

        this._helpDock = Core.create("div", this.element, "Help-Dock");
        this.linkComponentContainer("helpDock", this._helpDock);
        // this._helpDock.style.visibility = "hidden";
    }


    // public set globalDock(value: any[]) {
    //     this.setComponentContainer("globalDock", value);
    // }
    // public get globalDock(): any[] {
    //     return this.getComponentContainer("globalDock");
    // }


    public set contextDock(value: any[]) {
        this.setComponentContainer("contextDock", value);
    }
    /** 
     * Accesor gets the content of contextDock container.
	 * @returns  Array of type any 
     */
    public get contextDock(): any[] {
        return this.getComponentContainer("contextDock");
    }
    /** 
     * Accesor gets the text from _statusTitle.
     * @returns  String text value
     */
    public get statusTitle(): string {
        return this._statusTitle;
    }
    /** 
     * Method sets the content of the _statusTile which represents the 
     * text component of _statusArea
     * @param value String value to be viewed as text in _statusTitle. 
     */
    public set statusTitle(value: string) {
        if (this._statusTitle !== value) {
            this._statusTitle = value;
            this._statusTitleElement.textContent = this._statusTitle;
        }
    }
    /** 
     * Accesor gets the img source from _statusImageSource.
     * @returns  String path of image source
     */
    public get statusImageSource(): string {
        return this._statusImageSource;
    }
    /** 
     * Method sets the image source of the _statuImageElement as a string path
     * @param value String value of the path of image source file
     */
    public set statusImageSource(value: string) {
        if (this._statusImageSource !== value) {
            this._statusImageSource = value;
            this._statusImageElement.src = this._statusImageSource;
        }
    }
    /** 
     * Method sets the content of the _navDock Layout
     * @param value Array of contents of type any to be viewed in navDock.
     */
    public set navDock(value: any[]) {
        this.setComponentContainer("navDock", value);
    }
    /** 
     * Accersor gets the array of contents of navDock Layout.
     * @returns  Array of contents of type any viewed in navDock.
     */
    public get navDock(): any[] {
        return this.getComponentContainer("navDock");
    }
    
    public set helpDock(value: any[]) {
        this.setComponentContainer("helpDock", value);
    }
    /** 
     * Accersor gets the array of contents of navDock Layout.
     * @returns  Array of contents of type any viewed in navDock.
     */
    public get helpDock(): any[] {
        return this.getComponentContainer("helpDock");
    }
    /**
     * Toggle help
     * @param value - flag 
     */
    public toggleHelpBar(value: boolean) {
        if (value) {
            Core.replaceClass(this._helpDock, "Hidden-Dock", "Visible");
            Core.replaceClass(this._content, "Hidden-Help-Dock", "Show-Help-Dock");
            Core.replaceClass(this._contextDock, "Hidden-Help-Dock", "Show-Help-Dock");
        } else {
            Core.replaceClass(this._helpDock, "Visible", "Hidden-Dock");
            Core.replaceClass(this._content, "Show-Help-Dock", "Hidden-Help-Dock");
            Core.replaceClass(this._contextDock, "Show-Help-Dock", "Hidden-Help-Dock");
            
        }
    }
    /** 
     * Method sets the content of the Frame.
     * @param value Array of contents of type any to be viewed as screen. 
     */
    public set content(value: any[]) {
        this.setScreenContainer("content", value);
    }
    /** 
     * Accersor gets the content of Frame.
	 * @returns  Array of contents viewed as ScreenContainer.
     */
    public get content(): any[] {
        return this.getScreenContainer("content");
    }

    /** 
     * Method sets the addOns of the Frame.
     * @param value Array of addOns of type any to be viewed as screen. 
     */
    public set addOns(value: any[]) {
        this.setScreenContainer("addOns", value);
    }
    /** 
     * Accersor gets the addOns of Frame.
	 * @returns  Array of addOns viewed as ScreenContainer.
     */
    public get addOns(): any[] {
        return this.getScreenContainer("addOns");
    }

}