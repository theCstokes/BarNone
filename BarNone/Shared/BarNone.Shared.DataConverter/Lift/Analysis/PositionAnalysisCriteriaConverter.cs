using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter
{
    public class PositionAnalysisCriteriaConverter
        : BaseDetailDataConverter<PositionAnalysisCriteria, PositionAnalysisCriteriaDTO,
            PositionAnalysisCriteriaDetailDTO, Converters>
    {
        public PositionAnalysisCriteriaConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override PositionAnalysisCriteria OnCreateDataModel(PositionAnalysisCriteriaDTO dto)
        {
            return new PositionAnalysisCriteria
            {
                ID = dto.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = dto.JointTypeID
            };
        }

        public override void OnCreateDetailDataModel(PositionAnalysisCriteria data, PositionAnalysisCriteriaDetailDTO dto)
        {
            data.JointType = converterContext.JointType.CreateDataModel(dto.JointType);
        }

        public override PositionAnalysisCriteriaDetailDTO OnCreateDetailDTO(PositionAnalysisCriteria data)
        {
            return new PositionAnalysisCriteriaDetailDTO
            {
                JointType = converterContext.JointType.CreateDTO(data.JointType)
            };
        }

        public override PositionAnalysisCriteriaDTO OnCreateDTO(PositionAnalysisCriteria data)
        {
            return new PositionAnalysisCriteriaDTO
            {
                ID = data.ID,
                UserID = context?.UserID ?? 0,
                JointTypeID = data.JointTypeID
            };
        }
    }
}
