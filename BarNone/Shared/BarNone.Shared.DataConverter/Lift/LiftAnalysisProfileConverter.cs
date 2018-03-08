using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.Core;
using System.Linq;

namespace BarNone.Shared.DataConverter
{
    public class LiftAnalysisProfileConverter 
        : BaseDetailDataConverter<LiftAnalysisProfile, LiftAnalysisProfileDTO, LiftAnalysisProfileDetailDTO, Converters>
    {
        public LiftAnalysisProfileConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override LiftAnalysisProfile OnCreateDataModel(LiftAnalysisProfileDTO dto)
        {
            return new LiftAnalysisProfile
            {
                ID = dto.ID,
                UserID = context?.UserID ?? 0,
                LiftTypeID = dto.LiftTypeID
            };
        }

        public override void OnCreateDetailDataModel(LiftAnalysisProfile data, LiftAnalysisProfileDetailDTO dto)
        {
            data.LiftType = converterContext.LiftType.CreateDataModel(dto.LiftType);
            data.AccelerationAnalysis = dto.AccelerationAnalysis?.Select(a => converterContext.AccelerationAnalysisProfile.CreateDataModel(a)).ToList();
        }

        public override LiftAnalysisProfileDetailDTO OnCreateDetailDTO(LiftAnalysisProfile data)
        {
            return new LiftAnalysisProfileDetailDTO
            {
                LiftType = converterContext.LiftType.CreateDTO(data.LiftType),
                AccelerationAnalysis = data.AccelerationAnalysis?.Select(a => converterContext.AccelerationAnalysisProfile.CreateDTO(a)).ToList()
            };
        }

        public override LiftAnalysisProfileDTO OnCreateDTO(LiftAnalysisProfile data)
        {
            return new LiftAnalysisProfileDTO
            {
                ID = data.ID,
                UserID = context?.UserID ?? 0,
                LiftTypeID = data.LiftTypeID
            };
        }
    }
}
