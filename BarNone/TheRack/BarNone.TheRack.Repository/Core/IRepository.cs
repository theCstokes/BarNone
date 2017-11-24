using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DTOTransformable;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

namespace BarNone.TheRack.Repository.Core
{
    public interface IRepository
    {
        List<IDomainModel> Get(WhereFunc where = null);

        IDomainModel Get(int id);

        IDomainModel GetWithDetails(int id);

        IDomainModel Create(dynamic dto);

        IDomainModel Update(int id, dynamic dto);

        IDomainModel Remove(int id);
    }

    public interface IRepository<TDomainModel, TDTO> : IRepository
        where TDTO : BaseDTO<TDTO>, new()
        where TDomainModel : class, IDomainModel<TDomainModel>, new()
    {
        new List<TDomainModel> Get(WhereFunc where = null);

        new TDomainModel Get(int id);

        new TDomainModel GetWithDetails(int id);

        TDomainModel Create(TDTO dto);

        TDomainModel Update(int id, TDTO dto);

        new TDomainModel Remove(int id);
    }

    //public interface ITest
    //{
    //    dynamic Create<TDTO>(TDTO dto) where TDTO : BaseDTO<TDTO>, new();
    //}

    //public class BaseTest<TDTO> : ITest
    //    where TDTO : BaseDTO<TDTO>, new()
    //{
    //    public dynamic Create<TDTO>(TDTO dto) where TDTO : BaseDTO<TDTO>, new()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
