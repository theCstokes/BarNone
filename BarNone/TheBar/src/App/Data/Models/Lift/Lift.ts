import BodyData from "App/Data/Models/BodyData/BodyData";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import LiftType from "App/Data/Models/Lift/LiftType";

export default class Lift {
    public id: number;

    public parentID: number;

    public name: string;

    public LiftTypeID: number;

    public details: LiftDetails;
}

class LiftDetails {
    public bodyData: BodyData;

    public parent: LiftFolder;

    public liftType: LiftType;
}