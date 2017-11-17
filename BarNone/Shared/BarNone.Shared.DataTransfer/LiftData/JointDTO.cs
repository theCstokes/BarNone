using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.LiftData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BarNone.Shared.DataTransfer
{

    //public class JointDetailDTO : BaseDetailDTO<JointDetailDTO>
    //{
    //    //There are no details for a Joint
    //    //  when fetching a joint the entire object is always sent
    //}

    public class JointDTO : BaseDTO<JointDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        //[JsonConverter(typeof(StringEnumConverter))]
        public JointTypeDTO JointType { get; set; }

        [JsonProperty(Order = 2)]
        public float PositionX { get; set; }

        [JsonProperty(Order = 3)]
        public float PositionY { get; set; }

        [JsonProperty(Order = 4)]
        public float PositionZ { get; set; }

        [JsonProperty(Order = 5)]
        //[JsonConverter(typeof(StringEnumConverter))]
        public TrackingStateDTO TrackingState { get; set; }

    }

}
