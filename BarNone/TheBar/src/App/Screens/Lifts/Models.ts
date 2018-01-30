export enum LiftListType { Lift, Folder }

export class LiftListItem {
	public id: number;
    public name: string;
    public type: LiftListType;
    public parentID: number;
}