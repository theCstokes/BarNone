using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.Repository.Core
{
    public interface IRepository<TDTO, TDomainModel>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel>, new()
    {
        List<TDomainModel> Get();

        TDomainModel Get(int id);

        TDomainModel GetWithDetails(int id);

        TDomainModel Create(TDTO dto);

        TDomainModel Update(int id, TDTO dto);

        TDomainModel Remove(int id);
    }
}
