import BodyData from "App/Data/Models/BodyData/BodyData";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";

export default class Lift {
    public id: number;

    public parentID: number;

    public name: string;

    public details: LiftDetails;
}

class LiftDetails {
    public bodyData: BodyData;

    public parent: LiftFolder;
}