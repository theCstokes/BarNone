import Button from "UEye/Elements/Components/Button/Button";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represent interactive element Input. This component is editable andtakes text (strings) input. It extends Input component, all the while adding a layer of protection to honour user's privacy.
 */
export default class IconButton extends Button{
	
	public constructor(parent: HTMLElement) {
		super(parent);
       this.element.removeChild(this._textElement);
        this.element.setAttribute("type","icon");
	}
}