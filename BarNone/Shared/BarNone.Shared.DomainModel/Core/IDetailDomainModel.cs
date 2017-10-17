using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DomainModel.Core
{
    public interface IDetailDomainModel<TDTO, TDetailDTO>
        //where TDomainModel : BaseDomainModel<TDomainModel, TDTO>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        TDetailDTO BuildDetailDTO();
    }
}
