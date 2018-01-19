using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Response
{
    /// <summary>
    /// Entity DTO
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    public class ResponseEntityDTO<TDTO>
        where TDTO : BaseDTO<TDTO>, new()
    {
        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public TDTO Entity { get; set; }
    }
}
