using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    public interface ITrackable
    {
        int ID { get; set; }
    }

    public interface ITrackable<T> : ITrackable
        where T : ITrackable<T>, new()
    {
    }
}
