using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DomainModel.Core
{
    public interface IDomainModel
    {
        int ID { get; set; }

        dynamic BuildDTO();
    }

    public interface IDomainModel<TDTO> : IDomainModel
        where TDTO : BaseDTO<TDTO>, new()
    {
        new TDTO BuildDTO();
    }

    public interface IParentDomainModel : IDomainModel
    {
        dynamic BuildDetailDTO();
    }

    public interface IParentDomainModel<TDTO, TDetailDTO> : IDomainModel<TDTO>, IParentDomainModel
        where TDTO : BaseDTO<TDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        new TDetailDTO BuildDetailDTO();
    }
}
