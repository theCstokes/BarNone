using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDTO{BarNone.Shared.DataTransfer.UserChangePasswordDTO}" />
    public class UserChangePasswordDTO: BaseDTO<UserChangePasswordDTO>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the retype password.
        /// </summary>
        /// <value>
        /// The retype password.
        /// </value>
        public string RetypePassword { get; set; }       
    }
}
