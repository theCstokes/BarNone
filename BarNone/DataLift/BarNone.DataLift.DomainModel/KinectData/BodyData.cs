using System;
using System.Collections.Generic;

namespace BarNone.DataLift.DomainModel.KinectData
{
    class BodyData
    {
        #region Properties
        /// <summary>
        /// The date and time of the record's start time
        /// </summary>
        public DateTime RecordDate;

        /// <summary>
        /// List of all body data for a given Record
        /// </summary>
        public IReadOnlyList<BodyDataFrame> DataFrames
        {
            get
            {
                return InternalRecordDate.AsReadOnly();
            }
        }

        /// <summary>
        /// List of the body data stored internally for controlled modification
        /// </summary>
        private List<BodyDataFrame> InternalRecordDate;

        #endregion

        #region Constroctor(s)
        /// <summary>
        /// Creates a new Body Data Record
        /// </summary>
        public BodyData()
        {
            RecordDate = DateTime.Now;
            InternalRecordDate = new List<BodyDataFrame>();
        }

        #endregion

        #region API Method(s)
        /// <summary>
        /// Add a new frame to the Body Data Record
        ///     Appends <paramref name="df"/> to the end of <see cref="DataFrames"/>
        /// </summary>
        /// <param name="df">Data Frame being added</param>
        public void AddNewFrame(BodyDataFrame df)
        {
            InternalRecordDate.Add(df);
        }
        #endregion

    }
}
