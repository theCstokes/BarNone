using TheRack.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DomainModel
{
    public abstract class BaseDomainModel<TModel> : BaseModel<TModel>
        where TModel : class, new()
    {
    }
}
