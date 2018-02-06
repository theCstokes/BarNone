using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// Lift details dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDetailDTO{BarNone.Shared.DataTransfer.LiftDetailDTO}" />
    public class LiftDetailDTO : BaseDetailDTO<LiftDetailDTO>
    {
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public LiftFolderDTO Parent { get; set; }

        /// <summary>
        /// Gets or sets the body data.
        /// </summary>
        /// <value>
        /// The body data.
        /// </value>
        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public BodyDataDTO BodyData { get; set; }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        /// <value>
        /// The video.
        /// </value>
        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public VideoDTO Video { get; set; }

        [JsonProperty(Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftPermissionDTO> Permissions { get; set; }
    }

    /// <summary>
    /// Lift dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseParentDTO{BarNone.Shared.DataTransfer.LiftDTO, BarNone.Shared.DataTransfer.LiftDetailDTO}" />
    public class LiftDTO : BaseParentDTO<LiftDTO, LiftDetailDTO>
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [JsonProperty(Order = 2)]
        public int? ParentID { get; set; }

        /// <summary>
        /// Gets or sets the body data identifier.
        /// </summary>
        /// <value>
        /// The body data identifier.
        /// </value>
        [JsonProperty(Order = 3)]
        public int? BodyDataID { get; set; }
    }
}
