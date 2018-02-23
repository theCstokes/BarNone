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

        /// <summary>
        /// Finds the local minima within the provided series.
        /// </summary>
        /// <param name="y">Target Series</param>
        /// <param name="sensitivity">Specifies how sensitive the algorithm should be to noise within y</param>
        /// <returns>The indexes of the local minima of the provided series.</returns>
        //public static List<int> FindLocalMinima(double[] y, double sensitivity = 0.1)
        //{
        //    if (y == null)
        //        throw new ArgumentNullException();

        //    List<Tuple<int, double>> localMinima = new List<Tuple<int, double>> { };
        //    Tuple<int, double> currentMinima = new Tuple<int, double>(0, y[0]);
        //    for (int i = 1; i < y.Length; i++)
        //    {
        //        if (y[i] < currentMinima.Item2 - sensitivity)
        //            currentMinima = new Tuple<int, double>(i, y[i]);
        //        else if (y[i] > currentMinima.Item2 + sensitivity)
        //            localMinima.
        //    }
        //}


        //public static List<int> FindLocalMinima(float[] y)
        //{
        //    double[] a = y.Select(x => (double)x).ToArray();
        //    return FindLocalMinima(a);
        //}

        /// <summary>
        /// Produces the discrete derivative of y calculated as y'(n) = (y(n)-y(n-1))/Ts
        /// </summary>
        /// <param name="y">The signal to be differentiated</param>
        /// <param name="samplingRate">1/Ts, the number of samples per second</param>
        /// <returns>The discrete derivative of the provided signal.</returns>
        public static double[] DiscreteDerivative(double[] y, float samplingRate)
        {
            List<double> d = new List<double>();
            for (int i = 1; i < y.Length; i++)
            {
                d.Add((y[i] - y[i - 1])*samplingRate);
            }

            return d.ToArray();
        }

        public static Dictionary<string, double> GetSquatAnalyticsBottom(List<JointTimeSeries> timeSeries, int frame)
        {
            double hipKneeAngle = HipKneeAngle(timeSeries[16].Y[frame],
                timeSeries[16].Z[frame], timeSeries[17].Y[frame], timeSeries[17].Z[frame]);

            double stanceWidth = timeSeries[19].X[frame] - timeSeries[15].X[frame];
            double shoulderWidth = timeSeries[8].X[frame] - timeSeries[4].X[frame];
            double relativeStanceWidth = stanceWidth / shoulderWidth;

            Dictionary<string,double> r = new Dictionary<string, double> { };
            r.Add("HipKneeAngle", hipKneeAngle);
            r.Add("RelativeStanceWidth", relativeStanceWidth);

            return r;
        }

        private static double HipKneeAngle(double hh, double hd, double kh, double kd)
        {
            var deltaD = hd - kd;
            var deltaH = hh - kh;
            return Math.Atan(deltaH / deltaD)*180/Math.PI;
        }

        
    }
}
