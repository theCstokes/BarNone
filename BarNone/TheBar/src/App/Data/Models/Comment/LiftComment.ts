import Lift from "App/Data/Models/Lift/Lift";

export default class LiftComment {
	public id: number;

	public sentUserID: number;

	public liftID: number;

	public timeSent: string;

	public text: string;

	public details?: LiftCommentDetails;
}

class LiftCommentDetails {
	public lift: Lift;
}