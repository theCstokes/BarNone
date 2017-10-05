using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataTransfer
{
    public abstract class BaseDTO
    {
        public abstract int ID { get; set; }

        public List<UpdateRequestDTO> UpdateFilter { get; set; }

    }
}
