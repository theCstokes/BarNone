using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Core;
using BarNone.TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

namespace BarNone.TheRack.Repository.Core
{
    public interface IRepository<TDTO, TDomainModel>
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
    {
        List<TDomainModel> Get(WhereFunc where = null);

        TDomainModel Get(int id);

        TDomainModel GetWithDetails(int id);

        TDomainModel Create(TDTO dto);

        TDomainModel Update(int id, TDTO dto);

        TDomainModel Remove(int id);
    }
}
