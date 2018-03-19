type RenderPipeLineStage<TState> = (current: TState, original: TState) => void;

type ScreenPipeLineStage = (data?: any) => void;

export default class ScreenPipeLine<TState> {
	private renderStages: RenderPipeLineStage<TState>[];
	private showStages: ScreenPipeLineStage[];

	public constructor() {
		this.renderStages = [];
		this.showStages = [];
	}

	public static create<TState>(): ScreenPipeLine<TState> {
		return new ScreenPipeLine();
	}

	public onRender(callback: RenderPipeLineStage<TState>): ScreenPipeLine<TState> {
		this.renderStages.push(callback);
		return this;
	}

	public onShow(callback: ScreenPipeLineStage): ScreenPipeLine<TState> {
		this.showStages.push(callback);
		return this;
	}

	public get onRenderInvokable(): RenderPipeLineStage<TState> {
		return (current: TState, original: TState) => this.renderStages.forEach(s => s(current, original));
	}

	public get onShowInvokable(): ScreenPipeLineStage {
		return (data?: any) => this.showStages.forEach(s => s(data));
	}
}