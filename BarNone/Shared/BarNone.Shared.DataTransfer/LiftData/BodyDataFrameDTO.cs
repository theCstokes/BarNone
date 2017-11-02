using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using BarNone.Shared.DataTransfer.Types;

namespace BarNone.Shared.DataTransfer
{

    public class BodyDataFrameDetailDTO : BaseDetailDTO<BodyDataFrameDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<DTOJointType, JointDTO> Joints { get; set; }
    }

    public class BodyDataFrameDTO : BaseParentDTO<BodyDataFrameDTO, BodyDataFrameDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public DateTime TimeOfFrame { get; set; }

        [JsonProperty(Order = 2)]
        public TimeSpan TimeUntilNextFrame { get; set; }

    }
}
