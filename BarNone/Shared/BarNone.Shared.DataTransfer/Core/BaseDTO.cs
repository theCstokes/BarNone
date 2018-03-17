using BarNone.Shared.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BarNone.Shared.DataTransfer.Core
{
    /// <summary>
    /// Base dto.
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <seealso cref="BarNone.Shared.Core.ITrackableDTO{TDTO}" />
    public abstract class BaseDTO<TDTO> : ITrackableDTO<TDTO>
        where TDTO : ITrackableDTO<TDTO>, new()
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public abstract int ID { get; set; }

        /// <summary>
        /// Gets or sets the update filter.
        /// </summary>
        /// <value>
        /// The update filter.
        /// </value>
        [JsonIgnoreDeserialize]
        public List<string> UpdateFilter { get; set; }
    }
}
