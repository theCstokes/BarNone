namespace BarNone.Shared.DataTransfer.Core
{
    /// <summary>
    /// Base type dto.
    /// </summary>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseDTO{TDTO}" />
    public abstract class BaseTypeDTO<TDTO> : BaseDTO<TDTO>
        where TDTO : BaseTypeDTO<TDTO>, new()
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public abstract int Value { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract string Name { get; set; }
    }
}
