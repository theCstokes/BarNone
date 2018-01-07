import { IControlType } from "Vee/IControlType";

export default class ComponentConfig {
	[key:string]: any;
	public instance: IControlType<any>;
}