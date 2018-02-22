import Lift from "App/Data/Models/Lift/Lift";

export default class LiftFolder {
	public id: number;

	public name: string;

	public parentID: number;

	public details: LiftFolderDetails;
}

class LiftFolderDetails {
	public lifts: Lift[];

	public subFolders: LiftFolder[];
}