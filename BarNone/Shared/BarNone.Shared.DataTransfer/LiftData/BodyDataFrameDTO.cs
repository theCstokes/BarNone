using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// Body data frame detail dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDetailDTO{BarNone.Shared.DataTransfer.BodyDataFrameDetailDTO}" />
    public class BodyDataFrameDetailDTO : BaseDetailDTO<BodyDataFrameDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<JointDTO> Joints { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public BodyDataDTO BodyData { get; set; }
    }

    /// <summary>
    /// Body data frame dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseParentDTO{BarNone.Shared.DataTransfer.BodyDataFrameDTO, BarNone.Shared.DataTransfer.BodyDataFrameDetailDTO}" />
    public class BodyDataFrameDTO : BaseParentDTO<BodyDataFrameDTO, BodyDataFrameDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public TimeSpan TimeOfFrame { get; set; }

        [JsonProperty(Order = 2)]
        public TimeSpan TimeUntilNextFrame { get; set; }

        [JsonProperty(Order = 3)]
        public int BodyDataID { get; set; }

    }
}
