export class HelpElement {
	public name: string;
	public content: string;
	public image?: string;
}

export interface IHelp {
	content: HelpElement[];
}
