import BodyData from "App/Data/Models/BodyData/BodyData";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import LiftType from "App/Data/Models/Lift/LiftType";
import Permission from "App/Data/Models/Lift/Permission";
import { BaseDataModel } from "App/Data/Models/Core/BaseDataModel";

export default class Lift extends BaseDataModel {
    public id: number;

    public parentID: number;

    public name: string;

    public liftTypeID: number;

    public bodyDataID: number;

    public liftType: LiftType;

    public details: Partial<LiftDetails>;
}

class LiftDetails {
    public bodyData: BodyData;

    public parent: LiftFolder;

    public liftType: LiftType;

    public permissions: Permission[];
}