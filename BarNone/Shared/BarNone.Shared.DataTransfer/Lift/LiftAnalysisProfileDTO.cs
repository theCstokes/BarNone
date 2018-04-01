using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class LiftAnalysisProfileDetailDTO : BaseDetailDTO<LiftAnalysisProfileDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public LiftTypeDTO LiftType { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public List<AccelerationAnalysisCriteriaDTO> AccelerationAnalysisCriteria { get; set; }

        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public List<PositionAnalysisCriteriaDTO> PositionAnalysisCriteria { get; set; }

        [JsonProperty(Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public List<SpeedAnalysisCriteriaDTO> SpeedAnalysisCriteria { get; set; }

        [JsonProperty(Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public List<AngleAnalysisCriteriaDTO> AngleAnalysisCriteria { get; set; }
    }

    public class LiftAnalysisProfileDTO : BaseParentDTO<LiftAnalysisProfileDTO, LiftAnalysisProfileDetailDTO>
    {
        public override int ID { get; set; }

        public int UserID { get; set; }

        public int LiftTypeID { get; set; }
    }
}
