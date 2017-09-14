import Vee from "Vee/Vee";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";

export default class App {
	public static start(): void {
		Core.inflate(Vee.base, [
			{
				instance: ControlTypes.Frame,
				statusTitle: "Christopher Stokes",
				statusImageSource: "res/jedi-symbol.jpg",
				globalDock: {
					instance: ControlTypes.OrderLayout,
					rightToLeft: true,
					content: [
						{
							instance: ControlTypes.Label,
							text: "Hello World"
						}
					]
				},
				contextDock: {
					instance: ControlTypes.ColumnLayout,
					columns: [
						{
							instance: ControlTypes.Column,
							content: [
								{
									instance: ControlTypes.Label,
									text: "User/Contacts"
								}
							]
						},
						{
							instance: ControlTypes.Column,
							content: [
								{
									instance: ControlTypes.Button,
									text: "Add Contact"
								}
							]
						},
						{
							instance: ControlTypes.Column,
							content: {
							}
						}
					]
				}
			}
		]);
	}
}