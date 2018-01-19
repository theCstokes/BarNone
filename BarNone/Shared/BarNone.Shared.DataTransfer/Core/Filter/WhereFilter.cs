namespace BarNone.Shared.DataTransfer.Core.Filter
{
    /// <summary>
    /// Where filter dto.
    /// </summary>
    public class WhereFilter
    {
        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the op.
        /// </summary>
        /// <value>
        /// The op.
        /// </value>
        public FilterOpType Op { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }
    }
}
