using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.LiftData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BarNone.Shared.DataTransfer
{
    public class JointDTO : BaseDTO<JointDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public JointTypeDTO JointType { get; set; }

        [JsonProperty(Order = 2)]
        public float PositionX { get; set; }

        [JsonProperty(Order = 3)]
        public float PositionY { get; set; }

        [JsonProperty(Order = 4)]
        public float PositionZ { get; set; }

        [JsonProperty(Order = 5)]
        public TrackingStateDTO TrackingState { get; set; }

    }

}
