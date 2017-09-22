using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheRack.ResourceServer
{
    public static class Json
    {
        public static string Stringify<TSource>(TSource source)
        {
            return JsonConvert.SerializeObject(source,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static TSource Parse<TSource>(string value)
        {
            return JsonConvert.DeserializeObject<TSource>(value,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}