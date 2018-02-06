import Lift from "App/Data/Models/Lift/Lift";

export default class Comment {
	public id: number;

	public userID: number;

	public liftID: number;

	public timeSent: string;

	public text: string;

	public details: CommentDetails;
}

class CommentDetails {
	public lift: Lift;
}