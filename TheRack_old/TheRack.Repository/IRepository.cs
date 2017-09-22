using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess;
using TheRack.DomainModel;

namespace TheRack.Repository
{
    public interface IRepository
    {
        IList Get(DomainContext dc);
    }

    public interface IRepository<TDomainModel> : IRepository
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        new List<TDomainModel> Get(DomainContext dc);
    }
}
