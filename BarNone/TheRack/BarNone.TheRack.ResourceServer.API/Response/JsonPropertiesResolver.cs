using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using BarNone.Shared.Core;

namespace BarNone.TheRack.ResourceServer.API.Response
{
    public class JsonPropertiesResolver : DefaultContractResolver
    {
        
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization); ;

            if (Attribute.IsDefined(member, typeof(JsonIgnoreDeserializeAttribute)))
            {
                property.ShouldDeserialize = o => false;
            }
            return property;
        }
    }
}
