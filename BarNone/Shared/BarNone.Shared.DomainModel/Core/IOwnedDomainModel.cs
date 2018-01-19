using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DomainModel.Core
{
    /// <summary>
    /// Domain model owned by user.
    /// </summary>
    public interface IOwnedDomainModel
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
