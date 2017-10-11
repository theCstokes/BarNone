﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataTransfer.Core;

namespace TheRack.DataTransfer.Lift
{
    public class LiftFolderDetailDTO : BaseDetailDTO<LiftFolderDetailDTO>
    {
        [JsonProperty(Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftFolderDTO> SubFolders { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public List<LiftDTO> Lifts { get; set; }
    }

    public class LiftFolderDTO : BaseParentDTO<LiftFolderDTO, LiftFolderDetailDTO>
    {
        [JsonProperty(Order = 0)]
        public override int ID { get; set; }

        [JsonProperty(Order = 1)]
        public string Name { get; set; }
    }
}
