using BarNone.Shared.Core;
using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DataTransfer
{
    /// <summary>
    /// User dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDTO{BarNone.Shared.DataTransfer.UserDTO}" />
    public class UserDTO : BaseDTO<UserDTO>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [JsonIgnoreDeserialize]
        public string Password { get; set; }
    }
}
