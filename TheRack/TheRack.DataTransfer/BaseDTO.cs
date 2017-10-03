using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DataTransfer
{
    public abstract class BaseDTO<TDTO>
        where TDTO : new()
    {
        public abstract int ID { get; set; }

        public List<string> UpdateFilter { get; set; }
    }
}
