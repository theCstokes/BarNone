import BodyData from "App/Data/Models/BodyData/BodyData";

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

enum JointTypeEnum {
	SpineBase = 0,
	//
	// Summary:
	//     Middle of the spine.
	SpineMid = 1,
	//
	// Summary:
	//     Neck.
	Neck = 2,
	//
	// Summary:
	//     Head.
	Head = 3,
	//
	// Summary:
	//     Left shoulder.
	ShoulderLeft = 4,
	//
	// Summary:
	//     Left elbow.
	ElbowLeft = 5,
	//
	// Summary:
	//     Left wrist.
	WristLeft = 6,
	//
	// Summary:
	//     Left hand.
	HandLeft = 7,
	//
	// Summary:
	//     Right shoulder.
	ShoulderRight = 8,
	//
	// Summary:
	//     Right elbow.
	ElbowRight = 9,
	//
	// Summary:
	//     Right wrist.
	WristRight = 10,
	//
	// Summary:
	//     Right hand.
	HandRight = 11,
	//
	// Summary:
	//     Left hip.
	HipLeft = 12,
	//
	// Summary:
	//     Left knee.
	KneeLeft = 13,
	//
	// Summary:
	//     Left ankle.
	AnkleLeft = 14,
	//
	// Summary:
	//     Left foot.
	FootLeft = 15,
	//
	// Summary:
	//     Right hip.
	HipRight = 16,
	//
	// Summary:
	//     Right knee.
	KneeRight = 17,
	//
	// Summary:
	//     Right ankle.
	AnkleRight = 18,
	//
	// Summary:
	//     Right foot.
	FootRight = 19,
	//
	// Summary:
	//     Between the shoulders on the spine.
	SpineShoulder = 20,
	//
	// Summary:
	//     Tip of the left hand.
	HandTipLeft = 21,
	//
	// Summary:
	//     Left thumb.
	ThumbLeft = 22,
	//
	// Summary:
	//     Tip of the right hand.
	HandTipRight = 23,
	//
	// Summary:
	//     Right thumb.
	ThumbRight = 24
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

	public static build(bodyData: BodyData, canvasHeight: number, canvasWidth: number): SkeletonLine[][] {
		let i=0;
		SkeletonBuilder._init();
		return bodyData.details.orderedFrames.map(f => {
// <<<<<<< HEAD
// 			var spineBase = f.details.joints.find(j => j.jointTypeID === (EJointType.SpineBase));
		
// =======
			var spineBase = f.details.joints.find(j => j.jointTypeID === (JointTypeEnum.SpineBase + 1));
// >>>>>>> develop
			if (spineBase === undefined) return [];
			return SkeletonBuilder._jointMap.reduce((result, m) => {
				// Note: the jointTypeIds from the api are currently sifted up by 1.
				var startJoint = f.details.joints.find(j => j.jointTypeID === (m.start + 1));
				var endJoint = f.details.joints.find(j => j.jointTypeID === (m.end + 1));

				if (startJoint === undefined) return result;
				if (endJoint === undefined) return result;
				if (spineBase === undefined) return result;
// <<<<<<< HEAD

// 				result.push(new SkeletonLine(canvasHeight, canvasWidth,{
// 					// x1: (startJoint.x /*- spineBase.x*/) * -103.34 + 206,
// 					// y1: startJoint.y * -103.34 + 52,
// 					// x2: (endJoint.x /*- spineBase.x*/) * -103.34 + 206,
// 					// y2: endJoint.y * -103.34 + 52

// 					// x1: (startJoint.x /*- spineBase.x*/) * -75.34 + 185,
// 					// y1: startJoint.y * -75.34 + 55,
// 					// x2: (endJoint.x /*- spineBase.x*/) * -75.34 + 185,
// 					// y2: endJoint.y * -75.34 + 55
			

// 				// x1: (startJoint.x - spineBase.x) * -153.34 + 256,
// 				// y1: startJoint.y * -153.34 + 212,
// 				// x2: (endJoint.x - spineBase.x) * -153.34 + 256,
// 				// y2: endJoint.y * -153.34 + 212

// 						x1: startJoint.x*-60+(canvasWidth/3),
// 						y1: startJoint.y*-60+(canvasHeight/3),
// 						z1: startJoint.z*-60+(canvasWidth*3/5),
// 						x2: endJoint.x*-60+(canvasWidth/3),
// 						y2: endJoint.y*-60+(canvasHeight/3),
// 						z2: endJoint.z*-60+(canvasWidth*3/5),
// 						 timeStamp: f.timeOfFrame
// 				}));
// 				// console.log("Start JointType:"+m.start, "End JointType:"+m.end, i++);

// =======
					result.push(new SkeletonLine(canvasHeight, canvasWidth,{
						x1: startJoint.x*-60+(canvasWidth/3),
						y1: startJoint.y*-60+(canvasHeight/2),
						z1: startJoint.z*-60+(canvasWidth*3/5),
						x2: endJoint.x*-60+(canvasWidth/3),
						y2: endJoint.y*-60+(canvasHeight/2),
						z2: endJoint.z*-60+(canvasWidth*3/5),
						timeStamp: f.timeOfFrame

					}));
// >>>>>>> develop
				return result;
			}, new Array<SkeletonLine>());
		});
	}

}