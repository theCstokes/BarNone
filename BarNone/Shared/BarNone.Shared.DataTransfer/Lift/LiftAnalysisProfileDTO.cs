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

        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<AccelerationAnalysisProfileDTO> AccelerationAnalysis { get; set; }
    }

    public class LiftAnalysisProfileDTO : BaseParentDTO<LiftAnalysisProfileDTO, LiftAnalysisProfileDetailDTO>
    {
        public override int ID { get; set; }

        public int UserID { get; set; }

        public int LiftTypeID { get; set; }
    }
}
