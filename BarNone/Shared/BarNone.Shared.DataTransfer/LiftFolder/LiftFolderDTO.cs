using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer
{
    public class LiftFolderDetailDTO : BaseDetailDTO<LiftFolderDetailDTO>
    {
        /// <summary>
        /// Gets or sets the sub folders.
        /// </summary>
        /// <value>
        /// The sub folders.
        /// </value>
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftFolderDTO> SubFolders { get; set; }

        /// <summary>
        /// Gets or sets the lifts.
        /// </summary>
        /// <value>
        /// The lifts.
        /// </value>
        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftDTO> Lifts { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public LiftFolderDTO Parent { get; set; }
    }

    public class LiftFolderDTO : BaseParentDTO<LiftFolderDTO, LiftFolderDetailDTO>
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
    }
}
