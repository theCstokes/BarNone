using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer
{
    public class BodyDataDetailDTO : BaseDetailDTO<BodyDataDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<BodyDataFrameDTO> OrderedFrames { get; set; }
    }

    public class BodyDataDTO : BaseParentDTO<BodyDataDTO, BodyDataDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public DateTime RecordTimeStamp { get; set; }

    }

}
