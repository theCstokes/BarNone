using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer
{
    public class LiftFolderDetailDTO : BaseDetailDTO<LiftFolderDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftFolderDTO> SubFolders { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftDTO> Lifts { get; set; }

        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public LiftFolderDTO Parent { get; set; }
    }

    public class LiftFolderDTO : BaseParentDTO<LiftFolderDTO, LiftFolderDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public string Name { get; set; }
    }
}
