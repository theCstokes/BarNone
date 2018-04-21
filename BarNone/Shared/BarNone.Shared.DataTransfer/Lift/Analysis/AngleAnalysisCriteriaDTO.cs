using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.LiftData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class AngleAnalysisCriteriaDetailDTO : BaseDetailDTO<AngleAnalysisCriteriaDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public JointTypeDTO JointTypeA { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public JointTypeDTO JointTypeB { get; set; }

        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public JointTypeDTO JointTypeC { get; set; }
    }

    public class AngleAnalysisCriteriaDTO : BaseParentDTO<AngleAnalysisCriteriaDTO, AngleAnalysisCriteriaDetailDTO>
    {
        public override int ID { get; set; }

        public int UserID { get; set; }

        public int JointTypeAID { get; set; }

        public int JointTypeBID { get; set; }

        public int JointTypeCID { get; set; }
    }
}
