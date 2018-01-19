using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DataTransfer.LiftData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// Joint detail dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDetailDTO{BarNone.Shared.DataTransfer.JointDetailDTO}" />
    public class JointDetailDTO : BaseDetailDTO<JointDetailDTO>
    {
        /// <summary>
        /// Gets or sets the type of the joint.
        /// </summary>
        /// <value>
        /// The type of the joint.
        /// </value>
        [JsonProperty(Order = 0)]
        public JointTypeDTO JointType { get; set; }

        /// <summary>
        /// Gets or sets the type of the joint tracking state.
        /// </summary>
        /// <value>
        /// The type of the joint tracking state.
        /// </value>
        [JsonProperty(Order = 1)]
        public JointTrackingStateTypeDTO JointTrackingStateType { get; set; }

        /// <summary>
        /// Gets or sets the body data frame.
        /// </summary>
        /// <value>
        /// The body data frame.
        /// </value>
        [JsonProperty(Order = 2)]
        public BodyDataFrameDTO BodyDataFrame { get; set; }
    }

    /// <summary>
    /// Joint dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseParentDTO{BarNone.Shared.DataTransfer.JointDTO, BarNone.Shared.DataTransfer.JointDetailDTO}" />
    public class JointDTO : BaseParentDTO<JointDTO, JointDetailDTO>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the joint type identifier.
        /// </summary>
        /// <value>
        /// The joint type identifier.
        /// </value>
        [JsonProperty(Order = 1)]
        public int JointTypeID { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [JsonProperty(Order = 2)]
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [JsonProperty(Order = 3)]
        public float Y { get; set; }

        [JsonProperty(Order = 4)]
        public float Z { get; set; }

        /// <summary>
        /// Gets or sets the joint tracking state type identifier.
        /// </summary>
        /// <value>
        /// The joint tracking state type identifier.
        /// </value>
        [JsonProperty(Order = 5)]
        public int JointTrackingStateTypeID { get; set; }

        /// <summary>
        /// Gets or sets the body data frame identifier.
        /// </summary>
        /// <value>
        /// The body data frame identifier.
        /// </value>
        [JsonProperty(Order = 6)]
        public int BodyDataFrameID { get; set; }
    }

}
