import Input from "Vee/Elements/Components/Input/Input";

export default class PasswordInput extends Input {
	public constructor(parent: HTMLElement) {
		super(parent);
		this._input.setAttribute("type", "password");
	}
}