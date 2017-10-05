using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class UpdateRequest
    {
        public string Name { get; set; }
        public WhereOperation Operation { get; set; }
    }
}
