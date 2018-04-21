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
    public class AccelerationAnalysisCriteriaConverter 
        : BaseDetailDataConverter<AccelerationAnalysisCriteria, AccelerationAnalysisCriteriaDTO, AccelerationAnalysisCriteriaDetailDTO, Converters>
    {
        public AccelerationAnalysisCriteriaConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override AccelerationAnalysisCriteria OnCreateDataModel(AccelerationAnalysisCriteriaDTO dto)
        {
            return new AccelerationAnalysisCriteria
            {
                ID = dto.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = dto.JointTypeID
            };
        }

        public override void OnCreateDetailDataModel(AccelerationAnalysisCriteria data, AccelerationAnalysisCriteriaDetailDTO dto)
        {
            data.JointType = converterContext.JointType.CreateDataModel(dto.JointType);
        }

        public override AccelerationAnalysisCriteriaDetailDTO OnCreateDetailDTO(AccelerationAnalysisCriteria data)
        {
            return new AccelerationAnalysisCriteriaDetailDTO
            {
                JointType = converterContext.JointType.CreateDTO(data.JointType)
            };
        }

        public override AccelerationAnalysisCriteriaDTO OnCreateDTO(AccelerationAnalysisCriteria data)
        {
            return new AccelerationAnalysisCriteriaDTO
            {
                ID = data.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = data.JointTypeID
            };
        }
    }
}
