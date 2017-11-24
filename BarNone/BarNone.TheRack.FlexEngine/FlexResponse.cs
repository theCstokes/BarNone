using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace BarNone.TheRack.FlexEngine
{
    public class FlexRequestDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FlexRequestType RequestType { get; set; }

        public List<FlexRequestEntityDTO> Requests { get; set; }
    }

    public class FlexRequestEntityDTO
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public dynamic Entity { get; set; }

        public List<FlexRequestEntityDTO> Details { get; set; }
    }

    public class FlexResponseDTO
    {
        public Dictionary<string, dynamic> Results { get; set; }
    }

    public class FlexResposeEntityDTO
    {
        public dynamic Result { get; set; }

        public Dictionary<string, dynamic> Details { get; set; }
    }
}
