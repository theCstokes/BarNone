using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Response
{
    public class ResponseEntityDTO<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
    {
        public TDTO Entity { get; set; }
    }
}
