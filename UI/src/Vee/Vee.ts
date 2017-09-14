import { IUtils } from 'Vee/Vee.config';

declare global {
	const Utils: IUtils
}

export default class Vee {
	public static start(): void {
		console.log("Test");

		var p = "Chris";
		var p1 = Utils.clone(p);

		console.log(p);
		console.log(p1);
	}
}