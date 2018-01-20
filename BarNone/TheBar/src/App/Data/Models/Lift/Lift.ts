import BodyData from "App/Data/Models/BodyData/BodyData";

export default class Lift {
    public id: number;

    public parentID: number;

    public name: string;

    public details: LiftDetails;
}

class LiftDetails {
    public bodyData: BodyData;
}