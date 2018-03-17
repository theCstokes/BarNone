import BodyData from "App/Data/Models/BodyData/BodyData";
import { JointTypeEnum } from "App/Screens/Lifts/LiftEdit/JointTypeEnum";

export class SkeletonLine {
	public x1: number;

	public x2: number;

	public y1: number;

	public y2: number;

	public constructor(init?: Partial<SkeletonLine>) {
		Object.assign(this, init);
	}
}

export class SkeletonBuilder {

	private static _jointMap: { start: JointTypeEnum, end: JointTypeEnum }[];

	private static _init() {
		SkeletonBuilder._jointMap = [
			// Spine
			{ start: JointTypeEnum.Head, end: JointTypeEnum.Neck },
			{ start: JointTypeEnum.Neck, end: JointTypeEnum.SpineShoulder },
			{ start: JointTypeEnum.SpineShoulder, end: JointTypeEnum.SpineMid },
			{ start: JointTypeEnum.SpineMid, end: JointTypeEnum.SpineBase },



			// { start: JointTypeEnum.SpineBase, end: JointTypeEnum.SpineMid },
			// { start: JointTypeEnum.SpineMid, end: JointTypeEnum.SpineShoulder },
			// { start: JointTypeEnum.SpineShoulder, end: JointTypeEnum.Neck },
			// { start: JointTypeEnum.Neck, end: JointTypeEnum.SpineShoulder },

			// Right Top
			{ start: JointTypeEnum.SpineShoulder, end: JointTypeEnum.ShoulderRight },
			{ start: JointTypeEnum.ElbowRight, end: JointTypeEnum.ShoulderRight },
			{ start: JointTypeEnum.WristRight, end: JointTypeEnum.ElbowRight },
			{ start: JointTypeEnum.HandRight, end: JointTypeEnum.WristRight },
			{ start: JointTypeEnum.HandTipRight, end: JointTypeEnum.HandRight },
			{ start: JointTypeEnum.ThumbRight, end: JointTypeEnum.WristRight },

			// Right Bottom
			{ start: JointTypeEnum.SpineBase, end: JointTypeEnum.HipRight },
			{ start: JointTypeEnum.KneeRight, end: JointTypeEnum.HipRight },
			{ start: JointTypeEnum.AnkleRight, end: JointTypeEnum.KneeRight },
			{ start: JointTypeEnum.FootRight, end: JointTypeEnum.AnkleRight },

			// Left Top
			{ start: JointTypeEnum.SpineShoulder, end: JointTypeEnum.ShoulderLeft },
			{ start: JointTypeEnum.ElbowLeft, end: JointTypeEnum.ShoulderLeft },
			{ start: JointTypeEnum.WristLeft, end: JointTypeEnum.ElbowLeft },
			{ start: JointTypeEnum.HandLeft, end: JointTypeEnum.WristLeft },
			{ start: JointTypeEnum.HandTipLeft, end: JointTypeEnum.HandLeft },
			{ start: JointTypeEnum.ThumbLeft, end: JointTypeEnum.WristLeft },

			// Left Bottom
			{ start: JointTypeEnum.SpineBase, end: JointTypeEnum.HipLeft },
			{ start: JointTypeEnum.KneeLeft, end: JointTypeEnum.HipLeft },
			{ start: JointTypeEnum.AnkleLeft, end: JointTypeEnum.KneeLeft },
			{ start: JointTypeEnum.FootLeft, end: JointTypeEnum.AnkleLeft },
		];
	}

	public static build(bodyData: BodyData): SkeletonLine[][] {
		let i=0;
		SkeletonBuilder._init();
		return bodyData.details.orderedFrames.map(f => {
			var spineBase = f.details.joints.find(j => j.jointTypeID === (JointTypeEnum.SpineBase));
			
			if (spineBase === undefined) return [];

			return SkeletonBuilder._jointMap.reduce((result, m) => {
				// Note: the jointTypeIds from the api are currently sifted up by 1.
				var startJoint = f.details.joints.find(j => j.jointTypeID === (m.start));
				var endJoint = f.details.joints.find(j => j.jointTypeID === (m.end));

				if (startJoint === undefined) return result;
				if (endJoint === undefined) return result;
				if (spineBase === undefined) return result;

				result.push(new SkeletonLine({
					// x1: (startJoint.x /*- spineBase.x*/) * -103.34 + 206,
					// y1: startJoint.y * -103.34 + 52,
					// x2: (endJoint.x /*- spineBase.x*/) * -103.34 + 206,
					// y2: endJoint.y * -103.34 + 52

					// x1: (startJoint.x /*- spineBase.x*/) * -75.34 + 185,
					// y1: startJoint.y * -75.34 + 55,
					// x2: (endJoint.x /*- spineBase.x*/) * -75.34 + 185,
					// y2: endJoint.y * -75.34 + 55
			

				// x1: (startJoint.x - spineBase.x) * -153.34 + 256,
				// y1: startJoint.y * -153.34 + 212,
				// x2: (endJoint.x - spineBase.x) * -153.34 + 256,
				// y2: endJoint.y * -153.34 + 212

					x1: startJoint.x*-65+250,
					y1: startJoint.y*-65+100,
					x2: endJoint.x*-65+250,
					y2: endJoint.y*-65+100,
				}));
				// console.log("Start JointType:"+m.start, "End JointType:"+m.end, i++);
				return result;
			}, new Array<SkeletonLine>());
		});
	}

}