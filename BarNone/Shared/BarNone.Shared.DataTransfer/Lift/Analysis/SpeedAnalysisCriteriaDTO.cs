﻿using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.LiftData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class SpeedAnalysisCriteriaDetailDTO : BaseDetailDTO<SpeedAnalysisCriteriaDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public JointTypeDTO JointType { get; set; }
    }

    public class SpeedAnalysisCriteriaDTO : BaseParentDTO<SpeedAnalysisCriteriaDTO, SpeedAnalysisCriteriaDetailDTO>
    {
        public override int ID { get; set; }

        public int UserID { get; set; }

        public int JointTypeID { get; set; }
    }
}
