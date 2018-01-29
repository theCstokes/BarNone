using BarNone.DataLift.DomainModel.Core;
using System;
using System.Collections.Generic;

namespace BarNone.DataLift.DataModel.KinectData
{
    /// <summary>
    /// Kinect Body Data Domain Model
    /// </summary>
    public class BodyData : IDataModel<BodyData>
    {
        #region Properties
        /// <summary>
        /// ID of the item on the DB
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The date and time of the record's start time
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// List of all body data for a given Record
        /// </summary>
        public List<BodyDataFrame> DataFrames { get; set; } = new List<BodyDataFrame>();

        #endregion

        #region Public Member(s).
        /// <summary>
        /// Add a new frame to the Body Data Record
        ///     Appends <paramref name="df"/> to the end of <see cref="DataFrames"/>
        /// </summary>
        /// <param name="df">Data Frame being added</param>
        public void AddNewFrame(BodyDataFrame df)
        {
            if (DataFrames == null)
            {
                DataFrames = new List<BodyDataFrame>();
            }

            DataFrames.Add(df);
        }
        #endregion
        
    }
}
