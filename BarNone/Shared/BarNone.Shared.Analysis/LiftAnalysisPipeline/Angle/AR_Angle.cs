﻿using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Angle
{
    public class AR_Angle : RequestEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EJointType JointA { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EJointType JointB { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EJointType JointC { get; set; }
    }
}
