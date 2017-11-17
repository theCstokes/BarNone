﻿using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BarNone.DataLift.DataModel.KinectData
{
    public class BodyData : BaseChildDomainModel<BodyData, BodyDataDTO, BodyData, BodyDataDTO>,
        IDetailDTOTransformable<BodyDataDTO, BodyDataDetailDTO>
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
        public List<BodyDataFrame> DataFrames { get; set; } = new List<BodyDataFrame>();

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
            if (DataFrames == null)
            {
                DataFrames = new List<BodyDataFrame>();
            }

            DataFrames.Add(df);
        }

        public override BodyDataDTO BuildDTO()
        {
            return new BodyDataDTO
            {
                ID = ID,
                RecordTimeStamp = RecordDate,
            };
        }

        public BodyDataDetailDTO CreateDTO()
        {
            var ret = new BodyDataDetailDTO()
            {
                OrderedFrames = DataFrames?.Select(x => x.BuildDTO()).ToList()
            };

            DataFrames.Select(x => x?.CreateDTO());
            return ret;

        }

        public override BodyDataDTO BuildDTO(BodyDataDTO parentDTO)
        {
            return new BodyDataDTO
            {
                ID = this.ID,
                RecordTimeStamp = this.RecordDate
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
            DataFrames = dto.Details?.OrderedFrames.Select(
                joint => BodyDataFrame.CreateFromDTO(joint, this)).ToList();
        }

        #endregion

        #region IDetailDomainModel Implementation.
        dynamic IDetailDTOTransformable.CreateDTO()
        {
            return CreateDTO();
        }
        #endregion
    }
}
