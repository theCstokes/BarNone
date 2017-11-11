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
    public interface IRepository
    {
        List<dynamic> Get(WhereFunc where = null);

        dynamic Get(int id);

        dynamic GetWithDetails(int id);

        dynamic Create(dynamic dto);

        dynamic Update(int id, dynamic dto);

        dynamic Remove(int id);
    }

    public interface IRepository<TDTO, TDomainModel> : IRepository
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
    {
        new List<TDomainModel> Get(WhereFunc where = null);

        new TDomainModel Get(int id);

        new TDomainModel GetWithDetails(int id);

        TDomainModel Create(TDTO dto);

        TDomainModel Update(int id, TDTO dto);

        new TDomainModel Remove(int id);
    }

    public interface ITest
    {
        dynamic Create<TDTO>(TDTO dto) where TDTO : BaseDTO<TDTO>, new();
    }

    public class BaseTest<TDTO> : ITest
        where TDTO : BaseDTO<TDTO>, new()
    {
        public dynamic Create<TDTO>(TDTO dto) where TDTO : BaseDTO<TDTO>, new()
        {
            throw new NotImplementedException();
        }
    }
}
