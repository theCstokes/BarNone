using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Core
{
    //public interface IParentDTO : IDTO
    //{
    //    object Details { get; set; }
    //}

    public interface IParentDTO<TDetailDTO> : IDTO
        where TDetailDTO : IDTO, new()
    {
        TDetailDTO Details { get; set; }
    }
}
