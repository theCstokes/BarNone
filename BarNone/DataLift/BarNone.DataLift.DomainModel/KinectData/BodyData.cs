//using BarNone.DataLift.DomainModel.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BarNone.DataLift.DomainModel.KinectData
{
    internal class BodyData : BaseChildDomainModel<BodyData, BodyDataDTO,BodyData,BodyDataDTO>,
        IDetailDomainModel<BodyDataDTO,BodyDataDetailDTO>
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
        private List<BodyDataFrame> InternalRecordDate { get; set; }

        #endregion

        #region Constroctor(s)
        /// <summary>
        /// Creates a new Body Data Record
        /// </summary>
        public override BodyDataDTO BuildDTO()
        {
            return new BodyDataDTO
            {
                RecordTimeStamp = this.RecordDate,
                Details = BuildDetailDTO(),
            };
            
        }

        public override BodyDataDTO BuildDTO(BodyDataDTO parentDTO)
        {
            return new BodyDataDTO
            {
                RecordTimeStamp = this.RecordDate,
                Details = BuildDetailDTO(),
            };
        }

        public override void PopulateFromDTO(BodyDataDTO dto)
        {
            ID = dto.ID;
            RecordDate = dto.RecordTimeStamp;
            
        }

        public override void PopulateFromDTO(BodyDataDTO dto, BodyData parent)
        {
            ID = dto.ID;
            RecordDate = dto.RecordTimeStamp;
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

        public BodyDataDetailDTO BuildDetailDTO()
        {
            return new BodyDataDetailDTO()
            {
                OrderedFrames = this.DataFrames.Select(x => x.BuildDTO()).ToList()
            };
        }

        #endregion

    }
}
