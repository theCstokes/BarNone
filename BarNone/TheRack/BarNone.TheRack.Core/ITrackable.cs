using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    /// <summary>
    /// An entity that is trackable as unique.
    /// </summary>
    public interface ITrackable
    {
        int ID { get; set; }
    }

    /// <summary>
    /// An entity that is trackable as unique.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITrackable<T> : ITrackable
        where T : ITrackable<T>, new()
    {
    }
}
