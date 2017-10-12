using Newtonsoft.Json;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer.Core
{
    public abstract class BaseDTO<TDTO>
        where TDTO : new()
    {
        public abstract int ID { get; set; }

        [JsonIgnore]
        public List<string> UpdateFilter { get; set; }
    }
}
