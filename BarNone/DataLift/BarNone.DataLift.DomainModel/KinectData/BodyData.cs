//using BarNone.DataLift.DomainModel.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Kinect;
using BarNone.Shared.DataTransfer.Types;

namespace BarNone.DataLift.DomainModel.KinectData
{
    public class BodyData : BaseChildDomainModel<BodyData, BodyDataDTO, BodyData, BodyDataDTO>,
        IDetailDomainModel<BodyDataDTO, BodyDataDetailDTO>
    {
        #region Properties
        public override int ID { get; set; }
        /// <summary>
        /// The date and time of the record's start time
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// List of all body data for a given Record
        /// </summary>
        public IList<BodyDataFrame> DataFrames { get; set; }

        /// <summary>
        /// List of the body data stored internally for controlled modification
        /// </summary>
        private List<BodyDataFrame> InternalRecordDate { get; set; }

        #endregion

        #region Constructor(s)
        /// <summary>
        /// Creates a new Body Data Record
        /// </summary>

        #endregion

        #region API Method(s)
        /// <summary>
        /// Add a new frame to the Body Data Record
        ///     Appends <paramref name="df"/> to the end of <see cref="DataFrames"/>
        /// </summary>
        /// <param name="df">Data Frame being added</param>
        public void AddNewFrame(BodyDataFrame df)
        {
            if(DataFrames == null)
            {
                DataFrames = new List<BodyDataFrame>();
            }

            DataFrames.Add(df);
        }

        public override BodyDataDTO BuildDTO()
        {
            return new BodyDataDTO
            {
                ID = this.ID,
                RecordTimeStamp = this.RecordDate,
                Details = BuildDetailDTO(),
            };
        }

        public BodyDataDetailDTO BuildDetailDTO()
        {
            try
            {
                return new BodyDataDetailDTO()
                {
                    OrderedFrames = this.DataFrames.Select(x => x.BuildDTO()).ToList()
                };
            }

            catch (System.ArgumentNullException)
            {
                return null;
            }

        }

        public override BodyDataDTO BuildDTO(BodyDataDTO parentDTO)
        {
            return new BodyDataDTO
            {
                ID = this.ID,
                RecordTimeStamp = this.RecordDate,
                Details = BuildDetailDTO(),
            };
        }

        public override void PopulateFromDTO(BodyDataDTO dto)
        {
            CreateDMfromDTO(dto);
        }

        public override void PopulateFromDTO(BodyDataDTO dto, BodyData parent)
        {
            CreateDMfromDTO(dto);
        }

        private void CreateDMfromDTO(BodyDataDTO dto)
        {
            ID = dto.ID;
            RecordDate = dto.RecordTimeStamp;

            DataFrames = dto.Details.OrderedFrames.Select(
            joint => BodyDataFrame.CreateFromDTO(joint, this)).ToList();
        }

        #endregion
    }
}
