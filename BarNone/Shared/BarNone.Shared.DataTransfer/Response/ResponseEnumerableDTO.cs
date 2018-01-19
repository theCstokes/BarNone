using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Response
{
    /// <summary>
    /// Enumerable DTO
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    public class ResponseEnumerableDTO<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<TDTO> Entities { get; set; }
    }
}
