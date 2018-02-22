using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarNone.Shared.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonIgnoreDeserializeAttribute : Attribute
    {
    }
}
