using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Core
{
    public interface IParentDTO : IDTO
    {
        dynamic Details { get; set; }
    }

    public interface IParentDTO<TDetailDTO> : IParentDTO
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
    {
        new TDetailDTO Details { get; set; }
    }
}
