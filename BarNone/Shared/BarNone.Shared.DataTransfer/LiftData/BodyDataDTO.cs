using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// Body data detail.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDetailDTO{BarNone.Shared.DataTransfer.BodyDataDetailDTO}" />
    public class BodyDataDetailDTO : BaseDetailDTO<BodyDataDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<BodyDataFrameDTO> OrderedFrames { get; set; }
    }

    /// <summary>
    /// Body data.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseParentDTO{BarNone.Shared.DataTransfer.BodyDataDTO, BarNone.Shared.DataTransfer.BodyDataDetailDTO}" />
    public class BodyDataDTO : BaseParentDTO<BodyDataDTO, BodyDataDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public DateTime RecordTimeStamp { get; set; }
    }

}
