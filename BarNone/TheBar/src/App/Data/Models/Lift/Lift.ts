import BodyData from "App/Data/Models/BodyData/BodyData";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import LiftType from "App/Data/Models/Lift/LiftType";
import Permission from "App/Data/Models/Lift/Permission";

export default class Lift {
    public id: number;

    public parentID: number;

    public name: string;

    public LiftTypeID: number;

    public liftType: LiftType;

    public details: Partial<LiftDetails>;

    public updateFilter: string[];
}

class LiftDetails {
    public bodyData: BodyData;

    public parent: LiftFolder;

    public liftType: LiftType;

    public permissions: Permission[];
}