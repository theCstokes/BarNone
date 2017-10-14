using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Response
{
    public class ResponseEnumerableDTO<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
    {
        public int Count { get; set; }

        public IEnumerable<TDTO> Entities { get; set; }
    }
}
