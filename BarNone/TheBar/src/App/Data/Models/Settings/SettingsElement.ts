import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Screen from "UEye/Screen/Screen";

export default class SettingsElement implements IListItem {
	public id: number;
	public name: string;
	public icon: string;
	public screen: { new(): Screen<any> };
}