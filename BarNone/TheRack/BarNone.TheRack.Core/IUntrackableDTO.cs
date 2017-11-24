using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    public interface IUntrackableDTO
    {
    }

    public interface IUntrackableDTO<TDTO> : IUntrackableDTO
        where TDTO : new()
    {
    }
}
