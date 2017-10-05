export abstract class View {
	[name: string]: any;
	
	abstract get content(): any[];
}