using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataModel
{
    public abstract class BaseModel<TModel> where TModel : class, new()
    {
        public abstract int ID { get; set; }
    }
}
