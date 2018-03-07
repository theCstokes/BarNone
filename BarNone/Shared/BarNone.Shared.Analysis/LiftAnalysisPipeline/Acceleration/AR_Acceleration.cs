using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Acceleration
{
    public class AR_Acceleration : RequestEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EJointType JointType { get; set; }
    }
}
