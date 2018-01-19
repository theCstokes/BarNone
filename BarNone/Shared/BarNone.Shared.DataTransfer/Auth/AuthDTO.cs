namespace BarNone.Shared.DataTransfer.Auth
{
    /// <summary>
    /// Authentication dto.
    /// </summary>
    public class AuthDTO
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string Access_Token { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public int Expires_In { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AuthDTO"/> is authorized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if authorized; otherwise, <c>false</c>.
        /// </value>
        public bool Authorized { get; set; }
    }
}
