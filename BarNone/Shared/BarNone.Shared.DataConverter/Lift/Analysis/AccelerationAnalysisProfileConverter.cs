using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.Core;

namespace BarNone.Shared.DataConverter
{
    public class AccelerationAnalysisProfileConverter 
        : BaseDetailDataConverter<AccelerationAnalysisProfile, AccelerationAnalysisProfileDTO, AccelerationAnalysisProfileDetailDTO, Converters>
    {
        public AccelerationAnalysisProfileConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override AccelerationAnalysisProfile OnCreateDataModel(AccelerationAnalysisProfileDTO dto)
        {
            return new AccelerationAnalysisProfile
            {
                ID = dto.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = dto.JointTypeID
            };
        }

        public override void OnCreateDetailDataModel(AccelerationAnalysisProfile data, AccelerationAnalysisProfileDetailDTO dto)
        {
            data.JointType = converterContext.JointType.CreateDataModel(dto.JointType);
        }

        public override AccelerationAnalysisProfileDetailDTO OnCreateDetailDTO(AccelerationAnalysisProfile data)
        {
            return new AccelerationAnalysisProfileDetailDTO
            {
                JointType = converterContext.JointType.CreateDTO(data.JointType)
            };
        }

        public override AccelerationAnalysisProfileDTO OnCreateDTO(AccelerationAnalysisProfile data)
        {
            return new AccelerationAnalysisProfileDTO
            {
                ID = data.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = data.JointTypeID
            };
        }
    }
}
