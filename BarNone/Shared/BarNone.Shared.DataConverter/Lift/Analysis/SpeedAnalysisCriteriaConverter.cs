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
    public class SpeedAnalysisCriteriaConverter 
        : BaseDetailDataConverter<SpeedAnalysisCriteria, SpeedAnalysisCriteriaDTO, 
            SpeedAnalysisCriteriaDetailDTO, Converters>
    {
        public SpeedAnalysisCriteriaConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override SpeedAnalysisCriteria OnCreateDataModel(SpeedAnalysisCriteriaDTO dto)
        {
            return new SpeedAnalysisCriteria
            {
                ID = dto.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = dto.JointTypeID
            };
        }

        public override void OnCreateDetailDataModel(SpeedAnalysisCriteria data, SpeedAnalysisCriteriaDetailDTO dto)
        {
            data.JointType = converterContext.JointType.CreateDataModel(dto.JointType);
        }

        public override SpeedAnalysisCriteriaDetailDTO OnCreateDetailDTO(SpeedAnalysisCriteria data)
        {
            return new SpeedAnalysisCriteriaDetailDTO
            {
                JointType = converterContext.JointType.CreateDTO(data.JointType)
            };
        }

        public override SpeedAnalysisCriteriaDTO OnCreateDTO(SpeedAnalysisCriteria data)
        {
            return new SpeedAnalysisCriteriaDTO
            {
                ID = data.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = data.JointTypeID
            };
        }
    }
}
