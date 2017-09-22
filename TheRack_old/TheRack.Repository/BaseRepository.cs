using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataAccess;
using TheRack.DataAccessAdapter;
using TheRack.DomainModel;

namespace TheRack.Repository
{
    public abstract class BaseRepository<TDTO, TDomainModel> : IRepository<TDomainModel>
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        public abstract List<TDomainModel> Get(DomainContext dc);

        IList IRepository.Get(DomainContext dc)
        {
            return this.Get(dc);
        }
    }
}
