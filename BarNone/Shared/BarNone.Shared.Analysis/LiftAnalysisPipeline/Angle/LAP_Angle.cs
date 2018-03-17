using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Angle
{
    /// <summary>
    /// Analysis Pipe that produces the angle at vertex b of the triangle abc specified by the user.
    /// </summary>
    public class LAP_Angle : BaseLiftAnalysisPipe<AR_Angle>
    {
        private Lift _lift;
        private List<JointTimeSeries> _jtsList;

        public LAP_Angle(AR_Angle request, Lift lift) : base(request)
        {
            _lift = lift;
            _jtsList = Utils.ConvertBodyDataToTimeSeriesSet(_lift.BodyData);
        }

        public override ResultEntity Execute()
        {
            var thetas = buildResponseList();
            return new ResultEntity
            {
                Type = ELiftAnalysisType.Angle,
                Value = thetas
            };
        }


        public override bool Validate()
        {
            if (Request.Type == ELiftAnalysisType.Angle) return false;
            return true;
        }

        /// <summary>
        /// Builds a list of the joint angles at each respective frame.
        /// </summary>
        /// <returns></returns>
        private List<double> buildResponseList()
        {
            List<double> thetas = new List<double>();
            JointTimeSeries a = _jtsList[(int) Request.JointA];
            JointTimeSeries b = _jtsList[(int) Request.JointB];
            JointTimeSeries c = _jtsList[(int) Request.JointC];

            for (int i = 0; i < a.X.Length; i++)
            {
                double t = computeAngle(a: new[] { a.X[i], a.Y[i], a.Z[i] }, b: new[] { b.X[i], b.Y[i], b.Z[i] }, c: new[] { c.X[i], c.Y[i], c.Z[i] });
                thetas.Add(t);
            }
            return thetas;
        }


        /// <summary>
        /// Computes the angle b of triangle abc. See https://math.stackexchange.com/a/361419 for the math.
        /// </summary>
        /// <param name="a">Point A as an array [x,y,z]</param>
        /// <param name="b">Point B as an array [x,y,z]</param>
        /// <param name="c">Point C as an array [x,y,z]</param>
        /// <returns>theta_B</returns>
        private static float computeAngle(float[] a, float[] b, float[] c)
        {
            float[] ba_v = { a[0] - b[0], a[1] - b[1], a[2] - b[2] };
            float[] bc_v = { c[0] - b[0], c[1] - b[1], c[2] - b[2] };
            
            float ba = (float) Math.Sqrt(ba_v[0] * ba_v[0] + ba_v[1] * ba_v[1] + ba_v[2] * ba_v[2]);
            float bc = (float) Math.Sqrt(bc_v[0] * bc_v[0] + bc_v[1] * bc_v[1] + bc_v[2] * bc_v[2]);
            
            float ba_dot_bc = ba_v[0] * bc_v[0] + ba_v[1] * bc_v[1] + ba_v[2] * bc_v[2];
            
            float theta = (float) Math.Acos(ba_dot_bc / ba / bc);
            return theta;
        }


    }
}