using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Core
{
    public interface IDTO
    {
        int ID { get; set; }
    }

    //public interface IParentDTO : IDTO
    //{
    //    dynamic Details { get; set; }
    //}

    //public interface IParentDTO<TDetailDTO> : IDTO
    //{
    //    dynamic Details { get; set; }
    //}
}
