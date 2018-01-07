import { IControlType } from "UEye/IControlType";

export default class ComponentConfig {
	[key:string]: any;
	public instance: IControlType<any>;
}