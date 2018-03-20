using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Velocity
{
    /// <summary>
    /// Analysis Pipe that produces the angle at vertex b of the triangle abc specified by the user.
    /// </summary>
    public class LAP_Velocity : BaseLiftAnalysisPipe<AR_Velocity>
    {
        private Lift _lift;
        private List<JointTimeSeries> _jtsList;

        public LAP_Velocity(AR_Velocity request, Lift lift) : base(request)
        {
            _lift = lift;
            _jtsList = Utils.ConvertBodyDataToTimeSeriesSet(_lift.BodyData);
        }

        public override ResultEntity Execute()
        {
            //This is a little ugly, might clean it up later. We should discuss the default behaviour, currently returns Z.
            List<float> position;
            if (Request.Dimension == EDimension.X)
                position = new List<float>(_jtsList[(int)Request.Joint].X);
            else if (Request.Dimension == EDimension.Y)
                position = new List<float>(_jtsList[(int)Request.Joint].Y);
            else
                position = new List<float>(_jtsList[(int)Request.Joint].Z);

            double[] x = _jtsList[(int)Request.Joint].t.Select((n) => (double)n).ToArray(); //array of times in seconds
            double[] y = position.Select((n) => (double)n).ToArray(); //position at joints at the times stored in x


            var interpolate = MathNet.Numerics.Interpolation.CubicSpline.InterpolateNaturalSorted(x, y); //perform cubic spline interpolation
            var velocity = x.Select((t) => interpolate.Differentiate(t)).ToArray(); //differentiate cubic splines at all x values
            var vma = new MathNet.Filtering.Median.OnlineMedianFilter(10).ProcessSamples(velocity); //apply median filter to calculated velocities
            //foreach (var item in velocity)
            //    Console.Write("{0}", item);

            var time = new List<float>(_jtsList[(int)Request.Joint].t);
            var vmaList = y.Select((n) => (float)n).ToList();



            return new ResultEntity
            {
                Type = ELiftAnalysisType.Velocity,
                Value = new Dictionary<string, List<float>>()
                {
                    ["time"] = time,
                    ["data"] = vmaList
                }
            };

           
        }

        public override bool Validate()
        {
            if (Request.Type == ELiftAnalysisType.Velocity) return false;
            return true;
        }

    }

}