using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.FlexEngine
{
    //public class FlexResponse
    //{

    //    public FlexResponse()
    //    {
    //        Results = new Dictionary<string, FlexResponseEntity>();
    //    }

    //    public Dictionary<string, FlexResponseEntity> Results { get; set; }
    //}

    //public class FlexResponseEntity
    //{
    //    public dynamic Result { get; set; }

    //    public List<dynamic> Details { get; set; }

    //}

    ////public class FlexRequestEntity
    ////{
    ////    public List<FlexEntity> Requests { get; set; }
    ////}

    ////public class FlexEntity
    ////{
    ////    public string Type { get; set; }

    ////    [JsonConverter(typeof(StringEnumConverter))]
    ////    public FlexRequestType RequestType { get; set; }

    ////    public string Name { get; set; }

    ////    public dynamic Entity { get; set; }

    ////    public List<FlexEntity> Details { get; set; }
    ////}

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
