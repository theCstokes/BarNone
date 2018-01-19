using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    /// <summary>
    /// An entity that is trackable as unique.
    /// </summary>
    /// <typeparam name="TDetailDTO">The type of the detail dto.</typeparam>
    /// <seealso cref="BarNone.Shared.Core.ITrackableDTO" />
    public interface ITrackableDetailDTO<TDetailDTO> : ITrackableDTO
        where TDetailDTO : ITrackableDTO
    {
        TDetailDTO Details { get; set; }
    }
}
