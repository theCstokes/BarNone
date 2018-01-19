using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Core
{
    /// <summary>
    /// Domain context.
    /// </summary>
    public interface IDomainContext
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserID { get; set; }
    }
}
