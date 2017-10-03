import { AppScreen } from "Vee/Screen/AppScreen";
import ControlTypes from "Vee/ControlTypes";

export default class BasicScreen extends AppScreen {
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Screen,
				content: this.view.content
			}
		]
	}

}