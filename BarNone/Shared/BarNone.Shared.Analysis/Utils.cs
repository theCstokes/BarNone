using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis
{
    public struct JointTimeSeries
    {
        public float[] X;
        public float[] Y;
        public float[] Z;
    }

    public static class Utils
    {
        public static List<JointTimeSeries> ConvertBodyDataToTimeSeriesSet(BodyData bodyData)
        {
            List<JointTimeSeries> joints = new List<JointTimeSeries>();
            for (int i = 0; i < 25; i++)
            {
                JointTimeSeries jts;
                jts.X = (from frame in bodyData.BodyDataFrames select frame.Joints[i].X).ToArray();
                jts.Y = (from frame in bodyData.BodyDataFrames select frame.Joints[i].Y).ToArray();
                jts.Z = (from frame in bodyData.BodyDataFrames select frame.Joints[i].Z).ToArray();
                joints.Add(jts);
            }
            return joints;
        }


    }
}
