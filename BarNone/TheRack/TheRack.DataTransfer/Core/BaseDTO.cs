using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DataTransfer.Core
{
    public abstract class BaseDTO<TDTO>
        where TDTO : new()
    {
        public abstract int ID { get; set; }

        [JsonIgnore]
        public List<string> UpdateFilter { get; set; }
    }
}
