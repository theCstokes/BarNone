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
    public class AngleAnalysisProfileConverter 
        : BaseDetailDataConverter<AngleAnalysisCriteria, AngleAnalysisCriteriaDTO, AngleAnalysisCriteriaDetailDTO, Converters>
    {
        public AngleAnalysisProfileConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override AngleAnalysisCriteria OnCreateDataModel(AngleAnalysisCriteriaDTO dto)
        {
            return new AngleAnalysisCriteria
            {
                ID = dto.ID,
                UserID = context?.UserID ?? 0,
                JointTypeAID = dto.JointTypeAID,
                JointTypeBID = dto.JointTypeBID,
                JointTypeCID = dto.JointTypeCID
            };
        }

        public override void OnCreateDetailDataModel(AngleAnalysisCriteria data, AngleAnalysisCriteriaDetailDTO dto)
        {
            data.JointTypeA = converterContext.JointType.CreateDataModel(dto.JointTypeA);
            data.JointTypeB = converterContext.JointType.CreateDataModel(dto.JointTypeB);
            data.JointTypeC = converterContext.JointType.CreateDataModel(dto.JointTypeC);
        }

        public override AngleAnalysisCriteriaDetailDTO OnCreateDetailDTO(AngleAnalysisCriteria data)
        {
            return new AngleAnalysisCriteriaDetailDTO
            {
                JointTypeA = converterContext.JointType.CreateDTO(data.JointTypeA),
                JointTypeB = converterContext.JointType.CreateDTO(data.JointTypeB),
                JointTypeC = converterContext.JointType.CreateDTO(data.JointTypeC)
            };
        }

        public override AngleAnalysisCriteriaDTO OnCreateDTO(AngleAnalysisCriteria data)
        {
            return new AngleAnalysisCriteriaDTO
            {
                ID = data.ID,
                UserID = context?.UserID ?? 0,
                JointTypeAID = data.JointTypeAID,
                JointTypeBID = data.JointTypeBID,
                JointTypeCID = data.JointTypeCID
            };
        }
    }
}
