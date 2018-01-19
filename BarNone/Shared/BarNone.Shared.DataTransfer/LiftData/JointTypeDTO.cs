using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DataTransfer.LiftData
{
    /// <summary>
    /// Joint type dto.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataTransfer.Core.BaseTypeDTO{BarNone.Shared.DataTransfer.LiftData.JointTypeDTO}" />
    public class JointTypeDTO : BaseTypeDTO<JointTypeDTO>
    {
        #region Public Property(s).        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override int ID { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public override int Value { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name { get; set; }
        #endregion
    }
}
