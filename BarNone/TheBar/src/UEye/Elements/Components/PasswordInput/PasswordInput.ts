import Input from "UEye/Elements/Components/Input/Input";
/**
 *  Represent interactive element Input. This component is editable andtakes text (strings) input. It extends Input component, all the while adding a layer of protection to honour user's privacy.
 */
export default class PasswordInput extends Input {
	
	public constructor(parent: HTMLElement) {
		super(parent);
		this._input.setAttribute("type", "password");
	}
}