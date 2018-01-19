using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    /// <summary>
    /// An entity that is not trackable as unique.
    /// </summary>
    public interface IUntrackableDTO
    {
    }

    /// <summary>
    /// An entity that is not trackable as unique.
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    public interface IUntrackableDTO<TDTO> : IUntrackableDTO
        where TDTO : new()
    {
    }
}
