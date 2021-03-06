﻿using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Position
{
    public class AR_Position : RequestEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EJointType JointType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EDimension Dimension { get; set; }
    }
}
