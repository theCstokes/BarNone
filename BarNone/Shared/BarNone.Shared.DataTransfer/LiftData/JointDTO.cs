using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.LiftData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BarNone.Shared.DataTransfer
{
    public class JointDetailDTO : BaseDetailDTO<JointDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public JointTypeDTO JointType { get; set; }

        [JsonProperty(Order = 1)]
        public JointTrackingStateTypeDTO JointTrackingStateType { get; set; }

        [JsonProperty(Order = 2)]
        public BodyDataFrameDTO BodyDataFrame { get; set; }
    }

    public class JointDTO : BaseParentDTO<JointDTO, JointDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public int JointTypeID { get; set; }

        [JsonProperty(Order = 2)]
        public float X { get; set; }

        [JsonProperty(Order = 3)]
        public float Y { get; set; }

        [JsonProperty(Order = 4)]
        public float Z { get; set; }

        [JsonProperty(Order = 5)]
        public int JointTrackingStateTypeID { get; set; }

        [JsonProperty(Order = 6)]
        public int BodyDataFrameID { get; set; }
    }

}
