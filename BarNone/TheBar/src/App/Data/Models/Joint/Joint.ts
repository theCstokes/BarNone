import JointTrackingStateType from "App/Data/Models/Joint/JointTrackingStateType";
import JointType from "App/Data/Models/Joint/JointType";

export default class Joint {
	public id: number;

	public bodyDataFrameID: number;

	public jointTrackingStateTypeID: number;

	public jointTypeID: number;

	public x: number;

	public y: number;

	public z: number;

	public details: JointDetails;
}

class JointDetails {
	public jointTrackingStateType: JointTrackingStateType;

	public jointType: JointType;
}