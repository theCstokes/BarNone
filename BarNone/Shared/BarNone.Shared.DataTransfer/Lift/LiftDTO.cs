using BarNone.Shared.DataTransfer.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class LiftDetailDTO : BaseDetailDTO<LiftDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public LiftFolderDTO Parent { get; set; }
    }

    public class LiftDTO : BaseParentDTO<LiftDTO, LiftDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2)]
        public int ParentID { get; set; }
    }
}
