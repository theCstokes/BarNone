using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    public interface ITrackableDTO : ITrackable
    {
        
    }

    public interface ITrackableDTO<T> : ITrackable<T>, ITrackableDTO
        where T : ITrackableDTO<T>, new()
    {

    }
}
