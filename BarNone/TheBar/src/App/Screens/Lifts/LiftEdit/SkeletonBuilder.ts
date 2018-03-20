import BodyData from "App/Data/Models/BodyData/BodyData";
import { EJointType } from "App/Data/Models/Joint/EJointType";

export class SkeletonLine {
	public x1: number;

	public x2: number;

	public y1: number;

	public y2: number;

	public z1: number;

	public z2: number;

	public timeStamp: string;

	public constructor(height: number, width: number, init?: Partial<SkeletonLine>) {
		Object.assign(this, init);
	}
}

export class SkeletonBuilder {

	private static _jointMap: { start: EJointType, end: EJointType }[];

	private static _init() {
		SkeletonBuilder._jointMap = [
			// Spine
			{ start: EJointType.Head, end: EJointType.Neck },
			{ start: EJointType.Neck, end: EJointType.SpineShoulder },
			{ start: EJointType.SpineShoulder, end: EJointType.SpineMid },
			{ start: EJointType.SpineMid, end: EJointType.SpineBase },



			// { start: JointTypeEnum.SpineBase, end: JointTypeEnum.SpineMid },
			// { start: JointTypeEnum.SpineMid, end: JointTypeEnum.SpineShoulder },
			// { start: JointTypeEnum.SpineShoulder, end: JointTypeEnum.Neck },
			// { start: JointTypeEnum.Neck, end: JointTypeEnum.SpineShoulder },

			// Right Top
			{ start: EJointType.SpineShoulder, end: EJointType.ShoulderRight },
			{ start: EJointType.ElbowRight, end: EJointType.ShoulderRight },
			{ start: EJointType.WristRight, end: EJointType.ElbowRight },
			{ start: EJointType.HandRight, end: EJointType.WristRight },
			{ start: EJointType.HandTipRight, end: EJointType.HandRight },
			{ start: EJointType.ThumbRight, end: EJointType.WristRight },

			// Right Bottom
			{ start: EJointType.SpineBase, end: EJointType.HipRight },
			{ start: EJointType.KneeRight, end: EJointType.HipRight },
			{ start: EJointType.AnkleRight, end: EJointType.KneeRight },
			{ start: EJointType.FootRight, end: EJointType.AnkleRight },

			// Left Top
			{ start: EJointType.SpineShoulder, end: EJointType.ShoulderLeft },
			{ start: EJointType.ElbowLeft, end: EJointType.ShoulderLeft },
			{ start: EJointType.WristLeft, end: EJointType.ElbowLeft },
			{ start: EJointType.HandLeft, end: EJointType.WristLeft },
			{ start: EJointType.HandTipLeft, end: EJointType.HandLeft },
			{ start: EJointType.ThumbLeft, end: EJointType.WristLeft },

			// Left Bottom
			{ start: EJointType.SpineBase, end: EJointType.HipLeft },
			{ start: EJointType.KneeLeft, end: EJointType.HipLeft },
			{ start: EJointType.AnkleLeft, end: EJointType.KneeLeft },
			{ start: EJointType.FootLeft, end: EJointType.AnkleLeft },
		];
	}

	public static build(bodyData: BodyData, canvasHeight: number, canvasWidth: number): SkeletonLine[][] {
		let i=0;
		SkeletonBuilder._init();
		return bodyData.details.orderedFrames.map(f => {
			var spineBase = f.details.joints.find(j => j.jointTypeID === (EJointType.SpineBase));
		
			if (spineBase === undefined) return [];
			return SkeletonBuilder._jointMap.reduce((result, m) => {
				// Note: the jointTypeIds from the api are currently sifted up by 1.
				var startJoint = f.details.joints.find(j => j.jointTypeID === (m.start));
				var endJoint = f.details.joints.find(j => j.jointTypeID === (m.end));

				if (startJoint === undefined) return result;
				if (endJoint === undefined) return result;
				if (spineBase === undefined) return result;

				result.push(new SkeletonLine(canvasHeight, canvasWidth,{
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

						x1: startJoint.x*-60+(canvasWidth/3),
						y1: startJoint.y*-60+(canvasHeight/3),
						z1: startJoint.z*-60+(canvasWidth*3/5),
						x2: endJoint.x*-60+(canvasWidth/3),
						y2: endJoint.y*-60+(canvasHeight/3),
						z2: endJoint.z*-60+(canvasWidth*3/5),
						 timeStamp: f.timeOfFrame
				}));
				// console.log("Start JointType:"+m.start, "End JointType:"+m.end, i++);

				return result;
			}, new Array<SkeletonLine>());
		});
	}

}