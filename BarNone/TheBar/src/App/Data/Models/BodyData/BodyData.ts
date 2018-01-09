import BodyDataFrame from "App/Data/Models/BodyDataFrame/BodyDataFrame";

export default class BodyData {
    public id: number;

    public recordTimeStamp: string;

    public details: BodyDataDetails;
}

class BodyDataDetails {
    public orderedFrames: BodyDataFrame[];
}