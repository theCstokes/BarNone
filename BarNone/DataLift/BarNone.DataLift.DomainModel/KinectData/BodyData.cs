using BarNone.DataLift.DomainModel.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarNone.DataLift.DataModel.KinectData
{
    public class BodyData : IDataModel<BodyData>
    {
        #region Properties
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

        //#region Abstract Impl
        //protected override BodyDataDetailDTO OnBuildDetailDTO(ConvertConfig config)
        //{
        //    var ret = new BodyDataDetailDTO()
        //    {
        //        OrderedFrames = DataFrames?.Select(x => x.CreateDTO(config)).ToList()
        //    };

        //    //DataFrames.Select(x => x?.CreateDTO());
        //    return ret;
        //}

        //protected override BodyDataDTO OnBuildDTO()
        //{
        //    return new BodyDataDTO
        //    {
        //        //TODO ID DUH DO IT
        //        //ID = this.ID, 
        //        RecordTimeStamp = this.RecordDate
        //    };
        //}

        //protected override void OnPopulate(BodyDataDTO dto, ConvertConfig config = null)
        //{
        //    RecordDate = dto.RecordTimeStamp;
        //}

        //protected override void OnDetailPopulate(BodyDataDetailDTO dto, ConvertConfig config = null)
        //{
        //    DataFrames = dto.OrderedFrames.Select(
        //        joint => BodyDataFrame.CreateFromDTO(joint)).ToList();
        //}



        //#endregion
    }
}
