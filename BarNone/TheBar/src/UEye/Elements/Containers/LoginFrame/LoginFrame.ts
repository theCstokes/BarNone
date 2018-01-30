import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

/**
 *  Represents container that provides LoginFrame for Login elements and functionality. 
 */
export default class LoginFrame extends BaseContainer {
    /**  Represents background image seen on login interface */
    private _backgroundImage: HTMLImageElement;
     /**  Represents HTML content of the frame container */
    private _content: HTMLElement;
    private _onDo: OnClickCallback;
    private _backgroundImageSource: string;
    
    private _onDoC: number;
    
    /**  Represents the string image source of _backgroundImage element */
    // private _backgroundImageSource: string;

     /** Constructor intializes and defines the LoginFrame as an encompassing HTMLElement tag named UEye-Login-Frame (using Core.addClass). 
	 * * @returns Returns Frame with an image and content children.   
     * */
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Login-Frame");

        this._backgroundImage = Core.create("img", this.element, "Background") as HTMLImageElement;

        this._content = Core.create("div", this.element, "Content");
        this.linkComponentContainer("content", this._content);
        
        this._onDoC = 0;
        this._backgroundImage.onclick = () => {
            if (this._onDoC >= 2) {
                this._onDoC = 0;
                this._onDo();
            }
            this._onDoC++;
        };
    }

    public set onDo(value: OnClickCallback) {
        this._onDo = value;
    }
       /** Method sets the content of the LoginFrame.
	 * * @param value Array of contents of type any to be viewed as screen. 
     * */
    public set content(value: any[]) {
        this.setScreenContainer("content", value);
    }
    /** Accersor gets the content of Frame.
	 * * @returns  Array of contents viewed as ScreenContainer.
     * */
    public get content(): any[] {
        return this.getScreenContainer("content");
    }
     /** Accersor gets the background image of the Login Frame. 
	 * * @returns  String text that of the image source of the current background image.   
     * */
    public get background(): string {
        return this._backgroundImageSource;
    }
     /** Method sets the background image of the Login Frame. 
	 * * @param value String text that represents the image source of the background image.   
     * */
    public set background(value: string) {
        if (this._backgroundImageSource !== value) {
            this._backgroundImageSource = value;
            this._backgroundImage.src = this._backgroundImageSource;
        }
    }
}