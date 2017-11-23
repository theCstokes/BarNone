using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    public interface ITrackableDetailDTO<TDetailDTO> : ITrackableDTO
        where TDetailDTO : ITrackableDTO
    {
        TDetailDTO Details { get; set; }
    }
}
