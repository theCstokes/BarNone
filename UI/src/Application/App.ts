import Vee from "Vee/Vee";
import Core from "Vee/Elements/Core/Core";

export default class App {
	public static start(): void {
		Core.inflate(Vee.base, [
			{
				instance: Vee.Button,
				text: "Add"
			}
		]);
	}
}