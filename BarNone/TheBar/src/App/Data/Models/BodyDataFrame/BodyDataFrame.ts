import Joint from "App/Data/Models/Joint/Joint";

export default class BodyDataFrame {
    public id: number;

    public bodyDataID: number;

    public timeOfFrame: string;

    public timeUntilNextFrame: string;

    public details: BodyDataFrameDetail;
}

class BodyDataFrameDetail {
    public joints: Joint[];
}