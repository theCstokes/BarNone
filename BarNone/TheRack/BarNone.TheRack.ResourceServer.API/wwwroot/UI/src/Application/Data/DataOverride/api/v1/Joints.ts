import { BaseDataOverride } from "Vee/Data/BaseDataOverride";
import Joint from "Application/Data/Models/Body/Joint";

export default class JointsData extends BaseDataOverride<Joint> {
	public data: Joint[] = [
		{ x: 50, y: 50, z: 0 },
		{ x: 100, y: 50, z: 0 },
		{ x: 50, y: 100, z: 0 },
		{ x: 100, y: 100, z: 0 }
	];

}